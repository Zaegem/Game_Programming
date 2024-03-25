using System;
using GXPEngine.Core;

namespace GXPEngine
{
    class Player : AnimationSprite
    {
        LevelManager levelManager;
        int counter;
        int frame;

        private float speed = 320;
        private float jumpForce = 8;

        // gravity is a vector, because it signifies a force applied at a direction 
        private Vec2 gravity = new Vec2(0, 9.81f);

        //public Vec2 velocity;
        //Vec2 _velocity = new Vec2(1, 1);

        //test

        private Vec2 velocity;

        private bool isFalling = true;

        private float drag = 0.05f;
        private float characterMass = 40f;

        public Player(LevelManager levelManager, Vec2 position) : base("assets/Run.png", 12, 1)
        {
            this.levelManager = levelManager;
            x = position.x;
            y = position.y;

            scale = 0.75f;
        }

        void Update()
        {
            Movement();
            Animation();
        }

        void Movement()
        {
            ////moving
            //if (Input.GetKey(Key.W))
            //{


            //    if (speed < 5)
            //    {
            //        speed += 2.5f;
            //    }
            //    else
            //    {
            //        if (speed > 0)
            //        {
            //            speed -= 1f;
            //        }
            //    }
            //}
            //else
            //{
            //    MoveUntilCollision(0, gravity);
            //}


            //velocity.y *= speed;
            //_position.y += velocity.y;
            //Console.WriteLine(" " + y);


            // you don't want to set the position directly
            // you wanna use MoveUntilCollision to move always
            // because that also does collision checking for you
            // if you set the position directly, you will go through the colliders

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


            Console.WriteLine($"velocity: {velocity}");

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
