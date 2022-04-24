using System;
using UnityEngine;

namespace Game.Character
{
    public class Clothing : MonoBehaviour
    {
        [SerializeField] private ArmourSet[] armourSets;
        [SerializeField] private GameObject[] bodyParts;

        public void EquipArmourSet(string id)
        {
            foreach (var bodyPart in bodyParts)
            {
                bodyPart.SetActive(false);
            }
            
            foreach (var armourSet in armourSets)
            {
                if (id.Equals(armourSet.ID))
                {
                    print("yes");
                    foreach (var part in armourSet.Parts)
                    {
                        part.SetActive(true);
                    }
                    return;
                }
            }
        }
    }

    [Serializable]
    public class ArmourSet
    {
        [SerializeField] private GameObject[] parts;
        [SerializeField] private string id;
        [Range(0, 1)]
        [SerializeField] private float protection;

        public GameObject[] Parts => parts;

        public float Protection => protection;
        public string ID => id;
    }
}