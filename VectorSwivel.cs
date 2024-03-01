using Microsoft.Xna.Framework;

public static class VectorSwivel
{
    // Vector3 Extensions
    public static Vector2 XY(this Vector3 v) => new Vector2(v.X, v.Y);
    public static Vector2 YX(this Vector3 v) => new Vector2(v.Y, v.X);
    public static Vector2 XZ(this Vector3 v) => new Vector2(v.X, v.Z);
    public static Vector2 ZX(this Vector3 v) => new Vector2(v.Z, v.X);
    public static Vector2 YZ(this Vector3 v) => new Vector2(v.Y, v.Z);
    public static Vector2 ZY(this Vector3 v) => new Vector2(v.Z, v.Y);

    public static Vector3 XYZ(this Vector3 v) => new Vector3(v.X, v.Y, v.Z);
    public static Vector3 XZY(this Vector3 v) => new Vector3(v.X, v.Z, v.Y);
    public static Vector3 YXZ(this Vector3 v) => new Vector3(v.Y, v.X, v.Z);
    public static Vector3 YZX(this Vector3 v) => new Vector3(v.Y, v.Z, v.X);
    public static Vector3 ZXY(this Vector3 v) => new Vector3(v.Z, v.X, v.Y);
    public static Vector3 ZYX(this Vector3 v) => new Vector3(v.Z, v.Y, v.X);

    // Vector2 Extensions
    public static Vector3 XY0(this Vector2 v) => new Vector3(v, 0);
    public static Vector3 XY1(this Vector2 v) => new Vector3(v, 1);
}
/*public static class VectorSwivel
{
    // Vector3 Extensions
    public static Vector2 XY(this Vector3 v) {
        return new Vector2(v.X, v.Y);
    }
    public static Vector2 YX(this Vector3 v) {
        return new Vector2(v.Y, v.X);
    }
    public static Vector2 XZ(this Vector3 v) {
        return new Vector2(v.X, v.Z);
    }
    public static Vector2 ZX(this Vector3 v) {
        return new Vector2(v.Z, v.X);
    }
    public static Vector2 YZ(this Vector3 v) {
        return new Vector2(v.Y, v.Z);
    }
    public static Vector2 ZY(this Vector3 v) {
        return new Vector2(v.Z, v.Y);
    }
    
    public static Vector3 XYZ(this Vector3 v) {
        return new Vector3(v.X, v.Y, v.Z);
    }
    public static Vector3 XZY(this Vector3 v) {
        return new Vector3(v.X, v.Z, v.Y);
    }
    public static Vector3 YXZ(this Vector3 v) {
        return new Vector3(v.Y, v.X, v.Z);
    }
    public static Vector3 YZX(this Vector3 v) {
        return new Vector3(v.Y, v.Z, v.X);
    }
    public static Vector3 ZXY(this Vector3 v) {
        return new Vector3(v.Z, v.X, v.Y);
    }
    public static Vector3 ZYX(this Vector3 v) {
        return new Vector3(v.Z, v.Y, v.X);
    }
    
    // Vector2 EXtensions
    public static Vector3 XY0(this Vector2 v) {
        return new Vector3(v, 0);
    }
    public static Vector3 XY1(this Vector2 v) {
        return new Vector3(v, 1);
    }
}*/