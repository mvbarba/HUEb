using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class LevelChangeInteractable : Interactable
{
    public LevelManager.LevelNum level;

    public override void OnInteract()
    {
        if (isOn)
            LevelManager.Instance().StartLevel(level);
    }

    private TextMeshPro label;
    private bool isOn = false;
    public bool startOn = false;

    private void Start()
    {
        label = GetComponentInChildren<TextMeshPro>();
        int levelInt = (int)level;
        label.text = levelInt.ToString();
        isOn = startOn;
        SetOn(isOn);
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
