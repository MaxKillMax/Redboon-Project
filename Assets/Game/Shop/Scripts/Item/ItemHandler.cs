using UnityEngine;
using UnityEngine.EventSystems;

namespace RedboonTestProject.Store
{
    public class ItemHandler : MonoBehaviour, IHandler, IPointerDownHandler
    {
        private Hand _hand;

        public Item Item { get; private set; }

        public void Initialize(Hand hand, ItemData itemData)
        {
            _hand = hand;
            Item = new(itemData.Cost);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _hand.SwitchDragObject(transform);
        }
    }
}
