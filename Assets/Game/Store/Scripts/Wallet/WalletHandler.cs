using UnityEngine;
using NaughtyAttributes;
using System;

namespace RedboonTestProject.Store
{
    public class WalletHandler : MonoBehaviour, IHandler<Wallet>
    {
        public event Action OnHandableObjectInitialized;

        [SerializeField] private bool _initializeOnAwake;
        [SerializeField, ShowIf("_initializeOnAwake")] private float _startMoney;

        public Wallet HandableObject { get; private set; }

        private void Awake()
        {
            if (_initializeOnAwake)
                Initialize(new Wallet(_startMoney));
        }

        public void Initialize(Wallet wallet)
        {
            HandableObject = wallet;
            OnHandableObjectInitialized?.Invoke();
        }
    }
}
