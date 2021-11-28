using UnityEngine;
using UnityEngine.Events;

namespace Game.Dialogue
{
    [CreateAssetMenu(fileName = "Dialogue", order = 1)]
    public class Dialogue : ScriptableObject
    {
        public string dialogueText;
    }
}