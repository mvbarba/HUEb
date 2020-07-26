using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteractable : Interactable
{
    public override void OnInteract()
    {
        Debug.Log("Button Pressed!");
    }
}
