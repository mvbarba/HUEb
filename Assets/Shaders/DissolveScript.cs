using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveScript : MonoBehaviour
{
    private bool dissolve;
    private bool isDissolving;
    private Renderer rend;
    private float lerp;

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    public void SetDissolve(bool setDissolve)
    {
        dissolve = setDissolve;
        isDissolving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDissolving)
        {
            if (dissolve)
            {
                lerp += Time.deltaTime;
            }
            else
            {
                lerp -= Time.deltaTime;
            }
            lerp = Mathf.Clamp(lerp, 0, 1); 
            rend.material.SetFloat("_Amount", lerp);
            if (lerp == 0 || lerp == 1)
                isDissolving = false;
        }
    }
}
