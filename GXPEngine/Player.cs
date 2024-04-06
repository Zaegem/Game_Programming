using System;
using System.Collections.Generic;
using GXPEngine.Core;
using GXPEngine;
public enum PlayerState
{
    IdleRight,
    IdleLeft,
    MovingRight,
    MovingLeft,
    JumpRight,
    JumpLeft,
    FallRight,
    FallLeft
}
class Player : GameObject
{
    int counter;
    int frame;
    private int offSet = 10;

    private float speed = 320;
    private float jumpForce = 8;
    private float drag = 0.05f;
    private float characterMass = 40f;

    private Vec2 gravity = new Vec2(0, 9.81f);
    private Vec2 velocity;

    private bool isFalling = true;
    private bool isLookingLeft = false;

    AnimationSprite currentAnimation;
    AnimationSprite idleRightAnimationSprite =  new AnimationSprite("assets/idleRight.png", 11, 1);
    AnimationSprite idleLeftAnimationSprite = new AnimationSprite("assets/idleLeft.png", 11, 1);
    AnimationSprite moveRightAnimationSprite = new AnimationSprite("assets/RunRight.png", 12, 1);
    AnimationSprite moveLeftAnimationSprite = new AnimationSprite("assets/RunLeft.png", 12, 1);
    AnimationSprite jumpRightAnimationSprite = new AnimationSprite("assets/JumpRight.png", 1, 1);
    AnimationSprite jumpLeftAnimationSprite = new AnimationSprite("assets/JumpLeft.png", 1, 1);
    AnimationSprite fallRightAnimationSprite = new AnimationSprite("assets/FallRight.png", 1, 1);
    AnimationSprite fallLeftAnimationSprite = new AnimationSprite("assets/FallLeft.png", 1, 1);

    public PlayerState playerState;


    private List<Bullet> bullets = new List<Bullet>();

    public Player(Vec2 position) : base(true)
    {
        x = position.x;
        y = position.y;

        scale = 0.75f;

        AddChild(idleRightAnimationSprite);
        idleRightAnimationSprite.visible = false;

        AddChild(idleLeftAnimationSprite);
        idleLeftAnimationSprite.visible = false;

        AddChild(moveRightAnimationSprite);
        moveRightAnimationSprite.visible = false;

        AddChild(moveLeftAnimationSprite);
        moveLeftAnimationSprite.visible = false;

        AddChild(jumpRightAnimationSprite);
        jumpRightAnimationSprite.visible = false;

        AddChild(jumpLeftAnimationSprite);
        jumpLeftAnimationSprite.visible = false;

        AddChild(fallRightAnimationSprite);
        fallRightAnimationSprite.visible = false;

        AddChild(fallLeftAnimationSprite);
        fallLeftAnimationSprite.visible = false;

    }

    void Update()
    {
        Movement();
        Animation();
        SpawnBullet(x + offSet, y + offSet);
    }

    void SpawnBullet(float x, float y)
    {
        if (Input.GetKeyUp(Key.SPACE))
        {

            Bullet bullet = new Bullet();
            game.AddChild(bullet);
            bullet.SetXY(x, y);
            bullets.Add(bullet);
        }
    }

    protected override Collider createCollider()
    {
        return new BoxCollider(idleRightAnimationSprite);
    }

    void Movement()
    {
        if (!isFalling && Input.GetKeyDown(Key.W))
        {
            velocity += new Vec2(0, -1) * jumpForce;

            if(isLookingLeft)
            {
                playerState = PlayerState.JumpLeft;
            } else
            {
                playerState = PlayerState.JumpRight;
            }
        }

        Vec2 direction = new Vec2(0, 0);
        if (Input.GetKey(Key.A))
        {
            direction += new Vec2(-1, 0);
            playerState = PlayerState.MovingLeft;
            isLookingLeft = true;
        }

        if (Input.GetKey(Key.D))
        {
            direction += new Vec2(1, 0);
            playerState = PlayerState.MovingRight;
            isLookingLeft = false;
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
        AnimationSprite prevAnimation = currentAnimation;
        switch (playerState)
        {
            case PlayerState.IdleRight:
                currentAnimation = idleRightAnimationSprite;
                break;
            case PlayerState.IdleLeft:
                currentAnimation = idleLeftAnimationSprite;
                break;
            case PlayerState.MovingRight:
                currentAnimation = moveRightAnimationSprite;
                break;
            case PlayerState.MovingLeft:
                currentAnimation = moveLeftAnimationSprite;
                break;
            case PlayerState.JumpRight:
                currentAnimation = jumpRightAnimationSprite;
                break;
            case PlayerState.JumpLeft:
                currentAnimation = jumpLeftAnimationSprite;
                break;
            case PlayerState.FallRight:
                currentAnimation = jumpLeftAnimationSprite;
                break;
            case PlayerState.FallLeft:
                currentAnimation = jumpLeftAnimationSprite;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if(isLookingLeft)
        {
            playerState = PlayerState.IdleLeft;
        } else
        {
            playerState = PlayerState.IdleRight;
        }



        if (currentAnimation != prevAnimation)
        {
            if (prevAnimation != null)
            {
                prevAnimation.visible = false;
            }

            currentAnimation.visible = true;
        }

        if (counter >= 6)
        {
            counter = 0;
            if (frame == currentAnimation.frameCount)
            {
                frame = 0;
            }
            currentAnimation.SetFrame(frame);
            frame++;
        }
        counter++;
    }
}
