using System;
using UnityEngine;

namespace RedboonTestProject.Store
{
    public class Hand
    {
        [SerializeField] private int _uiLayerMask;

        public event Action OnStartDrag;
        public event Action OnStartEndDrag;
        public event Action OnEndDrag;

        private ItemSlotPanel _itemSlotPanel;
        public ItemHandler DragItem { get; private set; }
        public bool IsDrag => DragItem != null;

        private bool _firstFrameEnded = false;
        private bool _startEnd = false;

        public void TryDrag()
        {
            if (!IsDrag)
                return;

            DragItem.transform.position = Input.mousePosition;

            if (_firstFrameEnded)
            {
                if (Input.GetMouseButtonDown(0))
                    StartEndDrag();
                else if (_startEnd && Input.GetMouseButtonUp(0))
                    EndDrag();
            }

            _firstFrameEnded = true;
        }

        public void TryDragObject(ItemSlotPanel itemSlotPanel, ItemHandler item)
        {
            if (DragItem != item)
                StartDragObject(itemSlotPanel, item);
        }

        private void StartDragObject(ItemSlotPanel itemSlotPanel, ItemHandler item)
        {
            _startEnd = false;
            _firstFrameEnded = false;

            DragItem = item;
            _itemSlotPanel = itemSlotPanel;
            _itemSlotPanel.Drag();

            OnStartDrag?.Invoke();
        }

        private void StartEndDrag()
        {
            _startEnd = true;
            OnStartEndDrag?.Invoke();
        }

        private void EndDrag()
        {
            DragItem.SetPosition(DragItem.PositionTransform);
            _itemSlotPanel.UnDrag();
            DragItem = null;
            _itemSlotPanel = null;

            OnEndDrag?.Invoke();
        }
    }
}
