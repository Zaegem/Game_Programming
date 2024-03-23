using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public struct Vec2
{
    public float x;
    public float y;

    public Vec2(float pX = 0, float pY = 0)
    {
        x = pX;
        y = pY;
    }

    public static float DegToRad(float degrees)
    {
        return degrees * (Mathf.PI / 180);
    }

    public static Vec2 GetUnitVectorDeg(float degrees)
    {
        float radians = Vec2.DegToRad(degrees);
        float x = Mathf.Cos(radians);
        float y = Mathf.Sin(radians);
        return new Vec2(x, y);
    }

    public static Vec2 operator +(Vec2 left, Vec2 right)
    {
        return new Vec2(left.x + left.x, right.y + right.y);
    }

    public static Vec2 operator *(Vec2 left, float right)
    {
        return new Vec2(left.x * right, left.y * right);
    }
}