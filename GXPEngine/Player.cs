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
        int jumpPower = 5;
        float speed = 0;

        public Player(LevelManager levelManager) : base("assets/Run.png", 12, 1)
        {
            this.levelManager = levelManager;

            scale = 0.75f;
        }

        void Update()
        {
            PlayerMovement();
            PlayerAnimation();

        }

        void PlayerMovement()
        {
            if (Input.GetKey(Key.W))
            {
                if (speed < 5)
                {
                    speed += 1;
                }
                else
                {
                    if (speed > 0)
                    {
                        speed -= 0.1f;
                    }
                }

            }

            velocity *= speed;
            _position += velocity;
        }

        void PlayerAnimation()
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
