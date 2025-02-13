using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Abilities
{
    public class BlackHoleAbilityController : MonoBehaviour
    {
        [SerializeField] private List<KeyCode> keyCodeList; 
        public float maxSize;
        public float growthRate;
        public bool isGrowing;
        public List<Transform> affectedObjects;

        private bool canActtack;
        public int amountOfAttacks = 4;
        public float cloneAttackCooldown = 0.3f;
        private float cloneAttackTimer;
        private void Update()
        {
            cloneAttackTimer -= Time.deltaTime;
            
            if (Input.GetKeyDown(KeyCode.A))
                canActtack = true;
            if (cloneAttackTimer < 0 && canActtack)
            {
                cloneAttackTimer = cloneAttackCooldown;
                int randomIndex = Random.Range(0, affectedObjects.Count);
                float xOffset;
                if (Random.Range(0, 100) > 50)
                {
                    xOffset = 2;
                }
                else
                {
                    xOffset = -2;
                }
                AbilityManager.instance.cloneAbility.CreateClone(affectedObjects[randomIndex], new Vector3(xOffset, 0, 0));
                amountOfAttacks--;
                if (amountOfAttacks <= 0)
                {
                    canActtack = false;
                }
            }
            
            
            if (isGrowing)
            {
                transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growthRate * Time.deltaTime);
            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            Enemy.Enemy enemyInfo = other.GetComponent<Enemy.Enemy>();
            if (enemyInfo == null) return;
            enemyInfo.FreezeTimer(true);
            affectedObjects.Add(other.transform);
        }
    }
}
