using UnityEngine;

namespace Abilities
{
    public class SwordAbility : Ability
    {
        [Header("Skill info")] [SerializeField]
        private SwordController swordPrefab;

        [SerializeField] private Vector2 launchDir;
        [SerializeField] private float swordGravity;
        public override void UseAbility(ICaster caster)
        {
            var casterTrans = caster.GetTransform();
            var sword = Instantiate(swordPrefab, casterTrans.position, Quaternion.identity);
            sword.SetupSword(launchDir, swordGravity);
        }
    }
}
