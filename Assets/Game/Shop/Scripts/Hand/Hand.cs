using UnityEngine;

namespace RedboonTestProject.Store
{
    public class Hand
    {
        private Camera _camera;

        public Transform DragObject { get; private set; }
        public bool IsDrag => DragObject != null;

        public Hand(Camera camera)
        {
            _camera = camera;
        }

        public void TryDrag()
        {
            if (!IsDrag)
                return;

            DragObject.position = _camera.ScreenToWorldPoint(Input.mousePosition);
        }

        public void SwitchDragObject(Transform transform)
        {
            if (DragObject == transform)
                ResetDragObject();
            else
                SetDragObject(transform);
        }

        private void SetDragObject(Transform transform)
        {
            DragObject = transform;
        }

        private void ResetDragObject()
        {
            DragObject = null;
        }
    }
}
