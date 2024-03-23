

namespace GXPEngine
{
    class Player : AnimationSprite
    {

        LevelManager levelManager;
        int counter;
        int frame;
        int jumpPower = 5;
        int speed = 1;

        //test

        public Player(LevelManager levelManager) : base("assets/Run.png", 12, 1)
        {
            this.levelManager = levelManager;

            scale = 0.75f;
        }

        void Update()
        {
            counter++;

            if(counter > 10)
            {
                counter = 0;
                frame++;
                if(frame == frameCount)
                {
                    frame = 0;
                }
                SetFrame(frame);
            }

            //moving
            if (Input.GetKey(Key.W))
            {
                Move(0, -5);
            } else
            {
                MoveUntilCollision(0, 5);
            }

            if (Input.GetKey(Key.S))
            {
                MoveUntilCollision(0, speed);
            }

            if (Input.GetKey(Key.A))
            {
                MoveUntilCollision(-speed, 0);
            }

            if (Input.GetKey(Key.D))
            {
                MoveUntilCollision(speed, 0);
            }
        }
    }
}
