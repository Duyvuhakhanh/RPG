using UnityEngine;
public class PlayerAnimationTriggers : MonoBehaviour
{
    public void AnimationTrigger()
    {
        Player player = GetComponentInParent<Player>();
        player.AnimationTrigger();
    }
}
