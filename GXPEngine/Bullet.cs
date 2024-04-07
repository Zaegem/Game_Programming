using System;
using GXPEngine;
using GXPEngine.Core;

class Bullet : GameObject
{
    public event Action<Bullet> OnDestroyed;

    private int radius;
    private float bulletSpeed = 2f;

    private bool isColliding = false;

    Sprite sprite = new Sprite("assets/Bullet.png");

    public Bullet(int radius = 8) : base(true)
    {
        this.radius = radius;

        sprite.collider.isTrigger = true;
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
        Collision collision = MoveUntilCollision(bulletSpeed, 0f);

        if (collision != null)
        {
            LateDestroy();

            if (OnDestroyed != null)
            {
                OnDestroyed.Invoke(this);
            }
        }
    }

    void CheckCollision()
    {
        Collision collision = MoveUntilCollision(x, y);
        isColliding = collision != null;

    }
}
