using System.Collections;
using System.Collections.Generic;
using Game.Character;
using Game.Core.Dialogue;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup restrictedNotification;

    [SerializeField] private GameObject dialogueUI;
    private Speaker _speaker;
    
    public void ShowRestrictedNotification()
    {
        restrictedNotification.GetComponent<Animator>().SetTrigger("FadeIn");
    }
    
    public void HideRestrictedNotification()
    {
        restrictedNotification.GetComponent<Animator>().SetTrigger("FadeOut");
    }

    public void ShowDialogue(Dialogue dialogue, Speaker speaker)
    {
        print("Callled");
        FindObjectOfType<PlayerController>().SetBusy(true);
        _speaker = speaker;
        dialogueUI.SetActive(true);
        dialogueUI.GetComponentInChildren<Text>().text = dialogue.dialogueText;
    }

    public void HideDialogue(Speaker speaker)
    {
        dialogueUI.SetActive(false);
        dialogueUI.GetComponentInChildren<Text>().text = "";
        FindObjectOfType<PlayerController>().SetBusy(false);
        speaker.ResetDialogue();
    }

    public void NextDialogue()
    {
        if(_speaker == null) return;
        
        _speaker.ShowDialogue();
    }
}
