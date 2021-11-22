using UnityEngine;

namespace Game.Core.Dialogue
{
    [CreateAssetMenu(fileName = "Dialogue", order = 1)]
    public class Dialogue : ScriptableObject
    {
        public string dialogueText;
    }
}