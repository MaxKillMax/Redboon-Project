using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace RedboonTestProject.Pathfinding
{
    public class NewBehaviourScript : MonoBehaviour
    {
        
    }

    public struct Rectangle
    {
        public Vector2 Min;
        public Vector2 Max;
    }

    public struct Edge
    {
        public Rectangle First;
        public Rectangle Second;
        public Vector3 Start;
        public Vector3 End;
    }

    public interface IPathFinder
    {
        IEnumerable<Vector2> GetPath(Vector2 A, Vector2 C, IEnumerable<Edge> edges);
    }
}
