using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class ParallaxBackground : MonoBehaviour
    {
        [SerializeField] private float parallaxEffectMultiplier;
        private float startPosX;
        private float legth;
        private void Start()
        {
            startPosX = transform.position.x;
            legth = GetComponent<SpriteRenderer>().bounds.size.x;
        }
        private void FixedUpdate()
        {
            float distanceToMove = Camera.main.transform.position.x * parallaxEffectMultiplier;
            transform.position = new Vector3(startPosX + distanceToMove, Camera.main.transform.position.y);
            var distaceMoved = Camera.main.transform.position.x * (1 - parallaxEffectMultiplier);
            if(distaceMoved > startPosX + legth) startPosX += legth;
            else if(distaceMoved < startPosX - legth) startPosX -= legth;
        }
    }
}
