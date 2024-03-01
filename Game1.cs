using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Audio;

namespace Mini_Golf;

/*
TODO: You're almost done.  Got to implement hills, sounds, and track strokes.  You already added a hill image to the content manager.  Just make a rectangle,
and apply a force to the ball if it collides with the hill.
*/

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private List<Entity> entities;
    private Entity golfBall;
    private Collider golfCollider;
    private ImageSprite golfSprite;
    private RigidBody golfPhysics;

    private Entity hill;
    private Collider hillCollider;

    private List<Collider> horWallColliders;
    private List<Collider> vertWallColliders;

    private Collider holeCollider;

    private bool unstick;

    private Point mouseClickPos;
    private bool mouseClicked;
    
    private bool win;
    private int strokeCount;


    SoundEffect wallSound;
    SoundEffect holeSound;
    SoundEffect swooshSound;
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        entities = new List<Entity>();
        unstick = false;
        mouseClicked = false;
        win = false;
    }

    protected override void Initialize()
    {
        hill = new Entity(new Vector2(300, 200), new Vector2(175, 100), 0);
        entities.Add(hill);
        hillCollider = new RectangleCollider();
        hill.AddComponent(hillCollider);

        // Create the golf ball
        golfBall = new Entity(
            new Vector2(600,100),
            new Vector2(25,25),
            0
        ); entities.Add(golfBall);

        golfCollider = new RectangleCollider();
        golfPhysics = new RigidBody(1);

        golfBall.AddComponents(new List<Component>(){golfCollider, golfPhysics});

        holeCollider = new RectangleCollider();


        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        wallSound = Content.Load<SoundEffect>("walfx");
        holeSound = Content.Load<SoundEffect>("holefx");
        swooshSound= Content.Load<SoundEffect>("swingfx");

        // Golf Ball
        golfSprite = new ImageSprite(
            "golf_ball",
            _spriteBatch,
            Content
        );
        golfBall.AddComponent( golfSprite);
        
        // Hill
        hill.AddComponent(new ImageSprite(
            "hill",
            _spriteBatch,
            Content
        ));

        // Create walls
        vertWallColliders = new List<Collider>(){
            new RectangleCollider(),
            new RectangleCollider(),
            new RectangleCollider(),
        };
        horWallColliders = new List<Collider>() {
            new RectangleCollider(),
            new RectangleCollider()
        };
        entities.Add(new Entity(
            new Vector2(0,0),
            new Vector2(10, 400),
            0,
            new List<Component>(){
                new ImageSprite("squirrel", _spriteBatch, Content),
                vertWallColliders[0]
            }
        ));
        entities.Add(new Entity(
            new Vector2(0,390),
            new Vector2(700, 10),
            0,
            new List<Component>(){
                new ImageSprite("squirrel", _spriteBatch, Content),
                horWallColliders[0]
            }
        ));
        entities.Add(new Entity(
            new Vector2(690,0),
            new Vector2(10, 400),
            0,
            new List<Component>(){
                new ImageSprite("squirrel", _spriteBatch, Content),
                vertWallColliders[1]
            }
        ));
        entities.Add(new Entity(
            new Vector2(0,0),
            new Vector2(700, 10),
            0,
            new List<Component>(){
                new ImageSprite("squirrel", _spriteBatch, Content),
                horWallColliders[1]
            }
        ));
        entities.Add(new Entity(
            new Vector2(200,0),
            new Vector2(10, 200),
            0,
            new List<Component>(){
                new ImageSprite("squirrel", _spriteBatch, Content),
                vertWallColliders[2]
            }
        ));


        // Hole
        entities.Add(new Entity(
            new Vector2(50, 50),
            new Vector2(25,25),
            0,
            new List<Component>(){
                new ImageSprite("hole", _spriteBatch, Content),
                holeCollider
            }
        ));
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        entities.ForEach(it => it.Update((float)gameTime.ElapsedGameTime.TotalSeconds));

        if (win) {}
        else if (golfPhysics.Velocity.Length() > 5) {
            // Wall Collision
            List<Collider> horColls = golfCollider.CollidesWith(horWallColliders);
            List<Collider> vertColls = golfCollider.CollidesWith(vertWallColliders);
            if (horColls.Count != 0 || vertColls.Count != 0)
            {
                if (!unstick)
                {
                    if (horColls.Count != 0) golfPhysics.Velocity.Y *= -1;
                    if (vertColls.Count != 0) golfPhysics.Velocity.X *= -1;
                    unstick = true;
                    wallSound.Play();
                }
            }
            else { unstick = false; }

            if (golfCollider.CollidesWith(holeCollider) && golfPhysics.Velocity.Length() < 100) {
                win = true;
                golfPhysics.Velocity = new Vector2(0,0);
                golfSprite.Visible = false;
                holeSound.Play();
            }


            if (golfCollider.CollidesWith(hillCollider)) {
                golfPhysics.ApplyForce(new Vector2(100,0));
            }

            golfPhysics.Velocity *= 0.99f;
        }
        else {
            golfPhysics.Velocity = new Vector2(0,0);

            MouseState mouse = Mouse.GetState();

            // Press Mouse
            if (!mouseClicked && mouse.LeftButton == ButtonState.Pressed) {
                mouseClickPos = mouse.Position;
                mouseClicked = true;
            }
            // Release
            if (mouseClicked && mouse.LeftButton == ButtonState.Released) {
                golfPhysics.Velocity = (mouseClickPos - mouse.Position).ToVector2() * 2;
                mouseClicked = false;
                swooshSound.Play();
                strokeCount ++;
            }
        }


        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        entities.ForEach(it => it.Draw(((float)gameTime.ElapsedGameTime.TotalSeconds)));
        _spriteBatch.Begin();
        _spriteBatch.DrawString(Content.Load<SpriteFont>("font"), "Strokes: " + strokeCount.ToString(), new Vector2(300,400), Color.Black);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
