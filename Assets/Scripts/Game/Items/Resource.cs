using UnityEngine;

namespace Game.Core.Items
{
    [CreateAssetMenu(fileName = "Resource", order = 1)]
    public class Resource : ScriptableObject
    {
        [SerializeField] private string name;

        public string Name => name;
    }
}