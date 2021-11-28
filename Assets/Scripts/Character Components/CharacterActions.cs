using UnityEngine;

namespace Game.Character
{
    public class CharacterActions : MonoBehaviour
    {
        public void Test()
        {
            FindObjectOfType<PlayerController>().PlayerHealth.TakeDamage(10f);
        }

        public void Talk()
        {
            int r = Random.Range(1, 3);

            switch (r)
            {
                case 1:
                    GetComponent<Animator>().SetTrigger("talk");
                    break;
                case 2:
                    GetComponent<Animator>().SetTrigger("talk1");
                    break;
                case 3:
                    GetComponent<Animator>().SetTrigger("talk2");
                    break;
            }
        }

        public void Wave()
        {
            GetComponent<Animator>().SetTrigger("wave");
        }
    }
}