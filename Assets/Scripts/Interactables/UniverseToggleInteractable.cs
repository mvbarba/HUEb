using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniverseToggleInteractable : Interactable
{
    private bool isOn = false;

    public void Start()
    {
        SetOn(false);
    }

    public override void OnInteract() {
        if (isOn)
        {
            DimensionManager.Instance()?.ChangeDimension(color);
            this.SetOn(false);
            foreach (UniverseToggleInteractable button in LevelManager.Instance().dimensionButtons)
            {
                if (button.color != this.color)
                    button.SetOn(true);
            }
        }
	}

    public void SetOn(bool isOn)
    {
        this.isOn = isOn;
        Material mat = new Material(GetComponent<Renderer>().sharedMaterial);
        if (mat)
        {
            UnityEngine.Color uCol = Constants.GetColor(this.color);
            mat.color = uCol;
            mat.SetColor("_EmissionColor", uCol * (isOn ? 2 : 0));
        }
        GetComponent<Renderer>().sharedMaterial = mat;
    }
}
