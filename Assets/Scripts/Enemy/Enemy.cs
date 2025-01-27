using UnityEngine;
public class Enemy : Enity
{
    [SerializeField] protected LayerMask whatIsPlayer;
    [SerializeField] protected float playerCheckRange;
    public EnemyStateMachine enemyStateMachine { get; private set; }
    public float moveSpeed;

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

    public bool IsPlayerDetected()
    {
        return Physics2D.Raycast(horizontalCheck.position, faceRight ? Vector2.right : Vector2.left, playerCheckRange, whatIsPlayer);

    }
}