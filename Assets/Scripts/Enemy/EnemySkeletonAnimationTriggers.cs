using UnityEngine;
public class EnemySkeletonAnimationTriggers : MonoBehaviour
{
    protected Enemy_Skeleton skeleton => GetComponentInParent<Enemy_Skeleton>();
    private void AnimationFinishTrigger()
    {
        skeleton.AnimationFinishTriger();
    }
}
