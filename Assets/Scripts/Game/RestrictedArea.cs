using System;
using System.Collections;
using System.Collections.Generic;
using Game.Character;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(BoxCollider))]
    public class RestrictedArea : MonoBehaviour
    {
        [SerializeField] private List<Combat> guards = new List<Combat>();

        private UIManager _uiManager;
    
        private void Awake()
        {
            _uiManager = FindObjectOfType<UIManager>();
            GetComponent<BoxCollider>().isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                //_uiManager.ShowRestrictedNotification();

                foreach (var guard in guards)
                {
                    guard.SetAggressive(true);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            //_uiManager.HideRestrictedNotification();
        }
    }
}



