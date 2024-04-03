using System;
using System.Collections.Generic;
using GXPEngine.Core;
using GXPEngine;
public enum PlayerState
{
    Idle,
    MovingRight,
    MovingLeft
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

    AnimationSprite currentAnimation = null;
    AnimationSprite idleAnimationSprite;
    AnimationSprite moveRightAnimationSprite;
    AnimationSprite moveLeftAnimationSprite;

    public PlayerState playerState;


    private List<Bullet> bullets = new List<Bullet>();

    public Player(Vec2 position)
    {
        x = position.x;
        y = position.y;

        scale = 0.75f;

        playerState = PlayerState.Idle;

        idleAnimationSprite = new AnimationSprite("assets/idle.png", 11, 1);
        AddChild(idleAnimationSprite);
        idleAnimationSprite.visible = false;

        moveRightAnimationSprite = new AnimationSprite("assets/RunRight.png", 12, 1);
        AddChild(moveRightAnimationSprite);
        moveRightAnimationSprite.visible = false;

        moveLeftAnimationSprite = new AnimationSprite("assets/RunLeft.png", 12, 1);
        AddChild(moveLeftAnimationSprite);
        moveLeftAnimationSprite.visible = false;

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

    void Movement()
    {
        if (!isFalling && Input.GetKeyDown(Key.W))
        {
            velocity += new Vec2(0, -1) * jumpForce;
        }
        else
        {
            playerState = PlayerState.Idle;
        }

        Vec2 direction = new Vec2(0, 0);
        if (Input.GetKey(Key.A))
        {
            direction += new Vec2(-1, 0);
            playerState = PlayerState.MovingLeft;
        }

        if (Input.GetKey(Key.D))
        {
            direction += new Vec2(1, 0);
            playerState = PlayerState.MovingRight;
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
            case PlayerState.Idle:
                currentAnimation = idleAnimationSprite;
                break;
            case PlayerState.MovingRight:
                currentAnimation = moveRightAnimationSprite;
                break;
            case PlayerState.MovingLeft:
                currentAnimation = moveLeftAnimationSprite;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (currentAnimation != prevAnimation)
        {
            if (prevAnimation != null)
            {
                prevAnimation.visible = false;
            }

            currentAnimation.visible = true;
        }

        if (!Input.GetKey(Key.D) && !Input.GetKey(Key.A))
        {
            playerState = PlayerState.Idle;
        }

        if (counter > 5)
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
