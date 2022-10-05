using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RedboonTestProject.Store
{
    [RequireComponent(typeof(ItemHandler)), RequireComponent(typeof(Image))]
    public class ItemSlotPanel : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private ItemHandler _itemHandler;
        [SerializeField] private Image _iconImage;
        [SerializeField] private TMP_Text _costText;

        private void OnEnable()
        {
            _itemHandler.OnHandableObjectInitialized += UpdateInformation;
        }

        private void OnDisable()
        {
            _itemHandler.OnHandableObjectInitialized -= UpdateInformation;
        }

        private void UpdateInformation()
        {
            _iconImage.sprite = _itemHandler.HandableObject.Sprite;
            _costText.text = _itemHandler.HandableObject.Cost.ToString("N0");
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _itemHandler.Hand.TryDragObject(this, _itemHandler);
        }

        public void Drag()
        {
            _iconImage.raycastTarget = false;
        }

        public void UnDrag()
        {
            _iconImage.raycastTarget = true;
        }
    }
}
