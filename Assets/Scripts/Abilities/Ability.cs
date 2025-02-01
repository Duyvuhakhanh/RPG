using UnityEngine;

namespace Abilities
{
    public abstract class Ability : MonoBehaviour
    {
        [SerializeField] protected float cooldown;
        protected float coolDownTimer;
        public virtual void Update()
        {
            coolDownTimer -= Time.deltaTime;
        }
        public bool CheckAndUseAbility()
        {
            if (coolDownTimer <= 0)
            {
                UseAbility();
                coolDownTimer = cooldown;
                return true;
            }
            return false;
        }
        public virtual void UseAbility()
        {
            Debug.Log("Ability Used");
        }
    }
}
