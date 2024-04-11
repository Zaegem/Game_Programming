using GXPEngine;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

internal class TrunkEnemy : Enemy
{
    private List<Bullet> enemyBullets = new List<Bullet>();

    private int counter;
    private int frame;
    private int animationSpeed = 6;
    private int bulletSpeed = -2;
    private int offSetY = 12;
    private int offSetX = -2;

    private bool hasShot = false;
    public TrunkEnemy(Vec2 position) : base(position)
    {
        attackAnimationSprite = new AnimationSprite("assets/TrunkAttack.png", 11, 1);
        attackAnimationSprite.visible = false;
        attackAnimationSprite.collider.isTrigger = true;
        AddChild(attackAnimationSprite);

        takeDamageAnimationSprite = new AnimationSprite("assets/TrunkHit.png", 5, 1);
        takeDamageAnimationSprite.visible = false;
        takeDamageAnimationSprite.collider.isTrigger = true;
        AddChild(takeDamageAnimationSprite);

        IdleAnimationSprite = new AnimationSprite("assets/TrunkIdle.png", 18, 1);
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
        if(frame == 8)
        {
            if(!hasShot)
            {
                Bullet bullet = new Bullet(new Sprite("assets/TrunkBullet.png", true, false), bulletSpeed);
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
        if (counter >= animationSpeed)
        {
            counter = 0;
            if (frame >= currentAnimation.frameCount)
            {
                frame = 0;
            }
            currentAnimation.SetFrame(frame);
            frame++;
        }
        counter++;
    }


}
