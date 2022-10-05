using System;

namespace RedboonTestProject.Store
{
    public class Wallet
    {
        public event Action OnMoneyChanged;

        public float Money { get; private set; }

        public Wallet(float startMoney) : base() => Money = startMoney;

        public void SetMoney(float value)
        {
            Money = value >= 0 ? value : Money;
            OnMoneyChanged?.Invoke();
        }

        public void AddMoney(float value)
        {
            Money += value;
            OnMoneyChanged?.Invoke();
        }

        public void SpendMoney(float value)
        {
            Money -= Money - value >= 0 ? value : Money;
            OnMoneyChanged?.Invoke();
        }

        public static float operator +(Wallet wallet1, Wallet wallet2) => wallet1.Money + wallet2.Money;

        public static float operator -(Wallet wallet1, Wallet wallet2) => wallet1.Money - wallet2.Money;
    }
}
