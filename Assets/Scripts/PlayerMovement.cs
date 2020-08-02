using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerMovement : MonoBehaviour
{
    private static PlayerMovement instance;

    public static PlayerMovement Instance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }

    public Transform groundCheck;
	public CharacterController controller;
	public float movementSpeed = 12f;
	public float groundDistance = 0.4f;
	public float gravity = -12f;
	public float jumpForce = 3f;
	public LayerMask groundMask;
	
	private bool isGrounded = false;
	private Vector3 velocity;
	private DimensionManager dimension;

    private AudioManager audio;

	private void Start() {
		dimension = DimensionManager.Instance();
        audio = AudioManager.Instance();

    }

	public void Update() {

		// Check if our character is on the ground
		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

		// If the player is on the ground, set their yvel to 0;
		if (isGrounded && velocity.y < 0) {
			velocity.y = 0f;
		}
		
		// If the player is on the ground, let them jump if they wish
		if (Input.GetButtonDown("Jump") && isGrounded) {
			velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
		}

		// Check for universe switching
		if (!dimension.locked) {
			if (Input.GetButtonDown("RedU")) {
				if (dimension.ChangeDimension(Constants.Color.Red, !dimension.inForcefield))
                    audio.PlayRandomSwitch();
			} else if (Input.GetButtonDown("GreenU")) {
                if (dimension.ChangeDimension(Constants.Color.Green, !dimension.inForcefield))
                    audio.PlayRandomSwitch();
            } else if (Input.GetButtonDown("BlueU")) {
                if (dimension.ChangeDimension(Constants.Color.Blue, !dimension.inForcefield))
                    audio.PlayRandomSwitch();
            }
		}

		// Get the player WASD input
		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");

		// Move the player according to their WASD input
		Vector3 delta = transform.right * x + transform.forward * z;
		controller.Move(delta * movementSpeed * Time.deltaTime);

		// Apply gravity, dY = 1/2(g) * t^2
		velocity.y += gravity * Time.deltaTime;
		controller.Move(velocity * Time.deltaTime);
    }
}
