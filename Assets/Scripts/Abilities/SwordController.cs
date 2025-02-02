using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Abilities
{
    public class SwordController : MonoBehaviour
    {
        [SerializeField] private float returnSpeed;
        private readonly int hashFlip = Animator.StringToHash("Flip");
        
        private Rigidbody2D rb;
        private CircleCollider2D circleCollider2D;
        private Animator animator;
        private ICaster caster;
        private bool canRotate;
        private bool isReturning;
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();

            circleCollider2D = GetComponent<CircleCollider2D>();
            animator = GetComponentInChildren<Animator>();
        }
        private void Update()
        {
            if (canRotate)
            {
                transform.right = rb.velocity;
            }
            if (isReturning)
            {
                transform.position = Vector2.MoveTowards(transform.position, caster.GetTransform().position, returnSpeed * Time.deltaTime);
                CheckSwordReturn();
            }
        }
        private void CheckSwordReturn()
        {
            var minDistance = 0.1f;
            if(Vector2.Distance(transform.position, caster.GetTransform().position) < minDistance)
            {
                isReturning = false;
                caster.GetType<Player.Player>().CatchSword();
            }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            canRotate = false;
            animator.SetBool(hashFlip, false);
            rb.isKinematic = true;
            circleCollider2D.enabled = false;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            transform.parent = collision.transform;
            
        }

        public void ReturnSword()
        {
            isReturning = true;
            rb.isKinematic = false;
            transform.parent = null;
        }
        public void SetupSword(Vector2 finalDir, float swordGravity, ICaster caster)
        {
            this.caster = caster;
            rb.velocity = finalDir;
            rb.gravityScale = swordGravity;
            canRotate = true;
            animator.SetBool(hashFlip, true);
        }
    }
}
