using System.Collections.Generic;
using UnityEngine;

namespace RedboonTestProject.Pathfinding
{
    public class WavePathFinder : IPathFinder
    {
        public IEnumerable<Vector2> GetPath(Vector2 A, Vector2 C, IEnumerable<Edge> edges)
        {
            return default;
        }

        private Vector3 GetNearestRectanglePoint(Vector3 worldPoint, IEnumerable<Edge> edges)
        {
            return default;
        }

        private IEnumerable<Edge> GetConnectedEdges(Vector3 rectanglePoint, IEnumerable<Edge> edges)
        {
            return default;
        }
    }
}
