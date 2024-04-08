using System;
using GXPEngine;
using GXPEngine.Core;

class Bullet : GameObject
{
    public event Action<Bullet> OnDestroyed;

    private int radius;
    private float bulletSpeed;

    bool isColliding = false;

    Sprite sprite = new Sprite("assets/Bullet.png");

    public Bullet(float bulletSpeed, int radius = 8) : base(true)
    {
        this.radius = radius;
        this.bulletSpeed = bulletSpeed;

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
}
