using System;
using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
    public class BlackHoleAbilityController : MonoBehaviour
    {
        [SerializeField] private BlackHoleHotkeyController hotkeyControllerPrefab;
        [SerializeField] private List<KeyCode> keyCodeList;
        public float maxSize;
        public float growthRate;
        public bool isGrowing;
        public List<Transform> affectedObjects;
        private void Update()
        {
            if (isGrowing)
            {
                transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growthRate * Time.deltaTime);
            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            var enemyInfo = other.GetComponent<Enemy.Enemy>();
            if( enemyInfo != null)
            {
                enemyInfo.FreezeTimer(true);
                BlackHoleHotkeyController hotkeyController = Instantiate(hotkeyControllerPrefab, other.transform);
                KeyCode randomKeyCode = keyCodeList[UnityEngine.Random.Range(0, keyCodeList.Count)];
                hotkeyController.SetupHotkey(randomKeyCode);
                keyCodeList.Remove(randomKeyCode);
            }
        }
    }
}
