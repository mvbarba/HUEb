using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupInteractable : Interactable
{
    bool isPickedUp = false;
    Transform playerTransform;
    Vector3 pickupPosition;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        pickupPosition = Camera.main.transform.forward * 5f;
        OnInteract();
    }

    public override void OnInteract()
    {
        Debug.Log("Object picked up: " + gameObject.name);
        isPickedUp = true;
        rb.useGravity = false;
        rb.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        if (isPickedUp  && Time.time > 2)
        {
            Vector3 interactionPoint = playerTransform.position + Camera.main.transform.forward * 5f;
            Vector3 posDelta = interactionPoint - transform.position;
            rb.velocity = posDelta * 1000 * Time.fixedDeltaTime;
            transform.rotation = playerTransform.rotation;
        }
    }
}
