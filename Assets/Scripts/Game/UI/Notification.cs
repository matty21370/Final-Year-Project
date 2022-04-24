using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    [Serializable]
    public class Notification : MonoBehaviour
    {
        [SerializeField] private Text headerText, contentText;

        public void Init(string header, string content)
        {
            headerText.text = header;
            contentText.text = content;
        }

        public void Close()
        {
            Destroy(gameObject);
        }
    }
}