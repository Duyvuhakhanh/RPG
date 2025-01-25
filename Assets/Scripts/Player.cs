using System;
using UnityEngine;
public class Player : MonoBehaviour
{

    #region Compoment

    private Animator playerAnimator;
    public Rigidbody2D rb { get; private set; }
    public float dashDir { get; private set; }
    public int faceDir => faceRight ? 1 : -1;

    #endregion


    [Header("Player Settings")] public float speed = 5;
    public float jumpForce = 12;
    public float jumpWallForce = 3;
    public float jumpWallTime = 0.4f;
    public float dashForce = 20;
    public float dashTime = 0.2f;

    [Header("Collision Info")] [SerializeField]
    private float groundCheckDistance;

    [SerializeField] private float wallCheckDistance;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsWall;

    [HideInInspector] public bool faceRight = true;

    #region State

    private StateMachine stateMachine;
    public IdleState IdleState;
    public MoveState MoveState;
    public JumpState JumpState;
    public GroundedState GroundedState;
    public WallSlideState WallSlideState;
    public DashState DashState;
    public AirState AirState;
    public JumpWallState JumpWallState;

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
        IdleState = new IdleState(this, stateMachine, playerAnimator, AnimationKeys.Idle);
        MoveState = new MoveState(this, stateMachine, playerAnimator, AnimationKeys.Move);
        JumpState = new JumpState(this, stateMachine, playerAnimator, AnimationKeys.Jump);
        AirState = new AirState(this, stateMachine, playerAnimator, AnimationKeys.Fall);
        GroundedState = new GroundedState(this, stateMachine, playerAnimator, AnimationKeys.Idle);
        DashState = new DashState(this, stateMachine, playerAnimator, AnimationKeys.Dash);
        WallSlideState = new WallSlideState(this, stateMachine, playerAnimator, AnimationKeys.WallSlide);
        JumpWallState = new JumpWallState(this, stateMachine, playerAnimator, AnimationKeys.Jump);
        // // Inject Transitions
        // stateMachine.InjectTransition(IdleState, MoveState, new FuncPredicate(() => xInput != 0));
        // stateMachine.InjectTransition(IdleState, JumpState, new FuncPredicate(() => yInput > 0));
        // stateMachine.InjectTransition(MoveState, JumpState, new FuncPredicate(() => yInput > 0));
        // stateMachine.InjectTransition(MoveState, FallState, new FuncPredicate(() => rb.velocity.y < 0));
        // stateMachine.InjectTransition(JumpState, FallState, new FuncPredicate(() => rb.velocity.y < 0));
        // stateMachine.InjectTransition(JumpState, WallSlideState, new FuncPredicate(() => IsWallDetected() && xInput * faceDir > 0));
        // stateMachine.InjectTransition(FallState, IdleState, new FuncPredicate(() => IsOnGround() && rb.velocity == Vector2.zero &&  xInput == 0));
        // stateMachine.InjectTransition(FallState, MoveState, new FuncPredicate(() => IsOnGround() && xInput != 0 && rb.velocity.y == 0));
        // stateMachine.InjectTransition(FallState, WallSlideState, new FuncPredicate(() => IsWallDetected() && xInput * faceDir > 0));
        // stateMachine.InjectTransition(WallSlideState, FallState, new FuncPredicate(() => xInput * faceDir == 0));
        // stateMachine.InjectTransition(WallSlideState, JumpWallState, new FuncPredicate(() => yInput > 0));
        // stateMachine.InjectTransition(JumpWallState, WallSlideState, new FuncPredicate(() => IsWallDetected() && xInput * faceDir > 0));
        //
        //
        // //stateMachine.InjectTransition(idleState, dashState, new FuncPredicate(() => Input.GetKeyDown(KeyCode.LeftShift)));
        //
        // stateMachine.AddAnyTransition(IdleState,
        //                               new FuncPredicate(() => xInput == 0 && yInput == 0 && rb.velocity == Vector2.zero));
        // stateMachine.AddAnyTransition(DashState, new FuncPredicate(() => Input.GetKeyDown(KeyCode.LeftShift)));
        //Initial State
        stateMachine.SetState(IdleState);
    }
    private void Update()
    {
        stateMachine.Update();
        UpdateDirection();
        CheckDashInput();
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }
    private void CheckDashInput()
    {
        if(IsWallDetected()) return;
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir == 0)
                dashDir = faceDir;
            stateMachine.ChangeState(DashState);
        }
    }
    private void UpdateDirection()
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
    private void Flip()
    {
        faceRight = !faceRight;
        transform.Rotate(0, 180, 0);
        //Debug.Break();
    }
    public bool IsOnGround()
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    }
    public bool IsWallDetected()
    {
        return Physics2D.Raycast(wallCheck.position, faceRight ? Vector2.right : Vector2.left, wallCheckDistance, whatIsWall);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
    public void SetVelocity(Vector2 vector2)
    {
        rb.velocity = vector2;
    }
}
