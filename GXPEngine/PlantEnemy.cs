using GXPEngine;
    internal class PlantEnemy : Enemy
    {
        private int counter;
        private int frame;
        public PlantEnemy(Vec2 position) : base(position)
        {
            attackAnimationSprite = new AnimationSprite("assets/PlantAttack.png", 8, 1);
            attackAnimationSprite.visible = false;
            attackAnimationSprite.collider.isTrigger = true;
            AddChild(attackAnimationSprite);

            TakeDamageAnimationSprite = new AnimationSprite("assets/PlantHit.png", 5, 1);
            TakeDamageAnimationSprite.visible = false;
            TakeDamageAnimationSprite.collider.isTrigger = true;
            AddChild(TakeDamageAnimationSprite);

            IdleAnimationSprite = new AnimationSprite("assets/PlantIdle.png", 11, 1);
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

