using System;
using System.Drawing;
using GXPEngine;
using GXPEngine.Core;

public enum BulletFaction
{
    Player,
    Enemy
}

class Bullet : GameObject
{
    public event Action<Bullet> OnDestroyed;

    private const int radius = 8;
    private float bulletSpeed;
    private float bulletDamage;

    private Sprite sprite;
    private BulletFaction faction;

    public Bullet(Sprite sprite, float bulletSpeed, BulletFaction faction, float bulletDamage = 1) : base(true)
    {
        this.sprite = sprite;
        this.bulletSpeed = bulletSpeed;
        this.faction = faction;
        this.bulletDamage = bulletDamage;

        AddChild(sprite);
    }

    void Update()
    {
        MoveAndCollision();
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

    void MoveAndCollision()
    {
        Collision collision = MoveUntilCollision(bulletSpeed, 0f);

        if (collision != null)
        {
            if (collision.other is Player player && faction == BulletFaction.Enemy)
            {
                player.TakeDamage(bulletDamage);
            }

            if (collision.other is Enemy enemy && faction == BulletFaction.Player)
            {
                enemy.TakeDamage(bulletDamage);
            }

            LateDestroy();

            if (OnDestroyed != null)
            {
                OnDestroyed.Invoke(this);
            }
        }
    }
}
