using UnityEngine;

namespace Abilities
{
    public class AbilityManager : MonoBehaviour
    {
        //singleton
        public static AbilityManager instance;
        public DashAbility dashAbility { get; private set; }
        public CloneAbility cloneAbility { get; private set; }
        public SwordAbility swordAbility { get; private set; }
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        private void Start()
        {
            dashAbility = GetComponentInChildren<DashAbility>();
            cloneAbility = GetComponentInChildren<CloneAbility>();
            swordAbility = GetComponentInChildren<SwordAbility>();
        }

    }
}
