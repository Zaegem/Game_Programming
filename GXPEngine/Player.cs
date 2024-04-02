using System;
using System.Collections.Generic;
using GXPEngine.Core;

namespace GXPEngine
{
    class Player : AnimationSprite
    {
        int counter;
        int frame;

        private float speed = 320;
        private float jumpForce = 8;
        private float drag = 0.05f;
        private float characterMass = 40f;

        private Vec2 gravity = new Vec2(0, 9.81f);
        private Vec2 velocity;

        private bool isFalling = true;


        private List<Bullet> bullets = new List<Bullet>();

        public Player(Vec2 position) : base("assets/Run.png", 12, 1)
        {
            x = position.x;
            y = position.y;

            scale = 0.75f;
        }

        void Update()
        {
            Movement();
            Animation();
        }

        void SpawnBullet(float x, float y)
        {
            Bullet bullet = new Bullet();
            game.AddChild(bullet);
            bullet.SetXY(x, y);
            bullets.Add(bullet);
        }

        void Movement()
        {
            if (!isFalling && Input.GetKeyDown(Key.W))
            {
                velocity += new Vec2(0, -1) * jumpForce;
            }

            Vec2 direction = new Vec2(0, 0);
            if (Input.GetKey(Key.A))
            {
                direction += new Vec2(-1, 0);
            }

            if (Input.GetKey(Key.D))
            {
                direction += new Vec2(1, 0);
            }

            Vec2 acceleration = direction * speed * Time.deltaTimeSeconds;
            velocity = velocity * (1f - drag) + acceleration * drag;

            Collision collision = MoveUntilCollision(velocity.x, velocity.y);
            isFalling = collision == null;

            if (isFalling)
            {
                velocity += gravity * drag * characterMass * Time.deltaTimeSeconds;
            }
            else
            {
                velocity.y = 0;
            }
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
