using UnityEngine;

namespace RedboonTestProject.Store
{
    public class HandHandler : MonoBehaviour, IHandler
    {
        public Hand Hand { get; private set; }

        private void Awake()
        {
            Hand = new Hand(Camera.main);
        }

        private void Update()
        {
            Hand.TryDrag();
        }
    }
}
