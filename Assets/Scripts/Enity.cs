using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
public abstract class Enity : MonoBehaviour, IDamgeable
{

    #region Compoment

    protected Animator animator;
    protected EnityFx enityFx;
    public Rigidbody2D rb { get; protected set; }
    public float dashDir { get; protected set; }
    public int faceDir => faceRight ? 1 : -1;
    public bool isBusy { get; protected set; }
    #endregion
    [FormerlySerializedAs("knockBackForce")]
    [Header("KnockBack Info")]
    [SerializeField] protected Vector2 knockBackDirection;
    [SerializeField] protected float knockBackTime;
    private bool isKnockBack;
    [Header("Collision Info")] 
    public Transform attackCheck;
    public float attackCheckRadius;
    [SerializeField]
    protected float groundCheckDistance;

    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected Transform groundCheck;
    [FormerlySerializedAs("wallCheck")] [SerializeField] protected Transform horizontalCheck;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected LayerMask whatIsWall;

    [HideInInspector] public bool faceRight = true;
    public abstract void AnimationFinishTriger();

    #region Direction
    protected virtual void Awake()
    {
        if (animator == null) animator = GetComponentInChildren<Animator>();
        if (rb == null) rb = GetComponentInChildren<Rigidbody2D>();
        if (enityFx == null) enityFx = GetComponent<EnityFx>();
    }
    protected virtual void UpdateDirection()
    {
        if (rb.velocity.x > 0.01f && !faceRight)
        {
            Flip();
        }
        else if (rb.velocity.x < -0.01f && faceRight)
        {
            Flip();
        }
    }
    public virtual void Flip()
    {
        faceRight = !faceRight;
        transform.Rotate(0, 180, 0);
    }

    #endregion
    #region Collision
    public virtual bool IsOnGround()
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    }
    public virtual bool IsWallDetected()
    {
        return Physics2D.Raycast(horizontalCheck.position, faceRight ? Vector2.right : Vector2.left, wallCheckDistance, whatIsWall);
    }
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(horizontalCheck.position, new Vector2(horizontalCheck.position.x + wallCheckDistance, horizontalCheck.position.y));
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
    #endregion
    #region Velocity

    public void SetVelocity(Vector2 vector2)
    {
        if(isKnockBack) return;
        rb.velocity = vector2;
        UpdateDirection();
    }

    #endregion
    protected virtual void Update()
    {
    }
    protected virtual void FixedUpdate()
    {
    }

    private IEnumerator IKnockBack()
    {
        isKnockBack = true;
        rb.velocity = (new Vector2(knockBackDirection.x * -faceDir, knockBackDirection.y)); 
        yield return new WaitForSeconds(knockBackTime);
        rb.velocity = Vector2.zero;
        isKnockBack = false;
    }
    public void TakeDamage(int damage)
    {
        enityFx.OnHitFx();
        StartCoroutine(IKnockBack());
    }
}