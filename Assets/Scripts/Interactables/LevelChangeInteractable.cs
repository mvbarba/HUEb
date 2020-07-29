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
    public Material offMat;
    public Material onMat;
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
        GetComponent<MeshRenderer>().material = this.isOn ? onMat : offMat;
    }
}
