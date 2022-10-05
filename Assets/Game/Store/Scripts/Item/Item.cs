using UnityEngine;

namespace RedboonTestProject.Store
{
    public class Item
    {
        public float Cost { get; protected set; }
        public Sprite Sprite { get; protected set; }

        public Item(float cost, Sprite sprite)
        {
            Cost = cost;
            Sprite = sprite;
        }
    }
}
