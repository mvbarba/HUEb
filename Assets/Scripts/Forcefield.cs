using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forcefield : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            DimensionManager.Instance().EnterForcefield();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            DimensionManager.Instance().ExitForcefield();
    }
}
