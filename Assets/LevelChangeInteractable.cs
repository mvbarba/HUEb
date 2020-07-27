using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChangeInteractable : Interactable
{
    Renderer renderer;
    Color startColor;

    public override void OnInteract()
    {
        Debug.Log("Button Pressed!");
    }

    public void Start()
    {
        renderer = GetComponent<Renderer>();
        startColor = renderer.material.color;
    }

    public void ToggleButton(bool set)
    {
        renderer.material.color = set ? startColor : Color.black;
    }
}
