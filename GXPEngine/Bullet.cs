using System.Drawing;
using GXPEngine;

class Bullet : GameObject
{
    private int radius;
    private int bulletSpeed = 2;

    private EasyDraw bullet;

    public Bullet(int radius = 8)
    {
        this.radius = radius;

        bullet = new EasyDraw(radius * 2, radius * 2);
        bullet.Fill(255, 165, 128);
        bullet.ShapeAlign(CenterMode.Min, CenterMode.Min);
        bullet.Stroke(Color.Black);
        bullet.Ellipse(0, 0, radius, radius);
        bullet.SetXY(x, y);
        AddChild(bullet);
    }

    void Update()
    {
        Move();
        IsColliding();
    }

    public bool IsColliding()
    {
        GameObject[] collisions = bullet.GetCollisions();
        foreach (GameObject obj in collisions)
        {

        }

        return false;
    }

    void Move()
    {
        Move(bulletSpeed, 0);
    }

    void DeleteBullet()
    {
        LateDestroy();
    }
}
