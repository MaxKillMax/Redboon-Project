using System;

namespace RedboonTestProject
{
    public interface IHandler<T>
    {
        public event Action OnHandableObjectInitialized;

        public T HandableObject { get; }
    }
}
