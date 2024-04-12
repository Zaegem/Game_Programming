using GXPEngine;
using GXPEngine.Core;
using System.Drawing;

internal class TrunkEnemy : Enemy
{
    private int shootFrame = 8;
    private int animationSpeed = 6;
    private int offSetY = 12;
    private int offSetX = -2;

    private int width = 20;
    private int height = 21;

    public TrunkEnemy(Vec2 position, float health) : base(position, health)
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

    public override int GetShootFrame()
    {
        return shootFrame;
    }

    public override float GetAnimationSpeed()
    {
        return animationSpeed;
    }

    public override Sprite GetBulletSprite()
    {
        return new Sprite("assets/TrunkBullet.png", true, false);
    }

    public override int GetColliderWidth()
    {
        return width;
    }

    public override int GetColliderHeight()
    {
        return height;
    }
}
