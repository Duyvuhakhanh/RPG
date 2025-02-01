using UnityEngine;
namespace Player.State
{
    public class PlayerBaseState : BaseState
    {
        protected Player player;
        protected float xInput;
        protected float yInput;
        [Header("Attack Details")] 
        public float counterAttackDuration = 0.5f;
    
        public PlayerBaseState(Player player, PlayerStateMachine playerStateMachine, Animator animator, string animationKey
        )
        {
            this.PlayerStateMachine = playerStateMachine;
            this.player = player;
            this.animator = animator;
            hashKey = Animator.StringToHash(animationKey);

        }

        public override void Update()
        {
            base.Update();
            xInput = Input.GetAxis("Horizontal");
            yInput = Input.GetAxis("Vertical");


        }
 

    }
}