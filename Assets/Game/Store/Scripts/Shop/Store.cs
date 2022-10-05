using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RedboonTestProject.Store
{
    public class Store : MonoBehaviour
    {
        [SerializeField] private Slot[] _playerSlots;
        [SerializeField] private Slot[] _tradingSlots;

        [SerializeField] private WalletHandler _walletHandler;
        [SerializeField] private ItemFactory _itemFactory;

        [SerializeField, Range(0, 1)] private float _sellingPriceReduction;
        private float InvertedSellingPriceReduction => 1 - _sellingPriceReduction;

        private bool _isStoreOpen = false;

        private Inventory _playerInventory;
        private Inventory _tradeInventory;
        private Hand _playerHand;

        private void OnEnable()
        {
            for (int i = 0; i < _playerSlots.Length; i++)
                _playerSlots[i].OnSlotPointerDown += CheckSlot;

            for (int i = 0; i < _tradingSlots.Length; i++)
                _tradingSlots[i].OnSlotPointerDown += CheckSlot;
        }

        private void OnDisable()
        {
            for (int i = 0; i < _playerSlots.Length; i++)
                _playerSlots[i].OnSlotPointerDown -= CheckSlot;

            for (int i = 0; i < _tradingSlots.Length; i++)
                _tradingSlots[i].OnSlotPointerDown -= CheckSlot;
        }

        public void OpenStore(Inventory playerInventory, Inventory tradeInventory, Hand hand)
        {
            _isStoreOpen = true;

            _playerInventory = playerInventory;
            _tradeInventory = tradeInventory;

            _playerHand = hand;

            SetItems(StoreSide.Player, _playerInventory.GetItems());
            SetItems(StoreSide.Trader, _tradeInventory.GetItems());
        }

        public void CloseStore()
        {
            _isStoreOpen = false;

            DestroyItems();
        }

        public bool TryBuyItem(ItemHandler item)
        {
            if (!_isStoreOpen || _walletHandler.HandableObject.Money < item.HandableObject.Cost)
                return false;

            BuyItem(item);
            return true;
        }

        private void BuyItem(ItemHandler item)
        {
            GetItemSlot(item).ResetItem();
            GetFreeSlot(StoreSide.Player).SetItem(item);
            float spendMoney = item.HandableObject.Cost;
            _walletHandler.HandableObject.SpendMoney(spendMoney);
        }

        public bool TrySellItem(ItemHandler item)
        {
            if (!_isStoreOpen)
                return false;

            SellItem(item);
            return true;
        }

        private void SellItem(ItemHandler item)
        {
            GetItemSlot(item).ResetItem();
            GetFreeSlot(StoreSide.Trader).SetItem(item);
            float addMoney = InvertedSellingPriceReduction * item.HandableObject.Cost;
            _walletHandler.HandableObject.AddMoney(addMoney);
        }

        private void SetItems(StoreSide side, IEnumerable<ItemData> items)
        {
            Slot[] slots = side == StoreSide.Player ? _playerSlots : _tradingSlots;
            ItemData[] itemsArray = items.ToArray();

            InitializeItemSlots(slots, itemsArray);
        }

        private void InitializeItemSlots(Slot[] slots, ItemData[] itemsArray)
        {
            for (int i = 0; i < itemsArray.Length; i++)
            {
                _itemFactory.SetData(_playerHand, slots[i].transform, itemsArray[i]);
                slots[i].SetItem(_itemFactory.CreateObject());
            }
        }

        private Slot GetFreeSlot(StoreSide side)
        {
            Slot[] slots = side == StoreSide.Player ? _playerSlots : _tradingSlots;

            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].Item == null)
                    return slots[i];
            }

            return default;
        }

        private void DestroyItems()
        {
            for (int i = 0; i < _tradingSlots.Length; i++)
{
                if (_tradingSlots[i].Item)
                    Destroy(_tradingSlots[i].Item);
            }

            for (int i = 0; i < _playerSlots.Length; i++)
{
                if (_playerSlots[i].Item)
                    Destroy(_playerSlots[i].Item);
            }
        }

        private void CheckSlot(Slot slot)
        {
            if (_playerHand.IsDrag && slot.Item == null)
            {
                if (_playerSlots.Contains(slot) &&
                    _tradingSlots.Contains(GetItemSlot(_playerHand.DragItem)))
                {
                    TryBuyItem(_playerHand.DragItem);
                }
                else if (_tradingSlots.Contains(slot) &&
                    _playerSlots.Contains(GetItemSlot(_playerHand.DragItem)))
                {
                    TrySellItem(_playerHand.DragItem);
                }
            }
        }

        public Slot GetItemSlot(ItemHandler item)
        {
            for (int i = 0; i < _tradingSlots.Length; i++)
            {
                if (_tradingSlots[i].Item == item)
                    return _tradingSlots[i];
            }

            for (int i = 0; i < _playerSlots.Length; i++)
            {
                if (_playerSlots[i].Item == item)
                    return _playerSlots[i];
            }

            return default;
        }
    }
}
