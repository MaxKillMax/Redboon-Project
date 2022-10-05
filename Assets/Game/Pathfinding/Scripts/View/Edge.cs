using UnityEngine;
using System;

namespace RedboonTestProject.Pathfinding
{
    [Serializable]
    public struct Edge
    {
        public Rectangle First;
        public Rectangle Second;
        public Vector3 Start;
        public Vector3 End;
    }
}
