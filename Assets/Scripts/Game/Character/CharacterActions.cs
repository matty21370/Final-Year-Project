using System;
using Game.Dialogue;
using Game.Inventory;
using Game.Items;
using Game.UI;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Game.Character
{
    public class CharacterActions : MonoBehaviour
    {
        private Animator _animator;

        private bool _moveToPlayerAndTalk = false;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (_moveToPlayerAndTalk)
            {
                if (Vector3.Distance(transform.position, FindObjectOfType<PlayerController>().transform.position) < 1.5f)
                {
                    _moveToPlayerAndTalk = false;
                    GetComponent<Speaker>().Initiate();
                    FindObjectOfType<PlayerController>().GetComponent<Movement>().Stop();
                }
                else
                {
                    GetComponent<Movement>().Move(FindObjectOfType<PlayerController>().transform.position);
                }
            }
        }

        public void Talk()
        {
            int r = Random.Range(1, 3);
            _animator.SetTrigger("talk" + r);
        }

        public void Wave()
        {
            _animator.SetTrigger("wave");
        }

        public void GiveItemToPlayer(String n)
        {
            Item item = ItemDatabase.Instance.GetItem(n);

            if (item != null)
            {
                InventorySystem.Instance.AddItem(item);
            }
        }

        public void MoveToPlayerAndTalk()
        {
            _moveToPlayerAndTalk = true;
            GetComponent<Movement>().Move(FindObjectOfType<PlayerController>().transform.position);
        }

        public void ShowNotification(GameObject tGameObject)
        {
            NotificationArgs args = tGameObject.GetComponent<UI.NotificationArgs>();
            NotificationSystem.Instance.ShowNotifcation(args.header, args.content);
        }
    }
}