using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    Action OnUp { get; set; }
    Action OnDown { get; set; }
    Action OnInteraction { get; set; }
    void UpAction();
    void DownAction();
    void Interact();
    void SetDelegateUp(Action action);
    void SetDelegateDown(Action action);
    void SetDelegateInteract(Action action);
}

