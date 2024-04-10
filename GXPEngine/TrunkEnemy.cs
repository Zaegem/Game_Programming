using GXPEngine;
using System.Diagnostics;
using System.Reflection;

    internal class TrunkEnemy : Enemy
    {
        private int counter;
        private int frame;
    public TrunkEnemy(Vec2 position) : base(position)
        {
            attackAnimationSprite = new AnimationSprite("assets/TrunkAttack.png", 11, 1);
            attackAnimationSprite.visible = false;
            attackAnimationSprite.collider.isTrigger = true;
            AddChild(attackAnimationSprite);

            TakeDamageAnimationSprite = new AnimationSprite("assets/TrunkHit.png", 5, 1);
            TakeDamageAnimationSprite.visible = false;
            TakeDamageAnimationSprite.collider.isTrigger = true;
            AddChild(TakeDamageAnimationSprite);

            IdleAnimationSprite = new AnimationSprite("assets/TrunkIdle.png", 18, 1);
            IdleAnimationSprite.visible = false;
            IdleAnimationSprite.collider.isTrigger = true;
            AddChild(IdleAnimationSprite);
        }

    public override void Kill()
    {

    }

    public override void Animation()
    {
        base.Animation();
        if(counter >= 6)
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

    
}
