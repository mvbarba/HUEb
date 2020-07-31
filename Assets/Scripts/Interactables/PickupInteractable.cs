using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupInteractable : Interactable
{
	bool isPickedUp = false;
	Transform playerTransform;
	Vector3 pickupPosition;
	Rigidbody rb;
	PlayerStateManager playerState;
	Camera mainCamera;
	float pickupDistance;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		playerState = PlayerStateManager.Instance();
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		pickupPosition = Camera.main.transform.forward * 5f;
		mainCamera = Camera.main;
		//pickupDistance = mainCamera.GetComponent<MouseLook>().maxRaycastDistance * 0.9f;
		//OnInteract();
		gameObject.layer = Constants.GetLayerMask(this.color);
	}


	public override void OnInteract()
	{
		Debug.Log("Object picked up: " + gameObject.name);
		playerState.itemHeld = this;
        pickupDistance = maxDistance * 0.8f;
        isPickedUp = true;
		rb.useGravity = false;
		rb.freezeRotation = true;
	}

	public void Drop() {
		playerState.itemHeld = null;
		isPickedUp = false;
		rb.useGravity = true;
		rb.freezeRotation = false;
	}

	private void FixedUpdate()
	{
		if (isPickedUp)
		{
			Vector3 interactionPoint = mainCamera.transform.position + mainCamera.transform.forward * pickupDistance;
			Vector3 posDelta = interactionPoint - transform.position;
			rb.velocity = posDelta * 1000 * Time.fixedDeltaTime;
			transform.rotation = playerTransform.rotation;
		}
	}
}
