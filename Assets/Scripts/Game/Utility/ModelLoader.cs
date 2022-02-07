using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Utility
{
    public class ModelLoader : MonoBehaviour
    {
        private static ModelLoader _instance;
        public static ModelLoader Instance => _instance;
        
        private List<Model> _models = new List<Model>();

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            
            Model model = new Model("Test", (GameObject)Resources.Load("Models/Test"));
            print(model.TheModel.ToString());
            _models.Add(model);
        }

        public GameObject GetModel(string n)
        {
            foreach (var model in _models)
            {
                if (String.Equals(model.Name, n))
                {
                    return model.TheModel;
                }
            }
            
            Debug.LogWarning("Could not find model: " + n);
            return null;
        }
    }

    public class Model
    {
        public GameObject TheModel { get; }
        public string Name { get; }

        public Model(string name, GameObject model)
        {
            TheModel = model;
            Name = name;
        }
    }
}