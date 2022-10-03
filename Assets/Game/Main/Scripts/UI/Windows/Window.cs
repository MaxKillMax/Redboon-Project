using UnityEngine;

namespace RedboonTestProject
{
    public abstract class Window : MonoBehaviour
    {
        [SerializeField] private WindowState _startState = WindowState.Closed;
        public virtual WindowState State { get; private set; }

        public virtual void Initialize()
        {
            Switch(_startState);
        }

        public virtual void Switch(WindowState state)
        {
            if (state == WindowState.Opened)
                Open();
            else if (state == WindowState.Closed)
                Close();
        }

        public virtual void Open()
        {
            State = WindowState.Opened;
            gameObject.SetActive(true);
        }

        public virtual void Close()
        {
            State = WindowState.Closed;
            gameObject.SetActive(false);
        }
    }
}
