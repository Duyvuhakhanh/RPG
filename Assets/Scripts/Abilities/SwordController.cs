using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Abilities
{
    public class SwordController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        private void Awake()
        {
            if(rb == null)
                rb = GetComponent<Rigidbody2D>();
        }
        public void SetupSword(Vector2 launchDir, float swordGravity)
        {
            rb.velocity = launchDir;
            rb.gravityScale = swordGravity;
        }
    }
}
