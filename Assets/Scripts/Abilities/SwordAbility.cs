using UnityEngine;
using UnityEngine.Serialization;

namespace Abilities
{
    public class SwordAbility : Ability
    {
        [Header("Skill info")] [SerializeField]
        private SwordController swordPrefab;
        private Vector2 finalDir;

        [FormerlySerializedAs("launchDir")] [SerializeField] private Vector2 launchForce;
        [SerializeField] private float swordGravity;
        public override void UseAbility(ICaster _caster)
        {
            var casterTrans = _caster.GetTransform();
            var sword = Instantiate(swordPrefab, casterTrans.position, Quaternion.identity);
            
            Vector2 playerPos = casterTrans.position;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
            var aimDir = (mousePos - playerPos).normalized;
            finalDir = new Vector2(aimDir.x * launchForce.x, aimDir.y * launchForce.y);
            var player = _caster.GetType<Player.Player>();
            player.AssignSword(sword);
            sword.SetupSword(finalDir, swordGravity, _caster);
        }
        public (Vector2 launchForce, float swordGravity) GetAbilityInfo()
        {
            return (launchForce, swordGravity);
        } 
        

    }
}
