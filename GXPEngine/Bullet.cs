using System.Drawing;
using GXPEngine;
using GXPEngine.Core;

class Bullet : GameObject
{
    private int radius;
    private int bulletSpeed = 2;

    private bool isColliding = false;

    Sprite sprite = new Sprite("assets/Bullet.png");

    public Bullet(int radius = 8)
    {
        this.radius = radius;

        AddChild(sprite);
    }

    void Update()
    {
        Move();
    }

    protected override Collider createCollider()
    {
        BoxCollider boxCollider = new BoxCollider(sprite);
        boxCollider.isTrigger = true;
        return boxCollider;
    }

    void Move()
    {
        MoveUntilCollision(bulletSpeed, 0);
    }

    void CheckCollision()
    {
        Collision collision = MoveUntilCollision(x, y);
        isColliding = collision != null;

    }
}
