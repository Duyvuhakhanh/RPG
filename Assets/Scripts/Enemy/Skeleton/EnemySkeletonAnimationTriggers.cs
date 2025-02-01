using Animation;
using UnityEngine;
namespace Enemy.Skeleton
{
    public class EnemySkeletonAnimationTriggers : MonoBehaviour, IAnimationTrigger
    {
        protected Enemy_Skeleton skeleton => GetComponentInParent<Enemy_Skeleton>();
        public void AnimationFinishTrigger()
        {
            skeleton.AnimationFinishTriger();

        }
        public void AttackAnimationTrigger()
        {
            Collider2D[] hitTargets = Physics2D.OverlapCircleAll(transform.position, skeleton.attackCheckRadius, skeleton.whatIsPlayer);
            foreach(var target in hitTargets)
            {
                var targetInfo = target.GetComponent<IDamgeable>();
                if(targetInfo != null)
                {
                    targetInfo.TakeDamage(1);
                }
            }
        }
        protected void OpenCounterAttackWindow()
        {
            skeleton.OpenCounterAttackWindow();
        }
        protected void CloseCounterAttackWindow()
        {
            skeleton.CloseCounterAttackWindow();
        }
    }
}
