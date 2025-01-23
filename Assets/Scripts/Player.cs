using System;
using UnityEngine;
public class Player : MonoBehaviour
{

    #region Compoment

    private Animator playerAnimator;
    public Rigidbody2D rb { get; private set; }
    public float xInput { get; private set; }
    public float yInput { get; private set; }

    #endregion


    [Header("Player Settings")]
    public float speed = 5;
    public float jumpForce = 12;
    [Header("Collision Info")]
    [SerializeField] private float groundCheckDistance ;
    [SerializeField] private float wallCheckDistance ;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask whatIsGround;

    private bool faceRight = true;
    #region State

    private StateMachine stateMachine;

    #endregion

    private void Awake()
    {
        if (playerAnimator == null) playerAnimator = GetComponentInChildren<Animator>();
        if (rb == null) rb = GetComponentInChildren<Rigidbody2D>();
    }
    private void Start()
    {
        stateMachine = new StateMachine();
        // Define States
        var idleState = new IdleState(this, playerAnimator, AnimationKeys.Idle);
        var moveState = new MoveState(this, playerAnimator, AnimationKeys.Move);
        var jumpState = new JumpState(this, playerAnimator, AnimationKeys.JumpFall);
        var groundedState = new GroundedState(this, playerAnimator, AnimationKeys.Idle);
        // Inject Transitions
        stateMachine.InjectTransition(idleState, moveState, new FuncPredicate(() => xInput != 0));
        stateMachine.InjectTransition(idleState, jumpState, new FuncPredicate(() => yInput != 0));
        stateMachine.InjectTransition(jumpState,idleState, new FuncPredicate(IsOnGround));
        stateMachine.AddAnyTransition(idleState, new FuncPredicate(() => xInput == 0 && yInput == 0 && rb.velocity == Vector2.zero));
        //Initial State
        stateMachine.SetState(idleState);
    }
    private void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        playerAnimator.SetFloat(AnimationKeys.JumpFall_YVeclocity, rb.velocity.y);
        stateMachine.Update();
        UpdateDirection();
    }
    private void UpdateDirection()
    {
        if(rb.velocity.x > 0.01f && !faceRight)
        {
            Flip();
        }
        else if(rb.velocity.x < -0.01f && faceRight)
        {
            Flip();
        }
    }
    private void Flip()
    {
        faceRight = !faceRight;
        transform.Rotate(0, 180, 0);
    }
    public bool IsOnGround()
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y ));
    }
}