using TMPro;
using UnityEngine;

namespace RedboonTestProject.Store
{
    [RequireComponent(typeof(WalletHandler))]
    public class WalletPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _moneyText;
        [SerializeField] private WalletHandler _walletHandler;

        private Wallet _wallet;

        private void OnEnable()
        {
            _walletHandler.OnHandableObjectInitialized += UpdateWallet;

            if (_wallet != null)
                _wallet.OnMoneyChanged += UpdatePanel;
        }

        private void OnDisable()
        {
            _walletHandler.OnHandableObjectInitialized -= UpdateWallet;

            if (_wallet != null)
                _wallet.OnMoneyChanged -= UpdatePanel;
        }

        private void UpdateWallet()
        {
            if (_wallet != null)
                _wallet.OnMoneyChanged -= UpdatePanel;

            _wallet = _walletHandler.HandableObject;
            _wallet.OnMoneyChanged += UpdatePanel;

            UpdatePanel();
        }

        private void UpdatePanel()
        {
            _moneyText.text = _wallet.Money.ToString();
        }
    }
}
