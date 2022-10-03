using UnityEngine;

namespace RedboonTestProject.Pathfinding
{
    [CreateAssetMenu(fileName = "Grid Data", menuName = "Grid Data", order = 52)]
    public class GridData : ScriptableObject
    {
        [SerializeField] private Edge[] _edges;
        public Edge[] Edges => _edges;
    }
}
