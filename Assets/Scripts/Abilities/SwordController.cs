using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Abilities
{

    public class SwordController : MonoBehaviour
    {
        [Header("Bounce Info")] private bool isBouncing;
        private int amountOfBounces = 3;
        private float bounceSpeed = 20f;
        private float bounceRange = 10f;
        private float returnSpeed = 5f;
        [Header("Pierce Info")] private int amountOfPierces;
        [Header("Spin Info")] private float maxTravelDistance;
        private float spinDuration;
        private bool isSpinning;
        private float spinTimer;
        private bool wasStopped;

        private float hitTimer;
        private float hitCooldown = 0.1f;

        private List<Enemy.Enemy> enemiesTarget = new List<Enemy.Enemy>();

        private readonly int hashFlip = Animator.StringToHash("Flip");

        private Rigidbody2D rb;
        private CircleCollider2D circleCollider2D;
        private Animator animator;
        private ICaster caster;
        private bool canRotate;
        private bool isReturning;
        private float freezeTime;
        private int targetIndex = 0;
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();

            circleCollider2D = GetComponent<CircleCollider2D>();
            animator = GetComponentInChildren<Animator>();
        }
        public void SetupBounce(bool isBouncing, int amountOfBounces, float bounceSpeed, float bounceRange)
        {
            this.amountOfBounces = amountOfBounces;
            this.isBouncing = isBouncing;
            this.bounceSpeed = bounceSpeed;
            this.bounceRange = bounceRange;
        }
        public void SetupSpin(bool isSpinning, float maxTravelDistance, float spinDuration, float hitCooldown)
        {
            this.maxTravelDistance = maxTravelDistance;
            this.spinDuration = spinDuration;
            this.isSpinning = isSpinning;
            this.hitCooldown = hitCooldown;
        }
        public void SetupSword(Vector2 finalDir, float swordGravity, ICaster caster, float freezeTime, float returnSpeed)
        {
            this.caster = caster;
            this.freezeTime = freezeTime;
            this.returnSpeed = returnSpeed;
            rb.velocity = finalDir;
            rb.gravityScale = swordGravity;
            canRotate = true;

            animator.SetBool(hashFlip, amountOfPierces <= 0);
        }
        public void SetupPierce(int amountOfPierces)
        {
            this.amountOfPierces = amountOfPierces;
        }
        private void Update()
        {
            if (canRotate)
            {
                transform.right = rb.velocity;
            }
            if (isReturning)
            {
                transform.position =
                    Vector2.MoveTowards(transform.position, caster.GetTransform().position, returnSpeed * Time.deltaTime);
                CheckSwordReturn();
            }
            BounceLogic();
            SpinLogic();
        }
        private void BounceLogic()
        {

            if (isBouncing && enemiesTarget.Count > 0)
            {
                BounceToTarget(targetIndex);
                var distanceToCollide = 0.1f;
                if (Vector2.Distance(transform.position, enemiesTarget[targetIndex].transform.position) < distanceToCollide)
                {
                    OnBounce();
                    UpdateTargetIndex();
                }
            }
        }
        private void OnBounce()
        {
            SwordDamageEnemmy(enemiesTarget[targetIndex]);
            amountOfBounces--;
            if (amountOfBounces <= 0)
            {
                isBouncing = false;
                ReturnSword();
            }
        }
        private void UpdateTargetIndex()
        {

            targetIndex++;
            if (targetIndex >= enemiesTarget.Count)
            {
                targetIndex = 0;

            }
        }
        private void BounceToTarget(int targetIndex)

        {
            transform.position = Vector2.MoveTowards(transform.position,
                                                     enemiesTarget[targetIndex].transform.position,
                                                     bounceSpeed * Time.deltaTime);
        }
        private void CheckSwordReturn()
        {
            var minDistance = 0.1f;
            if (Vector2.Distance(transform.position, caster.GetTransform().position) < minDistance)
            {
                isReturning = false;
                caster.GetType<Player.Player>().CatchSword();
            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (isReturning) return;
            var enemy = other.GetComponent<Enemy.Enemy>();
            if (enemy != null)
            {
                SwordDamageEnemmy(enemy);
                if (isBouncing && enemiesTarget.Count <= 0)
                {
                    var colliders = Physics2D.OverlapCircleAll(transform.position, bounceRange);
                    foreach(Collider2D hit in colliders)
                    {
                        var nextEnemy = hit.GetComponent<Enemy.Enemy>();
                        if (nextEnemy != null)
                        {
                            enemiesTarget.Add(nextEnemy);
                        }
                    }
                }
            }

            StuckInto(other);

        }
        private void SwordDamageEnemmy(Enemy.Enemy enemy)
        {

            enemy.TakeDamage(1);
            enemy.DoFreezeTimer(freezeTime);
        }
        private void StuckInto(Collider2D collision)
        {
            if (amountOfPierces > 0 && collision.GetComponent<Enemy.Enemy>() != null)
            {
                amountOfPierces--;
                return;
            }
            if (isSpinning)
            {
                StopSpinning();
                return;
            }
            canRotate = false;
            rb.isKinematic = true;
            circleCollider2D.enabled = false;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

            if (isBouncing && enemiesTarget.Count > 0) return;
            animator.SetBool(hashFlip, false);
            transform.parent = collision.transform;
        }

        public void ReturnSword()
        {
            isReturning = true;
            rb.isKinematic = false;
            transform.parent = null;
        }
        private void DestroyMe()
        {
            Destroy(gameObject);
        }

        private void SpinLogic()
        {
            if (isSpinning)
            {
                if (Vector2.Distance(transform.position, caster.GetTransform().position) > maxTravelDistance && !wasStopped)
                {
                    StopSpinning();
                }
                if (wasStopped)
                {
                    spinTimer -= Time.deltaTime;
                    if (spinTimer <= 0)
                    {
                        isReturning = true;
                        isSpinning = false;
                    }
                    hitTimer -= Time.deltaTime;
                    if (hitTimer <= 0)
                    {
                        hitTimer = hitCooldown;
                        var radius = 1f;
                        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(transform.position, radius);
                        foreach(var target in hitTargets)
                        {
                            var enemy = target.GetComponent<Enemy.Enemy>();
                            if (enemy != null)
                            {
                                SwordDamageEnemmy(enemy);
                            }
                        }
                    }
                }
            }
        }
        private void StopSpinning()
        {

            wasStopped = true;
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            spinTimer = spinDuration;
        }
    }

}
