using System;
using System.Security.Cryptography;
using UnityEngine;

namespace Game.Character
{
    public class SpawnManager : MonoBehaviour
    {
        private static SpawnManager _instance;

        public static SpawnManager Instance => _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}