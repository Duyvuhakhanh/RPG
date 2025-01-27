using UnityEngine;
public class SkeletonBattleState : SkeletonBaseState
{
    private float moveDirection;
    private Player targetPlayer;
    public SkeletonBattleState(Enemy enemy, EnemyStateMachine stateMachine, Animator animator, string animationKey, Enemy_Skeleton skeleton) : base(enemy, stateMachine, animator, animationKey, skeleton)
    {
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();
        var playerInfo = skeleton.IsPlayerInSight();
        if (playerInfo && playerInfo.distance < skeleton.attackCheckRadius)
        {
            stateMachine.ChangeState(skeleton.attackState);
            return;
        }

        if (playerInfo)
        {
            targetPlayer = playerInfo.collider.GetComponent<Player>();
            moveDirection = (targetPlayer.transform.position - enemy.transform.position).normalized.x;
            skeleton.rb.velocity = new Vector2(skeleton.moveSpeed * moveDirection, skeleton.rb.velocity.y);
        }
        else
        {
            stateMachine.ChangeState(skeleton.idleState);
        }

    }
}