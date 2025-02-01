using UnityEngine;

namespace Abilities
{
    public class SwordAbility : Ability
    {
        [Header("Skill info")]
        [SerializeField] protected GameObject swordPrefab;
        public override void UseAbility()
        {
            Debug.Log("Sword Ability Used");
        }
    }
}
