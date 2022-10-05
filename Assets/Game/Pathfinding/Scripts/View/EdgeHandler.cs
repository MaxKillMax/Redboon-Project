using System;
using UnityEngine;

namespace RedboonTestProject.Pathfinding
{
    public class EdgeHandler : MonoBehaviour, IHandler<Edge>
    {
        public event Action OnHandableObjectInitialized;

        [SerializeField] private Edge _edge;
        public Edge HandableObject => _edge;

        private void Awake()
        {
            OnHandableObjectInitialized?.Invoke();
        }
    }
}
