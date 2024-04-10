using System;
using System.Drawing;
using GXPEngine;
using GXPEngine.Core;

class Bullet : GameObject
{
    public event Action<Bullet> OnDestroyed;

    private const int radius = 8;
    private float bulletSpeed;

    bool isColliding = false;

    private Sprite sprite;

    public Bullet(Sprite sprite, float bulletSpeed) : base(true)
    {
        this.bulletSpeed = bulletSpeed;
        this.sprite = sprite;

        AddChild(sprite);
    }

    void Update()
    {
        Move();
    }

    protected override Collider createCollider()
    {
        Bitmap bitmap = new Bitmap(radius, radius);
        Sprite colliderSprite = new Sprite(bitmap, false);
        AddChild(colliderSprite);
        BoxCollider boxCollider = new BoxCollider(colliderSprite);
        boxCollider.isTrigger = true;
        return boxCollider;
    }

    void Move()
    {
        Collision collision = MoveUntilCollision(bulletSpeed, 0f);

        Console.WriteLine($"collision: {collision != null}");

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
