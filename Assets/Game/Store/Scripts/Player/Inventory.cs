using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace RedboonTestProject.Store
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField, ReadOnly] private int _itemsLimit = 15;
        [SerializeField, ValidateInput("ExcessLimit", "Items out of limit")] private List<ItemData> _items;

        private bool ExcessLimit(List<ItemData> value) => value.Count <= _itemsLimit;

        public IEnumerable<ItemData> GetItems() => _items;

        public bool TryAddItem(ItemData itemData)
        {
            if (_items.Count >= _itemsLimit)
                return false;

            _items.Add(itemData);
            return true;
        }

        public bool TryRemoveItem(ItemData itemData)
        {
            if (!_items.Contains(itemData))
                return false;

            _items.Remove(itemData);
            return true;
        }
    }
}
