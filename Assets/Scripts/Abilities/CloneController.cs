using System;
using Animation;
using UnityEngine;

namespace Abilities
{
    public class CloneController : MonoBehaviour
    {
        private float cloneTimer;
        [SerializeField] private Transform attackCheck;
        [SerializeField] private float attackCheckRadius;
        [SerializeField] private LayerMask whatIsEnemy;
        private Animator animator;
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
        private void Update()
        {
            cloneTimer -= Time.deltaTime;
            if (cloneTimer <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        public void SetUpClone(Transform clonePos, float cloneDuration, Vector3 offset)
        {
            animator.SetBool(AnimationKeys.Attack, true);
            this.transform.position = clonePos.position + offset;
            this.transform.rotation = clonePos.rotation;
            cloneTimer = cloneDuration;
        }
        public void AnimationFinishTrigger()
        {
            cloneTimer = 0;
        }
        public void AttackAnimationTrigger()
        {
            Collider2D[] hitTargets = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius, whatIsEnemy);
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
