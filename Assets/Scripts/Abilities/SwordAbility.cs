using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Abilities
{
    public enum SwordType
    {
        Regular,
        Bounce,
        Pierce,
        Spin
    }
    public class SwordAbility : Ability
    {
        public SwordType swordType;

        [Header("Bounce Info")] [SerializeField]
        private int amountOfBounces = 5;
        [SerializeField] private float bounceSpeed = 20f;
        [SerializeField] private float bounceRange = 10f;

        [SerializeField] private float bounceGravity = 5f;

        [Header("Pierce Info")] [SerializeField]
        private int amountOfPierces = 5;

        [SerializeField] private float pierceGravity = .5f;

        [Header("Spin Info")] [SerializeField] private float maxTravelDistance = 7;
        [SerializeField] private float spinDuration = 2;
        [SerializeField] private float spinGravity = 1;
        [SerializeField] private float hitCooldown = 0.35f;

        [Header("Skill info")] 
        [SerializeField]
        private SwordController swordPrefab;

        [SerializeField] private float returnSpeed = 5f;
        [SerializeField] private float freezeTime = 1;


        [FormerlySerializedAs("launchDir")] [SerializeField]
        private Vector2 launchForce;

        [SerializeField] private float swordGravity;
        private void Start()
        {
            SetupGravity();
        }
        private void SetupGravity()
        {
            swordGravity = swordType switch
            {
                SwordType.Bounce => bounceGravity,
                SwordType.Pierce => pierceGravity,
                _ => swordGravity
            };
        }
        public override void UseAbility(ICaster _caster)
        {
            var casterTrans = _caster.GetTransform();
            var sword = Instantiate(swordPrefab, casterTrans.position, Quaternion.identity);
            switch (swordType)
            {
                case SwordType.Bounce:
                    swordGravity = bounceGravity;
                    sword.SetupBounce(true, amountOfBounces, bounceRange, bounceSpeed);
                    break;
                case SwordType.Pierce:
                    swordGravity = pierceGravity;
                    sword.SetupPierce(amountOfPierces);
                    break;
                case SwordType.Spin:
                    swordGravity = spinGravity;
                    sword.SetupSpin(true, maxTravelDistance, spinDuration, hitCooldown);
                    break;
            }
            var player = _caster.GetType<Player.Player>();
            player.AssignSword(sword);
            var finalDir = GetFinalDir(casterTrans);
            sword.SetupSword(finalDir, swordGravity, _caster, freezeTime, returnSpeed);
        }
        private Vector2 GetFinalDir(Transform casterTrans)
        {

            Vector2 playerPos = casterTrans.position;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var aimDir = (mousePos - playerPos).normalized;
            return new Vector2(aimDir.x * launchForce.x, aimDir.y * launchForce.y);
        }
        public (Vector2 launchForce, float swordGravity) GetAbilityInfo()
        {
            return (launchForce, swordGravity);
        }


    }
}
