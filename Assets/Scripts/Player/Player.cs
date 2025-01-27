using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : Enity
{
    protected PlayerStateMachine PlayerStateMachine;

    [Header("Attack Info")] 
    public Vector2[] attackMovement;


    [Header("Player Settings")] 
    public float speed = 5;
    public float jumpForce = 12;
    public float jumpWallForce = 3;
    public float jumpWallTime = 0.4f;
    public float dashForce = 20;
    public float dashTime = 0.2f;
    public LayerMask whatIsEnemy;

    #region State
    public IdleState IdleState;
    public MoveState MoveState;
    public JumpState JumpState;
    public GroundedState GroundedState;
    public WallSlideState WallSlideState;
    public DashState DashState;
    public AirState AirState;
    public JumpWallState JumpWallState;
    public PrimeAttackState PrimeAttackState;

    #endregion


    private void Start()
    {
        PlayerStateMachine = new PlayerStateMachine();
        // Define States
        IdleState = new IdleState(this, PlayerStateMachine, animator, AnimationKeys.Idle);
        MoveState = new MoveState(this, PlayerStateMachine, animator, AnimationKeys.Move);
        JumpState = new JumpState(this, PlayerStateMachine, animator, AnimationKeys.Jump);
        AirState = new AirState(this, PlayerStateMachine, animator, AnimationKeys.Fall);
        GroundedState = new GroundedState(this, PlayerStateMachine, animator, AnimationKeys.Idle);
        DashState = new DashState(this, PlayerStateMachine, animator, AnimationKeys.Dash);
        WallSlideState = new WallSlideState(this, PlayerStateMachine, animator, AnimationKeys.WallSlide);
        JumpWallState = new JumpWallState(this, PlayerStateMachine, animator, AnimationKeys.Jump);
        PrimeAttackState = new PrimeAttackState(this, PlayerStateMachine, animator, AnimationKeys.PrimeAttack);
        PlayerStateMachine.SetState(IdleState);
    }
    protected override void Update()
    {
        base.Update();
        PlayerStateMachine.Update();
        CheckDashInput();

    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        PlayerStateMachine.FixedUpdate();

    }
    private void CheckDashInput()
    {
        if(IsWallDetected()) return;
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir == 0)
                dashDir = faceDir;
            PlayerStateMachine.ChangeState(DashState);
        }
    }
    public void LockAcitivity(float time)
    {
        StartCoroutine(ILockAcitivity(time));
    }
    IEnumerator ILockAcitivity(float time)
    {
        isBusy = true;
        yield return new WaitForSeconds(time);
        isBusy = false;
    }
    public override void AnimationFinishTriger()
    {
        PlayerStateMachine.CurrentState.AnimationTrigger();
    }


}