using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
    AnimationSprite idleRightAnimationSprite = new AnimationSprite("assets/idleRight.png", 11, 1);
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

        idleRightAnimationSprite.visible = false;
        idleRightAnimationSprite.collider.isTrigger = true;
        AddChild(idleRightAnimationSprite);

        idleLeftAnimationSprite.visible = false;
        idleLeftAnimationSprite.collider.isTrigger = true;
        AddChild(idleLeftAnimationSprite);

        moveRightAnimationSprite.visible = false;
        moveRightAnimationSprite.collider.isTrigger = true;
        AddChild(moveRightAnimationSprite);

        moveLeftAnimationSprite.visible = false;
        moveLeftAnimationSprite.collider.isTrigger = true;
        AddChild(moveLeftAnimationSprite);

        jumpRightAnimationSprite.visible = false;
        jumpRightAnimationSprite.collider.isTrigger = true;
        AddChild(jumpRightAnimationSprite);


        jumpLeftAnimationSprite.visible = false;
        jumpLeftAnimationSprite.collider.isTrigger = true;
        AddChild(jumpLeftAnimationSprite);


        fallRightAnimationSprite.visible = false;
        fallRightAnimationSprite.collider.isTrigger = true;
        AddChild(fallRightAnimationSprite);


        fallLeftAnimationSprite.visible = false;
        fallLeftAnimationSprite.collider.isTrigger = true;
        AddChild(fallLeftAnimationSprite);
    }

    void Update()
    {
        Movement();
        Animation();
        SpawnBullet(x + offSet, y + offSet);
    }

    void SpawnBullet(float x, float y)
    {
        if(Input.GetKeyUp(Key.SPACE))
        {

            Bullet bullet = new Bullet();
            bullet.SetXY(x, y);

            bullet.OnDestroyed += OnBulletDestroyed;

            game.AddChild(bullet);
            bullets.Add(bullet);
        }
    }

    private void OnBulletDestroyed(Bullet bullet)
    {
        bullet.OnDestroyed -= OnBulletDestroyed;
        bullets.Remove(bullet);
    }

    protected override Collider createCollider()
    {
        BoxCollider boxCollider = new BoxCollider(idleRightAnimationSprite);
        boxCollider.isTrigger = true;
        return boxCollider;
    }

    void Movement()
    {
        if(!isFalling && Input.GetKeyDown(Key.W))
        {
            velocity += new Vec2(0, -1) * jumpForce;
            playerState = PlayerState.JumpRight;
        }

        Vec2 direction = new Vec2(0, 0);
        if(Input.GetKey(Key.A))
        {
            direction += new Vec2(-1, 0);
            playerState = PlayerState.MovingLeft;
            isLookingLeft = true;
        }

        if(Input.GetKey(Key.D))
        {
            direction += new Vec2(1, 0);
            playerState = PlayerState.MovingRight;
            isLookingLeft = false;
        }

        Vec2 acceleration = direction * speed * Time.deltaTimeSeconds;
        velocity = velocity * (1f - drag) + acceleration * drag;

        Collision collision = MoveUntilCollision(velocity.x, velocity.y);
        isFalling = collision == null;

        if(isFalling)
        {
            velocity += gravity * drag * characterMass * Time.deltaTimeSeconds;
        } else
        {
            velocity.y = 0;
        }

        if(velocity.Length() <= 0.3f)
        {
            if(isLookingLeft)
            {
                playerState = PlayerState.IdleLeft;
            } else
            {
                playerState = PlayerState.IdleRight;
            }
        }
    }

    void Animation()
    {
        AnimationSprite prevAnimation = currentAnimation;
        switch(playerState)
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

        if(currentAnimation != prevAnimation)
        {
            if(prevAnimation != null)
            {
                prevAnimation.visible = false;
            }

            currentAnimation.visible = true;
        }

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
