using System.Collections;
using UnityEngine;
namespace Enemy
{
    public class Enemy : Enity
    {
        [SerializeField] protected float playerCheckRange;
        public EnemyStateMachine enemyStateMachine { get; private set; }
        [Header("Move Settings")]
        public float moveSpeed;

        public float idleTime;
        [Header("Attack Info")]
        public LayerMask whatIsPlayer;
        [Header("Stunned Info")]
        public float stunTime = 1f;
        public Vector2 stunKnockBackDirection;
        protected bool canBeStunned;
        [SerializeField] protected GameObject counterImage;
        protected float awakeSpeed;
        protected override void Awake()
        {
            base.Awake();
            enemyStateMachine = new EnemyStateMachine();
            awakeSpeed = moveSpeed;
        }
        protected override void Update()
        {
            base.Update();
            enemyStateMachine.Update();

        }
        public virtual void FreezeTimer(bool timeFrozen)
        {
            moveSpeed = timeFrozen ? 0 : awakeSpeed;
            animator.speed = timeFrozen ? 0 : 1;
        }
        protected virtual IEnumerator IFrozenTimer(float time)
        {
            FreezeTimer(true);
            yield return new WaitForSeconds(time);
            FreezeTimer(false);
        }
        public void DoFreezeTimer(float time)
        {
            StartCoroutine(IFrozenTimer(time));
        }
        #region Counter Attack Window

        public virtual void OpenCounterAttackWindow()
        {
            canBeStunned = true;
            counterImage.SetActive(true);
        }
        public virtual void CloseCounterAttackWindow()
        {
            canBeStunned = false;
            counterImage.SetActive(false);
        }

        #endregion
       
        public override void AnimationFinishTriger()
        {
            enemyStateMachine.CurrentState.AnimationTrigger();
        }
        public virtual RaycastHit2D IsPlayerInSight()
        {
            return Physics2D.Raycast(transform.position, Vector2.right * faceDir, playerCheckRange, whatIsPlayer);
        }
        public virtual bool CanBeStunned()
        {
            if (canBeStunned)
            {
                CloseCounterAttackWindow();
                return true;
            }
            return false;
        }
        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackCheckRadius * faceDir, transform.position.y, transform.position.z));
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + playerCheckRange* faceDir, transform.position.y, transform.position.z));
        }
    }
}