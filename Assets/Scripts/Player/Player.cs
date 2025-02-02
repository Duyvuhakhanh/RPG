using System.Collections;
using Abilities;
using Animation;
using Player.State;
using UnityEngine;
namespace Player
{
    [RequireComponent(typeof(PlayerVisualizeSwordAbility))]
    public class Player : Enity, ICaster
    {

        protected PlayerStateMachine PlayerStateMachine;
        public PlayerVisualizeSwordAbility playerVisualizeSwordAbility { get; private set; }
        public SwordController sword;
        [Header("Attack Info")] 
        public Vector2[] attackMovement;

        public float counterAttackDuration = 1;

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
        public CounterAttackState CounterAttackState;
        public AimSwordState AimSwordState;
        public CatchSwordState CatchSwordState;

        #endregion

        protected override void Awake()
        {
            base.Awake();
            playerVisualizeSwordAbility = GetComponent<PlayerVisualizeSwordAbility>();
        }
        private void Start()
        {
            // Define States
            PlayerStateMachine = new PlayerStateMachine();
            IdleState = new IdleState(this, PlayerStateMachine, animator, AnimationKeys.Idle);
            MoveState = new MoveState(this, PlayerStateMachine, animator, AnimationKeys.Move);
            JumpState = new JumpState(this, PlayerStateMachine, animator, AnimationKeys.Jump);
            AirState = new AirState(this, PlayerStateMachine, animator, AnimationKeys.Fall);
            GroundedState = new GroundedState(this, PlayerStateMachine, animator, AnimationKeys.Idle);
            DashState = new DashState(this, PlayerStateMachine, animator, AnimationKeys.Dash);
            WallSlideState = new WallSlideState(this, PlayerStateMachine, animator, AnimationKeys.WallSlide);
            JumpWallState = new JumpWallState(this, PlayerStateMachine, animator, AnimationKeys.Jump);
            PrimeAttackState = new PrimeAttackState(this, PlayerStateMachine, animator, AnimationKeys.PrimeAttack);
            CounterAttackState = new CounterAttackState(this, PlayerStateMachine, animator, AnimationKeys.CounterAttack);
            AimSwordState = new AimSwordState(this, PlayerStateMachine, animator, AnimationKeys.AimSword);
            CatchSwordState = new CatchSwordState(this, PlayerStateMachine, animator, AnimationKeys.CatchSword);
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
            if (Input.GetKeyDown(KeyCode.LeftShift) && AbilityManager.instance.dashAbility.CheckAndUseAbility(this))
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

        public void AssignSword(SwordController sword)
        {
            this.sword = sword;
        }
        public void CatchSword()
        {
            PlayerStateMachine.ChangeState(CatchSwordState);
            ClearSword();
        }
        private void ClearSword()
        {

            Destroy(sword.gameObject);
            sword = null;
        }
        public Transform GetTransform() => this.transform;
        public Rigidbody2D GetRigidbody() => this.rb;
        public global::Player.Player GetType<Player>()
        {
            return this ;
        }
        public void ThrowSwordAnimationTrigger()
        {
            AbilityManager.instance.swordAbility.CheckAndUseAbility(this);
        }
    }
}