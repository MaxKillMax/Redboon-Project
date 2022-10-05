using System;
using UnityEngine;

namespace RedboonTestProject.Store
{
    public class ItemHandler : MonoBehaviour, IHandler<Item>
    {
        public event Action OnHandableObjectInitialized;

        public Item HandableObject { get; private set; }
        public Hand Hand { get; private set; }

        public Transform PositionTransform { get; private set; }

        public void Initialize(Hand hand, ItemData itemData)
        {
            Hand = hand;
            HandableObject = new Item(itemData.Cost, itemData.Sprite);
            OnHandableObjectInitialized?.Invoke();
        }

        public void SetPosition(Transform positionTransform)
        {
            transform.position = positionTransform.position;
            PositionTransform = positionTransform;
        }
    }
}
