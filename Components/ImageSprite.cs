using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

public class ImageSprite : Component {
    private Texture2D imageTexture;
    private SpriteBatch _spriteBatch;
    public bool Visible;

    public ImageSprite (
        string name, 
        SpriteBatch spriteBatch, 
        ContentManager Content
    ) {
        imageTexture = Content.Load<Texture2D>(name);
        _spriteBatch = spriteBatch;
        Visible = true;
    }
    
    public override void Draw(float gameTime) {
        if (parent is null || _spriteBatch is null || !Visible) return;

        _spriteBatch.Begin(transformMatrix: parent.ModelMatrix);
        _spriteBatch.Draw(
            imageTexture,
            new Rectangle(
                new Point(0,0),
                new Point(1,1)
            ),
            Color.White
        );
        _spriteBatch.End();
    }
}