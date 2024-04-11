using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq.Expressions;
using GXPEngine;
using GXPEngine.Core;
internal class PlantEnemy : Enemy
{
    private List<Bullet> enemyBullets = new List<Bullet>();

    private int counter;
    private int frame;
    private int animationSpeed = 8;
    private int bulletSpeed = -2;
    private int offSetY = 10;
    private int offSetX = -8;

    private int width = 44;
    private int height = 42;

    private bool hasShot = false;

    public PlantEnemy(Vec2 position) : base(position)
    {
        attackAnimationSprite = new AnimationSprite("assets/PlantAttack.png", 8, 1);
        attackAnimationSprite.visible = false;
        attackAnimationSprite.collider.isTrigger = true;
        AddChild(attackAnimationSprite);


        takeDamageAnimationSprite = new AnimationSprite("assets/PlantHit.png", 5, 1);
        takeDamageAnimationSprite.visible = false;
        takeDamageAnimationSprite.collider.isTrigger = true;
        AddChild(takeDamageAnimationSprite);

        IdleAnimationSprite = new AnimationSprite("assets/PlantIdle.png", 11, 1);
        IdleAnimationSprite.visible = false;
        IdleAnimationSprite.collider.isTrigger = true;
        AddChild(IdleAnimationSprite);
    }


    public void Update()
    {
        Move();
        Animation();
        SpawnBullet(x + offSetX, y + offSetY);
    }

    public override void Kill()
    {
    }

    public override void SpawnBullet(float x, float y)
    {
        if(frame == 5)
        {
            if(!hasShot)
            {
                Bullet bullet = new Bullet(new Sprite("assets/PlantBullet.png", true, false), bulletSpeed);
                bullet.SetXY(x, y);

                bullet.OnDestroyed += OnBulletDestroyed;

                game.AddChild(bullet);
                enemyBullets.Add(bullet);
                hasShot = true;
            }
        } else
        {
            hasShot = false;
        }
    }

    private void OnBulletDestroyed(Bullet bullet)
    {
        bullet.OnDestroyed -= OnBulletDestroyed;
        enemyBullets.Remove(bullet);
    }

    public override void Animation()
    {
        base.Animation();
        if(counter >= animationSpeed)
        {
            counter = 0;
            if(frame >= currentAnimation.frameCount)
            {
                frame = 0;
            }
            currentAnimation.SetFrame(frame);
            frame++;
        }
        counter++;
    }

    protected override Collider createCollider()
    {
        float desiredWidth = width / 2f;
        float desiredHeight = height / 2f;
        Bitmap bitmap = new Bitmap((int)desiredWidth, (int)desiredHeight);
        Sprite colliderSprite = new Sprite(bitmap, false);
        AddChild(colliderSprite);
        colliderSprite.SetXY(desiredWidth / 2f, desiredHeight / 2f);
        BoxCollider boxCollider = new BoxCollider(colliderSprite);
        return boxCollider;
    }
}
