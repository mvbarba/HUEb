using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forcefield : MonoBehaviour
{
    private void Start()
    {
        Physics.IgnoreCollision(this.GetComponent<Collider>(), Camera.main.GetComponentInParent<CharacterController>());
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.gameObject.tag == "CameraCenter")
            DimensionManager.Instance().EnterForcefield();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "CameraCenter")
            DimensionManager.Instance().ExitForcefield();
    }
}
