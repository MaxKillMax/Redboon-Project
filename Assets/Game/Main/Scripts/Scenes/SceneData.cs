using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RedboonTestProject
{
    [Serializable]
    public struct SceneData
    {
        [SerializeField] private Scene _scene;
        public Scene Scene => _scene;

        [SerializeField] private SceneType _type;
        public SceneType Type => _type;
    }
}
