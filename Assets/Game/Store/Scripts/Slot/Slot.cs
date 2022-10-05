using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace RedboonTestProject.Store
{
    [RequireComponent(typeof(Image))]
    public class Slot : MonoBehaviour, IPointerDownHandler
    {
        public event Action OnHandableObjectInitialized;
        public event Action<Slot> OnSlotPointerDown;

        public ItemHandler Item { get; private set; }
        public bool HaveItem => Item != null;

        public Vector3 ItemPosition => transform.position;

        private void Awake()
        {
            OnHandableObjectInitialized?.Invoke();
        }

        public void SetItem(ItemHandler item)
        {
            Item = item;
            item.transform.SetParent(transform);
            Item.SetPosition(transform);
        }

        public void ResetItem()
        {
            Item = null;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnSlotPointerDown?.Invoke(this);
        }
    }
}
