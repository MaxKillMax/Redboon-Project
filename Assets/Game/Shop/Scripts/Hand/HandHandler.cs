using UnityEngine;

namespace RedboonTestProject.Store
{
    public class HandHandler : MonoBehaviour, IHandler
    {
        public Hand Hand { get; private set; } = new(Camera.main);

        private void Update()
        {
            Hand.TryDrag();
        }
    }
}
