using UnityEngine;

namespace Abilities
{
    public abstract class Ability : MonoBehaviour, IAbility
    {
        [SerializeField] protected float cooldown;
        protected float coolDownTimer;
        public virtual void Update()
        {
            coolDownTimer -= Time.deltaTime;
        }
        public virtual bool CheckAndUseAbility(ICaster caster)
        {
            if (coolDownTimer <= 0)
            {
                UseAbility(caster);
                coolDownTimer = cooldown;
                return true;
            }
            return false;
        }
        public virtual void UseAbility(ICaster caster)
        {
            
        }
    }
}
