using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteAlways]
public abstract class Interactable : MonoBehaviour
{
    public Constants.Color color;
    public abstract void OnInteract();
    public float maxDistance;

    private void Awake()
    {
        Material mat = new Material(GetComponent<Renderer>().sharedMaterial);
        if (mat)
        {
            UnityEngine.Color uCol = Constants.GetColor(this.color);
            mat.color = uCol;
            mat.SetColor("_EmissionColor", uCol * 2);
        }

        GetComponent<Renderer>().sharedMaterial = mat;
    }

    private void OnEnable() {
		Material mat = new Material(GetComponent<Renderer>().sharedMaterial);
		if (mat) {
			UnityEngine.Color uCol = Constants.GetColor(this.color);
			mat.color = uCol;
			mat.SetColor("_EmissionColor", uCol * 2);
		}

		GetComponent<Renderer>().sharedMaterial = mat;
	}
}
