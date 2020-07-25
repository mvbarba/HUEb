using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public CharacterController controller;
	public float movementSpeed = 12f;

	public void Update() {
		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");

		Vector3 delta = transform.right * x + transform.forward * z;
		controller.Move(delta * movementSpeed * Time.deltaTime);
	}
}
