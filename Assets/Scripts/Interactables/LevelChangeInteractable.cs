using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChangeInteractable : Interactable
{
    public LevelManager.LevelNum level;

    private Renderer renderer;
    private Color startColor;

    bool canInteract = true;

    public override void OnInteract()
    {
        if (canInteract)
            LevelManager.Instance().StartLevel(level);
    }

    public void Start()
    {
        renderer = GetComponent<Renderer>();
        //startColor = renderer.material.color;
    }

    public void ToggleButton(bool set)
    {
        canInteract = set;
        //renderer.material.color = set ? startColor : Color.black;
    }
}
