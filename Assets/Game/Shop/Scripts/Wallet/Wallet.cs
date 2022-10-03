namespace RedboonTestProject.Store
{
    public class Wallet
    {
        public float Money { get; private set; }

        public Wallet(float startMoney) : base() => Money = startMoney;

        public void SetMoney(float value) => Money = value >= 0 ? value : Money;

        public void AddMoney(float value) => Money += value;

        public void SpendMoney(float value) => Money -= Money - value >= 0 ? value : Money;

        public static float operator +(Wallet wallet1, Wallet wallet2) => wallet1.Money + wallet2.Money;

        public static float operator -(Wallet wallet1, Wallet wallet2) => wallet1.Money - wallet2.Money;
    }
}
