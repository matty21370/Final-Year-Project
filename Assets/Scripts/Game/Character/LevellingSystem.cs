using System;
using System.Collections;
using System.Collections.Generic;
using Game.Saving;
using Game.UI;
using UnityEngine;

namespace Game.Character
{
    public class LevellingSystem : MonoBehaviour, ISaveable
    {
        private static LevellingSystem _instance;
    
        public static LevellingSystem Instance => _instance;
    
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
    
        [SerializeField]
        private float xpMultiplier = 1.0f;
    
        private int _level;
        private float _xp, _xpToLevel;

        public int Level => _level;
        public float Xp => _xp;
        public float XpToLevel => _xpToLevel;
        
        private void Start()
        {
            _level = 1;
            _xp = 0;
            CalculateXpToLevel();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                GiveXp(10f);
            }
        }

        public void GiveXp(float amount)
        {
            _xp += (amount * xpMultiplier);
            print(_xp);
            
            if (_xp >= _xpToLevel)
            {
                LevelUp();
            }
            
            UIManager.Instance.UpdateXpBar(amount);
        }
    
        private void LevelUp()
        {
            print("Level up");
            _level += 1;
            _xp = 0;
            
            CalculateXpToLevel();
        }
    
        private void CalculateXpToLevel()
        {
            _xpToLevel = _level * 100f;
        }
    
        public object CaptureState()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("level", _level);
            data.Add("xp", _xp);
    
            return data;
        }
    
        public void RestoreState(object state)
        {
            Dictionary<string, object> data = (Dictionary<string, object>) state;
            
            if(data == null) return;
    
            _level = (int) data["level"];
            _xp = (float) data["xp"];
            
            CalculateXpToLevel();
        }
    }
}

