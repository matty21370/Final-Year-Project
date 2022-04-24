using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class MenuSystem : MonoBehaviour
    {
        [SerializeField] private GameObject inventoryScreen, questScreen;
        [SerializeField] private Text inventoryText, questText;
        [SerializeField] private Color activeColour, inactiveColour;
        
        public void InventoryClicked()
        {
            inventoryScreen.SetActive(true);
            questScreen.SetActive(false);

            inventoryText.color = activeColour;
            questText.color = inactiveColour;
        }

        public void QuestClicked()
        {
            inventoryScreen.SetActive(false);
            questScreen.SetActive(true);
            QuestMenu questMenu = questScreen.GetComponent<QuestMenu>();
            questMenu.Init();
            
            inventoryText.color = inactiveColour;
            questText.color = activeColour;
        }
    }
}