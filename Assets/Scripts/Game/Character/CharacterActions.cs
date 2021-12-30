using System;
using Game.Inventory;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Character
{
    public class CharacterActions : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
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

        public void GiveItemToPlayer(Item item)
        {
            FindObjectOfType<InventorySystem>().AddItem(item);
        }
    }
}