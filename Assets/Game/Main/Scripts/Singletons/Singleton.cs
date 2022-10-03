namespace RedboonTestProject
{
    public abstract class Singleton<T> where T : Singleton<T>
    {
        public static T Instance { get; private set; }

        public Singleton()
        {
            Instance = Instance == null ? this as T : throw new System.Exception($"Instance of singleton {nameof(T)} is not null");
        }
    }
}
