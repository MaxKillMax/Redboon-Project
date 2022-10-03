using UnityEngine;

namespace RedboonTestProject.Pathfinding
{
    public class DotFactory : MonoBehaviour, IMonoBehaviourFactory<Dot>
    {
        [SerializeField] private Dot _dotPrefab;
        [SerializeField] private Transform _parent;

        public Dot CreateObject()
        {
            Dot dot = Instantiate(_dotPrefab, _parent);
            return dot;
        }
    }
}
