namespace RedboonTestProject.Store
{
    public class Slot
    {
        public Item Item { get; private set; }
        public bool HaveItem => Item != null;

        public void SetItem(Item item)
        {
            Item = item;
        }

        public void ResetItem()
        {
            Item = null;
        }
    }
}
