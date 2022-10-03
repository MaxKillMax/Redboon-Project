using UnityEngine;

namespace RedboonTestProject
{
    public abstract class Window : MonoBehaviour
    {
        public virtual WindowState State { get; private set; } = WindowState.Closed;

        public virtual void Initialize()
        {
            Switch(State);
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
