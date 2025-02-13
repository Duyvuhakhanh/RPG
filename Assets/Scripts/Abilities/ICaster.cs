using UnityEngine;

namespace Abilities
{
    public interface  ICaster
    {
        public Transform GetTransform();
        public Rigidbody2D GetRigidbody();
        public Player.Player GetType<T>();
    }
}
