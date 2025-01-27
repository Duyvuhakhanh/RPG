using UnityEngine;
public class Enemy : Enity
{
    [SerializeField] protected LayerMask whatIsPlayer;
    [SerializeField] protected float playerCheckRange;
    public EnemyStateMachine enemyStateMachine { get; private set; }
    [Header("Move Settings")]
    public float moveSpeed;

    public float idleTime;
    [Header("Attack Info")]
    public float attackRange;
    protected override void Awake()
    {
        base.Awake();
        enemyStateMachine = new EnemyStateMachine();
    }
    protected override void Update()
    {
        base.Update();
        enemyStateMachine.Update();
    }
    public override void AnimationFinishTriger()
    {
        enemyStateMachine.CurrentState.AnimationTrigger();
    }
    public virtual RaycastHit2D IsPlayerInSight()
    {
        return Physics2D.Raycast(transform.position, Vector2.right * faceDir, playerCheckRange, whatIsPlayer);
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackRange * faceDir, transform.position.y, transform.position.z));
    }
}