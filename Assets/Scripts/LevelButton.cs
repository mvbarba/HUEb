using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelButton : MonoBehaviour
{
    private TextMeshPro label;
    public int levelNumber = 0;
    public Material offMat;
    public Material onMat;
    private bool isOn = false;
    public bool startOn = false;


    private void Start() {
        label = GetComponentInChildren<TextMeshPro>();
        label.text = levelNumber.ToString();
        isOn = startOn;
        SetOn(isOn);
	}

    public void SetOn(bool isOn) {
        this.isOn = isOn;
        GetComponent<MeshRenderer>().material = this.isOn ? onMat : offMat;
	}
}
