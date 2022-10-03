using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RedboonTestProject
{
    [Serializable]
    public struct SceneData
    {
        [SerializeField] private string _scene;
        public string Scene => _scene;

        [SerializeField] private SceneType _type;
        public SceneType Type => _type;
    }
}
