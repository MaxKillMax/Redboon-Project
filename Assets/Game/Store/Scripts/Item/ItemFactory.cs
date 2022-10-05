using UnityEngine;

namespace RedboonTestProject.Store
{
    public class ItemFactory : MonoBehaviour, IMonoBehaviourFactory<ItemHandler>
    {
        [SerializeField] private ItemHandler _prefab;

        private Hand _hand;
        private Transform _parent;
        private ItemData _data;

        public void SetData(Hand hand, Transform parent, ItemData data)
        {
            _hand = hand;
            _parent = parent;
            _data = data;
        }

        public ItemHandler CreateObject()
        {
            ItemHandler itemHandler = Instantiate(_prefab, _parent);
            itemHandler.Initialize(_hand, _data);
            return itemHandler;
        }
    }
}
