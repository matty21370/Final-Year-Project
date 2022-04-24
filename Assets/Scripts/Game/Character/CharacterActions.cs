using System;
using Game.Character.AI;
using Game.Dialogue;
using Game.Interaction;
using Game.Inventory;
using Game.Items;
using Game.Questing;
using Game.UI;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Game.Character
{
    public class CharacterActions : MonoBehaviour
    {
        private Animator _animator;

        private bool _moveToPlayerAndTalk = false, _goingToDestination = false;

        private Vector3 _destination;

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

            if (_goingToDestination)
            {
                if (Vector3.Distance(transform.position, _destination) < 1.5f)
                {
                    _goingToDestination = false;
                    OnReachedDestination();
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

        private string _questIdentifier;
        
        public void SetDestination(Waypoint waypoint, string identifier)
        {
            _destination = waypoint.transform.position;
            GetComponent<Movement>().Move(waypoint.transform.position);
            _goingToDestination = true;
            _questIdentifier = identifier;
        }

        private void OnReachedDestination()
        {
            print("Reached destination");
            
            if (QuestManager.Instance.ActiveQuest != null)
            {
                Objective objective = QuestManager.Instance.ActiveQuest.GetCurrentObjective();
                if (_questIdentifier == objective.Identifier)
                {
                    objective.CompleteTarget(GetComponent<Interactable>());
                    print("Completing stuff");
                    _goingToDestination = false;
                }
            }
        }

        public void ShowNotification(GameObject tGameObject)
        {
            NotificationArgs args = tGameObject.GetComponent<UI.NotificationArgs>();
            NotificationSystem.Instance.ShowNotifcation(args.header, args.content);
        }
    }
}