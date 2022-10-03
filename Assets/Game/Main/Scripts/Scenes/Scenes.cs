using UnityEngine;
using UnityEngine.SceneManagement;

namespace RedboonTestProject
{
    public class Scenes : SingletonMB<Scenes>
    {
        [SerializeField] private SceneData[] _sceneDatas;

        public void SetScene(SceneType type)
        {
            foreach(SceneData sceneData in _sceneDatas)
            {
                if (sceneData.Type == type)
                    SceneManager.LoadSceneAsync(sceneData.Scene.buildIndex);
            }    
        }
    }
}
