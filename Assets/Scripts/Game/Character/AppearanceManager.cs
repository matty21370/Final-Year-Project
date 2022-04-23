using System;
using Game.UI;
using UnityEngine;

namespace Game.Character
{
    public class AppearanceManager : MonoBehaviour
    {
        private static AppearanceManager _intance;

        public static AppearanceManager Instance => _intance;

        private bool _characterCustomised = false;

        private UIManager _uiManager;

        private CharacterAppearance _characterAppearance;

        public CharacterAppearance CharacterAppearance => _characterAppearance;

        private void Awake()
        {
            if (_intance == null)
            {
                _intance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            _uiManager = UIManager.Instance;

            if (PlayerPrefs.HasKey("character"))
            {
                _characterCustomised = true;
                GetCharacter();
            }

            if (!_characterCustomised)
            {
                CreateCharacter();
            }
        }

        private void CreateCharacter()
        {
            //_uiManager.ShowCreateCharacterScreen();
            CreateCharacter(2, 4, 5);
        }

        private void GetCharacter()
        {
            if(!_characterCustomised) return;

            if (PlayerPrefs.HasKey("hair") && PlayerPrefs.HasKey("beard") && PlayerPrefs.HasKey("moustache"))
            {
                _characterAppearance = new CharacterAppearance(PlayerPrefs.GetInt("hair"), PlayerPrefs.GetInt("beard"), PlayerPrefs.GetInt("moustache"));
                PlayerController.Instance.ApplyCustomization(_characterAppearance);
            }
        }

        public void CreateCharacter(int hair, int beard, int moustache)
        {
            _characterCustomised = true;
            
            PlayerPrefs.SetInt("hair", hair);
            PlayerPrefs.SetInt("beard", beard);
            PlayerPrefs.SetInt("moustache", moustache);
            
            GetCharacter();
        }
    }

    public class CharacterAppearance
    {
        private int hair, beard, moustache;

        public CharacterAppearance(int hair, int beard, int moustache)
        {
            this.hair = hair;
            this.beard = beard;
            this.moustache = moustache;
        }

        public int Hair => hair;

        public int Beard => beard;

        public int Moustache => moustache;
    }
}