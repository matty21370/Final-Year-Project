using System;
using Game.Character;
using UnityEngine;

namespace Game.Utility
{
    public class GameSetup : MonoBehaviour
    {
        private void Awake()
        {
            Faction.RegisterFaction("The Omlex");
            Faction.RegisterFaction("Bandits");
            Faction.RegisterFaction("The Resistance");
        }
    }
}