using System;
using Game.Core.Items;
using UnityEngine;

namespace Game.Questing
{
    [CreateAssetMenu(fileName = "Quest", order = 1)]
    public class Quest : ScriptableObject
    {
        [SerializeField] private string questName;
        [SerializeField] private Reward reward;
    }
    
    [Serializable]
    public class Reward : MonoBehaviour
    {
        [SerializeField] private Resource resource;
        [SerializeField] private int amount;
        
        public Resource Resource => resource;

        public int Amount => amount;
    }
}