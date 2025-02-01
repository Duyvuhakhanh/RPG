using Player.State;
using UnityEngine;
namespace Enemy
{
    public class EnemyBaseState : BaseState
    {
        protected Enemy enemy;
        protected EnemyStateMachine stateMachine;
    
        public EnemyBaseState(Enemy enemy, EnemyStateMachine stateMachine,Animator animator, string animationKey)
        {
            this.enemy = enemy;
            this.stateMachine = stateMachine;
            base.animator = animator;
            this.hashKey = Animator.StringToHash(animationKey);
        }

    }
}