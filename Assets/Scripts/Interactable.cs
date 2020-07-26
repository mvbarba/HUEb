using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{

    public Constants.Color color;

    public abstract void OnInteract();


    /* In update, check if our raycast is hitting this object
     * 
     * If we press interact button, run OnInteract()
     */

    private void Update()
    {
        if (false)
        {
            OnInteract();
        }
    }
}
