using UnityEngine;

namespace RedboonTestProject.Store
{
    [CreateAssetMenu(fileName = "Item Data", menuName = "Item Data", order = 51)]
    public class ItemData : ScriptableObject
    {
        [SerializeField] private float _cost;
        public float Cost => _cost;
    }
}
