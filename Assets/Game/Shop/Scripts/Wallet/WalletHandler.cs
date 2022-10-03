using UnityEngine;

namespace RedboonTestProject.Store
{
    public class WalletHandler : MonoBehaviour, IHandler
    {
        [SerializeField] private float _startMoney;

        public Wallet Wallet { get; private set; }

        private void Awake()
        {
            Wallet = new(_startMoney);
        }
    }
}
