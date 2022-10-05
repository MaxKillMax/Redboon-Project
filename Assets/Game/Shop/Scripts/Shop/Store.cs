using UnityEngine;

namespace RedboonTestProject.Store
{
    public class Store : MonoBehaviour
    {
        [SerializeField] private SlotHandler[] _playerSlots;
        [SerializeField] private SlotHandler[] _tradingSlots;

        [SerializeField] private WalletHandler _walletHandler;
        private Wallet _wallet;

        [SerializeField, Range(0, 1)] private float _sellingPriceReduction;
        private float InvertedSellingPriceReduction => 1 - _sellingPriceReduction;

        private void Start()
        {
            _wallet = _walletHandler.Wallet;
        }

        public bool TryBuyItem(Item item)
        {
            if (_wallet.Money < item.Cost)
                return false;

            BuyItem(item);
            return true;
        }

        private void BuyItem(Item item)
        {
            float spendMoney = item.Cost;
            _wallet.SpendMoney(spendMoney);
        }

        public void SellItem(Item item)
        {
            float addMoney = InvertedSellingPriceReduction * item.Cost;
            _wallet.AddMoney(addMoney);
        }
    }
}
