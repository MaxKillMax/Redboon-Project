using UnityEngine;

namespace RedboonTestProject.Pathfinding
{
    public interface IMonoBehaviourFactory<T> where T : MonoBehaviour
    {
        public T CreateObject();
    }
}
