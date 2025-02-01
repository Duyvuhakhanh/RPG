using UnityEngine;
namespace Player.State
{
    public class PlayerState : PlayerBaseState
    {

        public PlayerState(Player player, PlayerStateMachine playerStateMachine, Animator animator, string animationKey) : base(player, playerStateMachine, animator, animationKey)
        {
        }
    }
}
