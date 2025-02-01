using Animation;
using UnityEngine;
namespace Player
{
    public class PlayerAnimationTriggers : MonoBehaviour, IAnimationTrigger
    {
        Player player => GetComponentInParent<Player>();

        public void AnimationFinishTrigger()
        {
            player.AnimationFinishTriger();
        }
        public void AttackAnimationTrigger()
        {
            Collider2D[] hitTargets = Physics2D.OverlapCircleAll(transform.position, player.attackCheckRadius, player.whatIsEnemy);
            foreach(var target in hitTargets)
            {
                var targetInfo = target.GetComponent<IDamgeable>();
                if(targetInfo != null)
                {
                    targetInfo.TakeDamage(1);
                }
            }
        }
    }
}
