using System;
using UnityEngine;

namespace RedboonTestProject.Store
{
    public class HandHandler : MonoBehaviour, IHandler<Hand>
    {
        public event Action OnHandableObjectInitialized;

        public Hand HandableObject { get; private set; }

        private void Awake()
        {
            HandableObject = new Hand();
            OnHandableObjectInitialized?.Invoke();
        }

        private void Update()
        {
            HandableObject.TryDrag();
        }
    }
}
