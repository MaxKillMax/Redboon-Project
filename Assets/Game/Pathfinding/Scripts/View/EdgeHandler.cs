using UnityEngine;

namespace RedboonTestProject.Pathfinding
{
    public class EdgeHandler : MonoBehaviour, IHandler
    {
        [SerializeField] private Edge _edge;
        public Edge Edge => _edge;
    }
}
