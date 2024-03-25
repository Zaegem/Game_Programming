using System;
using GXPEngine.Core;

namespace GXPEngine
{
    class Player : AnimationSprite
    {
        public Vec2 position
        {
            get
            {
                return _position;
            }
        }

        LevelManager levelManager;
        private Vec2 _position;
        public Vec2 velocity;
        int counter;
        int frame;

        float speed = 0;

        Vec2 position;
        Vec2 velocity = new Vec2(1, 1);

        //test

        public Player(LevelManager levelManager, Vec2 position) : base("assets/Run.png", 12, 1)
        {
            this.levelManager = levelManager;
            this.position = position;
            x = this.position.x;
            y = this.position.y;

            scale = 0.75f;
        }

        void Update()
        {
            Movement();
            Animation();
        }

        void Movement()
        {

            //moving
            if (Input.GetKey(Key.W))
            {
                if (speed < 5)
                {
                    speed += 2.5f;
                }
                else
                {
                    if (speed > 0)
                    {
                        speed -= 1f;
                    }
                }
            }
            else
            {
                MoveUntilCollision(0, 5);
            }

            
            velocity.y *= speed;
            position.y += velocity.y;

        }

        void Animation()
        {
            counter++;

            if (counter > 10)
            {
                counter = 0;
                frame++;
                if (frame == frameCount)
                {
                    frame = 0;
                }
                SetFrame(frame);
            }
        }
    }
}
