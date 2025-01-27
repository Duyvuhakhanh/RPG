using UnityEngine;
using UnityEngine.Serialization;
public abstract class Enity : MonoBehaviour
{

    #region Compoment

    protected Animator animator;
    public Rigidbody2D rb { get; protected set; }
    public float dashDir { get; protected set; }
    public int faceDir => faceRight ? 1 : -1;
    public bool isBusy { get; protected set; }
    #endregion
    [Header("Collision Info")] 
    [SerializeField]
    protected float groundCheckDistance;

    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected Transform groundCheck;
    [FormerlySerializedAs("wallCheck")] [SerializeField] protected Transform horizontalCheck;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected LayerMask whatIsWall;

    [HideInInspector] public bool faceRight = true;
    #region Direction
    protected virtual void Awake()
    {
        if (animator == null) animator = GetComponentInChildren<Animator>();
        if (rb == null) rb = GetComponentInChildren<Rigidbody2D>();
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
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(horizontalCheck.position, new Vector2(horizontalCheck.position.x + wallCheckDistance, horizontalCheck.position.y));
    }
    #endregion
    #region Velocity

    public void SetVelocity(Vector2 vector2)
    {
        rb.velocity = vector2;
    }

    #endregion
    protected virtual void Update()
    {
        UpdateDirection();
    }
    protected virtual void FixedUpdate()
    {
    }

}