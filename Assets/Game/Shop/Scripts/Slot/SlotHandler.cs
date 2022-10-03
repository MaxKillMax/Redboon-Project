using UnityEngine;

namespace RedboonTestProject.Store
{
    public class SlotHandler : MonoBehaviour, IHandler
    {
        public Slot Slot { get; private set; }

        private void Awake()
        {
            Slot = new Slot();
        }
    }
}
