using UnityEngine;

namespace RedboonTestProject.Pathfinding
{
    public class DotFactory : MonoBehaviour, IMonoBehaviourFactory<Dot>
    {
        [SerializeField] private Dot _prefab;
        [SerializeField] private Transform _parent;

        public Dot CreateObject()
        {
            Dot dot = Instantiate(_prefab, _parent);
            return dot;
        }
    }
}
