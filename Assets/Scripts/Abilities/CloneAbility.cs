using UnityEngine;

namespace Abilities
{
    public class CloneAbility : Ability
    {
        [SerializeField] private float cloneDuration;
        [SerializeField] private CloneController clonePrefab;
        public void CreateClone(Transform transform, Vector3 offset)
        {
            var clone = Instantiate(clonePrefab);
            clone.SetUpClone(transform, cloneDuration, offset);
            clone.gameObject.SetActive(true);
        }
    }
}
