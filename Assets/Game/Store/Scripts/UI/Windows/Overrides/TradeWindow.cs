using UnityEngine;
using UnityEngine.UI;

namespace RedboonTestProject
{
    public class TradeWindow : Window
    {
        [SerializeField] private Button _returnButton;

        private void OnEnable()
        {
            _returnButton.onClick.AddListener(StartMenuScene);
        }

        private void OnDisable()
        {
            _returnButton.onClick.RemoveListener(StartMenuScene);
        }

        private void StartMenuScene()
        {
            Scenes.Instance.SetScene(SceneType.Menu);
        }
    }
}
