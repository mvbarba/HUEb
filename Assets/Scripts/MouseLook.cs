using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
	public float mouseSensitivity = 100f;
	private float verticalRot = 0f;
	public Transform playerBody;
	public LayerMask raycastLayer;
	public float maxRaycastDistance = 10f;
	public bool seesInteractable = false;
	private GameObject hud;
	private PlayerStateManager playerState;

	public void Start() {
		Cursor.lockState = CursorLockMode.Locked;
		hud = GameObject.Find("HUD");
		playerState = PlayerStateManager.Instance();
	}

	public void Update() {

		// Unlock the mouse cursor if "F" is pressed
		if (Input.GetButtonDown("Free Camera")) {
			Cursor.lockState = (Cursor.lockState == CursorLockMode.Locked) ? CursorLockMode.None : CursorLockMode.Locked;
		}
		
		// Only move the character's view if the cursor is locked in
		if (Cursor.lockState == CursorLockMode.Locked) {
			float mx = Input.GetAxis("Mouse X");
			float my = Input.GetAxis("Mouse Y");

			mx *= mouseSensitivity * Time.deltaTime;
			my *= mouseSensitivity * Time.deltaTime;

			verticalRot -= my;
			verticalRot = Mathf.Clamp(verticalRot, -90f, 90f);

			transform.localRotation = Quaternion.Euler(verticalRot, 0f, 0f);
			playerBody.Rotate(Vector3.up * mx);
		}

		// Check if the player sees an interactable
		RaycastHit raycastHit;
		if (Physics.Raycast(transform.position, transform.forward, out raycastHit, maxRaycastDistance, raycastLayer)) {
			// Check if the object they're looking at is interactable
			GameObject obj = raycastHit.transform.gameObject;
			playerState.itemSeen = (obj.GetComponent<Interactable>() != null) ? obj.GetComponent<Interactable>() : null;
		} else {
			playerState.itemSeen = null;
		}

		// Check if they want to interact with the object
		if (Input.GetButtonDown("Interact")) {
			if (playerState.itemHeld != null) {
				playerState.itemHeld.Drop();
			} else if (playerState.itemSeen != null) {
				playerState.itemSeen.OnInteract();
			}
		}

		// Update the HUD
		hud.GetComponent<Animator>().SetBool("SeesInteractable", playerState.itemSeen != null && playerState.itemHeld == null);
	}
}
