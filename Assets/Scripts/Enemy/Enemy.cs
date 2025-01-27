using UnityEngine;
public class Enemy : Enity
{
    [SerializeField] protected float playerCheckRange;
    public EnemyStateMachine enemyStateMachine { get; private set; }
    [Header("Move Settings")]
    public float moveSpeed;

    public float idleTime;
    [Header("Attack Info")]
    public LayerMask whatIsPlayer;
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
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackCheckRadius * faceDir, transform.position.y, transform.position.z));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + playerCheckRange* faceDir, transform.position.y, transform.position.z));
    }
}