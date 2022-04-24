using System;
using UnityEngine;

namespace Game.UI
{
    public class NotificationSystem : MonoBehaviour
    {
        private static NotificationSystem _instance;
        public static NotificationSystem Instance => _instance;
        
        [SerializeField] private GameObject notification;

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

        public void ShowNotifcation(string header, string content)
        {
            if (notification != null)
            {
                GameObject tmp = Instantiate(notification, transform);
                Notification n = tmp.GetComponent<Notification>();
                n.Init(header, content);
            }
        }
    }
}