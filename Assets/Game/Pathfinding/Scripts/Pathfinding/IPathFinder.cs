using System.Collections.Generic;
using UnityEngine;

namespace RedboonTestProject.Pathfinding
{
    public interface IPathFinder
    {
        public IEnumerable<Vector2> GetPath(Vector2 A, Vector2 C, IEnumerable<Edge> edges);
    }
}
