using UnityEngine;

namespace Abilities
{
    public class CloneAbility : Ability
    {
        [SerializeField] private float cloneDuration;
        [SerializeField] private CloneController clonePrefab;
        public void CreateClone(Transform transform)
        {
            var clone = Instantiate(clonePrefab);
            clone.SetUpClone(transform, cloneDuration);
            clone.gameObject.SetActive(true);
        }
    }
}
