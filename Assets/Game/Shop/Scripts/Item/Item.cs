namespace RedboonTestProject.Store
{
    public class Item
    {
        public float Cost { get; protected set; }

        public Item(float cost)
        {
            Cost = cost;
        }
    }
}
