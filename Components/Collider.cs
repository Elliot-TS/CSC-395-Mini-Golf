using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
// using System.Numerics;

//public struct Circle
//{
//public Point Position;
//public int Radius;
//public Circle(Point position, int radius) {
//Position = position;
//Radius = radius;
//}

//public override string ToString() => $"\{Pos: {Position},  Rad: {Radius}\}"
//}

public static class CollisionAlgorithms
{
    public static bool RectRect (Rectangle a, Rectangle b) {
        // TODO: Verify that Rectangle.Contains implies Rectangle.Intersects
        return a.Intersects(b);
    }
    
    // The width of the bounding rectangle is taken to be the radius, and the height is assumed to be the same
    public static bool CircleCircle (Rectangle a, Rectangle b) {
        return new Vector2(a.X - b.X, a.Y - b.Y).Length() <= a.Width + b.Width;
    }
    public static bool RectCircle (Rectangle rect, Rectangle circle) {
        int distX = Math.Abs(circle.X - rect.X);
        int distY = Math.Abs(circle.Y - rect.Y);

        if (distX > (rect.Width/2 + circle.Width)) return false;
        if (distY > (rect.Height/2 + circle.Width)) return false;

        if (distX <= rect.Width/2) return true;
        if (distY <= rect.Height/2) return true;

        int cornerDX = distX - rect.Width/2;
        int cornerDY = distY - rect.Height/2;
        int cornerD = cornerDX*cornerDX + cornerDY*cornerDY;

        return cornerD <= circle.Width*circle.Width;
    }
}

public abstract class Collider : Component
{
    public Rectangle BoundingBox;

    public virtual bool CollidesWith(Collider that) {
        return CollisionAlgorithms.RectRect(this.BoundingBox, that.BoundingBox);
    }
    public List<Collider> CollidesWith(List<Collider> those) {
        return those.Where(CollidesWith).ToList();
    }
    public override void Update(float deltaTime) { 
        if (parent is null) return;
        BoundingBox = parent.GetBoundingBox();
    }
}

public class RectangleCollider : Collider
{
    public RectangleCollider() { }

    public override bool CollidesWith(Collider that) => that switch
    {
        RectangleCollider rc => CollisionAlgorithms.RectRect(this.BoundingBox, rc.BoundingBox),
        CircleCollider cc => CollisionAlgorithms.RectCircle(this.BoundingBox, that.BoundingBox),
        _ => base.CollidesWith(that),
    };
}

public class CircleCollider : Collider
{
    public CircleCollider() {
    }

    public override bool CollidesWith(Collider that) => that switch
    {
        CircleCollider cc => CollisionAlgorithms.CircleCircle(this.BoundingBox, that.BoundingBox),
        _ => CollisionAlgorithms.RectCircle(that.BoundingBox, this.BoundingBox),
    };
}
