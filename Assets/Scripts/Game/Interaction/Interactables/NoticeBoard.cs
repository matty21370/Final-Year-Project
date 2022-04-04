﻿namespace Game.Interaction.Interactables
{
    public class NoticeBoard : Interactable
    {
        public override void OnInteract(Interactor interactor)
        {
            base.OnInteract(interactor);
            print("Interacting with notice board");
        }
    }
}