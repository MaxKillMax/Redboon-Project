using UnityEngine;

namespace RedboonTestProject
{
    public interface IMonoBehaviourFactory<T> where T : MonoBehaviour
    {
        public T CreateObject();
    }
}
