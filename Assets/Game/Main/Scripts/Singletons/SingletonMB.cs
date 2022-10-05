using UnityEngine;

namespace RedboonTestProject
{
    public abstract class SingletonMB<T> : MonoBehaviour where T : SingletonMB<T>
    {
        [SerializeField] private bool _dontDestroyOnLoad;

        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;
                if (_dontDestroyOnLoad)
                    DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
