using GXPEngine;

public struct Vec2
{
    public float x;
    public float y;

    public Vec2(float pX, float pY)
    {
        x = pX;
        y = pY;
    }

    public float Length()
    {
        return Mathf.Sqrt(x * x + y * y);
    }
    
    public static Vec2 operator +(Vec2 left, Vec2 right)
    {
        return new Vec2(left.x + right.x, left.y + right.y);
    }

    public static Vec2 operator -(Vec2 left, Vec2 right)
    {
        return new Vec2(left.x - right.x, left.y - right.y);
    }

    public static Vec2 operator *(Vec2 left, float right)
    {
        return new Vec2(left.x * right, left.y * right);
    }

    public static Vec2 operator /(Vec2 left, float right)
    {
        return new Vec2(left.x / right, left.y / right);
    }

    public static bool operator ==(Vec2 left, Vec2 right)
    {
        return left.x == right.x && left.y == right.y;
    }

    public static bool operator !=(Vec2 left, Vec2 right)
    {
        return left.x != right.x || left.y != right.y;
    }

    public override string ToString()
    {
        return $"({x}, {y})";
    }
}
