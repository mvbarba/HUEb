using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
	public float mouseSensitivity = 100f;
	private float verticalRot = 0f;
	public Transform playerBody;

	public void Start() {
		Cursor.lockState = CursorLockMode.Locked;
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
	}
}
