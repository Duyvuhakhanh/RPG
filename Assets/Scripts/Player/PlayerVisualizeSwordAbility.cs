using System;
using Abilities;
using UnityEngine;

namespace Player
{
    public class PlayerVisualizeSwordAbility : MonoBehaviour
    {
        [SerializeField] private Player player;

        [SerializeField] GameObject aimDotPrefab;
        [SerializeField] private int numberOfAimDots;
        [SerializeField] private float aimDotDistance;
        private Vector2 lauchForce;
        private float swordGravity;
        private GameObject[] aimDots;
        private Camera mainCamera;
        private bool isAiming;
        private void Start()
        {
            mainCamera = Camera.main;
        }
        private void GetInfo()
        {
            var swordAbilityInfo = AbilityManager.instance.swordAbility.GetAbilityInfo();
            lauchForce = swordAbilityInfo.launchForce;
            swordGravity = swordAbilityInfo.swordGravity;
        }
        public void OnEnterAimState()
        {
            GetInfo();
            GenerateAimDots();
            ActiveDots(true);
            isAiming = true;
        }
        public void OnExitAimState()
        {
            ActiveDots(false);
            isAiming = false;
        }
        public void GenerateAimDots()
        {
            if(aimDots != null) return;
            //generate aim dots
            aimDots = new GameObject[numberOfAimDots];
            for (int i = 0; i < numberOfAimDots; i++)
            {
                aimDots[i] = Instantiate(aimDotPrefab, transform.position, Quaternion.identity);
                aimDots[i].SetActive(false);
            }
        }
        public void ActiveDots(bool active)
        {
            foreach(var aimDot in aimDots)
            {
                aimDot.SetActive(active);
            }
        }
        private Vector2 AimDirection()
        {
            Vector2 playerPos = player.GetTransform().position;
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            return (mousePos - playerPos).normalized;
        }
        private Vector2 GetDotPosition(float t)
        {
            Vector2 playerPos = player.GetTransform().position;
            Vector2 aimDir = new Vector2(AimDirection().x * lauchForce.x, AimDirection().y * lauchForce.y);
            Vector2 dotPos = playerPos + aimDir * t  + Physics2D.gravity * (0.5f * swordGravity * t * t); // v = v0 + at^2/2
            return dotPos;
        }
        private void Update()
        {
            if(!isAiming) return;
            for (int i = 0; i < numberOfAimDots; i++)
            {
                aimDots[i].transform.position = GetDotPosition(i * aimDotDistance);
            }
        }
    }
}
