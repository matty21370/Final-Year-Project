using System;
using Game.Saving;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        private SavingSystem _savingSystem;

        private void Awake()
        {
            _savingSystem = GetComponent<SavingSystem>();
        }

        public void Save()
        {
            print("saved");
            _savingSystem.Save("save");
        }

        public void Load()
        {
            _savingSystem.Load("save");
        }
    }
}