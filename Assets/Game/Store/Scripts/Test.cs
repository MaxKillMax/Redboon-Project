using UnityEngine;

namespace RedboonTestProject.Store
{
    // The class replaces the more global controller that should run the store
    public class Test : MonoBehaviour
    {
        [SerializeField] private Inventory _playerInventory;
        [SerializeField] private Inventory _tradeInventory;
        [SerializeField] private HandHandler _hand;
        [SerializeField] private Store _store;

        private void Start()
        {
            _store.OpenStore(_playerInventory, _tradeInventory, _hand.HandableObject);
        }
    }
}
