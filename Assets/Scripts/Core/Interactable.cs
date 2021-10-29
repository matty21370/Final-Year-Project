using System.Collections;
using System.Collections.Generic;
using Game.Character;
using UnityEngine;

namespace Game.Core
{
    public interface Interactable
    {
        void OnInteract(PlayerController player);
    }
}

