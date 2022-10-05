using UnityEngine;
using UnityEngine.UI;

namespace RedboonTestProject
{
    public class SelectSceneWindow : Window
    {
        [SerializeField] private Button _shopSceneButton;
        [SerializeField] private Button _pathFindingSceneButton;

        private void OnEnable()
        {
            _shopSceneButton.onClick.AddListener(StartShopScene);
            _pathFindingSceneButton.onClick.AddListener(StartPathFindingScene);
        }

        private void OnDisable()
        {
            _shopSceneButton.onClick.RemoveListener(StartShopScene);
            _pathFindingSceneButton.onClick.RemoveListener(StartPathFindingScene);
        }

        private void StartShopScene()
        {
            Scenes.Instance.SetScene(SceneType.Store);
        }

        private void StartPathFindingScene()
        {
            Scenes.Instance.SetScene(SceneType.PathFinding);
        }
    }
}
