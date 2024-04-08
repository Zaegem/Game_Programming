using System;
using GXPEngine;

public enum EnemyState
{
    Attack,
    TakeDamage,
    Idle
}
internal class Enemy : GameObject
{
    public EnemyState enemyState;

    protected AnimationSprite currentAnimation;
    protected AnimationSprite attackAnimationSprite;
    protected AnimationSprite TakeDamageAnimationSprite;
    protected AnimationSprite IdleAnimationSprite;
    public Enemy(Vec2 position) : base(true)
    {
        x = position.x;
        y = position.y;

        attackAnimationSprite.visible = false;
        attackAnimationSprite.collider.isTrigger = true;
        AddChild(attackAnimationSprite);

        TakeDamageAnimationSprite.visible = false;
        TakeDamageAnimationSprite.collider.isTrigger = true;
        AddChild(TakeDamageAnimationSprite);

        IdleAnimationSprite.visible = false;
        IdleAnimationSprite.collider.isTrigger = true;
        AddChild(IdleAnimationSprite);
    }

    public virtual void Update()
    {

    }

    public virtual void Animation()
    {
        AnimationSprite prevAnimation = currentAnimation;
        switch(enemyState)
        {
            case EnemyState.Attack:
                currentAnimation = attackAnimationSprite;
                break;
            case EnemyState.TakeDamage:
                currentAnimation = TakeDamageAnimationSprite;
                break;
            case EnemyState.Idle:
                currentAnimation = IdleAnimationSprite;
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

    }
}
