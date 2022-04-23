using System;
using Cinemachine;
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

        [SerializeField] private CinemachineVirtualCamera createCharacterCamera;

        private int _currentHair = 0, _currentBeard = 0, _currentMoustache = 0;

        public void HairNext()
        {
            _currentHair++;
            if (_currentHair > PlayerController.Instance.CustomizationOptions.Hair.Length)
            {
                _currentHair = 0;
            }
            
            UpdateCharacter();
        }

        public void HairPrevious()
        {
            _currentHair--;
            if (_currentHair < 0)
            {
                _currentHair = PlayerController.Instance.CustomizationOptions.Hair.Length;
            }
            
            UpdateCharacter();
        }

        public void BeardNext()
        {
            _currentBeard++;
            if (_currentBeard > PlayerController.Instance.CustomizationOptions.Beard.Length)
            {
                _currentBeard = 0;
            }
            
            UpdateCharacter();
        }

        public void BeardPrevious()
        {
            _currentBeard--;
            if (_currentBeard < 0)
            {
                _currentBeard = PlayerController.Instance.CustomizationOptions.Beard.Length;
            }
            
            UpdateCharacter();
        }

        public void MoustacheNext()
        {
            _currentMoustache++;
            if (_currentMoustache > PlayerController.Instance.CustomizationOptions.Moustache.Length)
            {
                _currentMoustache = 0;
            }
            
            UpdateCharacter();
        }

        public void MoustachePrevious()
        {
            _currentMoustache--;
            if (_currentMoustache < 0)
            {
                _currentMoustache = PlayerController.Instance.CustomizationOptions.Moustache.Length;
            }
            
            UpdateCharacter();
        }

        [SerializeField] private bool resetCharacter;

        private void UpdateCharacter()
        {
            CharacterAppearance tmp = new CharacterAppearance(_currentHair, _currentBeard, _currentMoustache);
            PlayerController.Instance.ApplyCustomization(tmp);
        }
        
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

            if (resetCharacter)
            {
                ResetCharacterAppearance();
            }

            if (PlayerPrefs.HasKey("character") && PlayerPrefs.GetString("character").Equals("yes"))
            {
                _characterCustomised = true;
                GetCharacter();
            }
            
            if (!_characterCustomised)
            {
                CreateCharacter();
            }
        }

        private void ResetCharacterAppearance()
        {
            if (PlayerPrefs.HasKey("character"))
            {
                PlayerPrefs.SetString("character", "no");
            }
        }

        private void CreateCharacter()
        {
            createCharacterCamera.Priority = 20;
            PlayerController.Instance.SetBusy(true);
            _uiManager.ShowCreateCharacterScreen();
        }

        public void CharacterDone()
        {
            CreateCharacter(_currentHair, _currentBeard, _currentMoustache);
            
            createCharacterCamera.Priority = 0;
            PlayerController.Instance.SetBusy(false);
            _uiManager.HideCreateCharacterScreen();
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
            
            PlayerPrefs.SetString("character", "yes");
            
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