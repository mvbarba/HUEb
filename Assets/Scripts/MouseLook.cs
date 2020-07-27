using System;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float cameraShakeIntensity;
    [SerializeField]
    Vector3 maximumPositionShake = Vector3.one * 0.5f;
    private Vector3 orignalCamPosition;

    public float mouseSensitivity = 100f;
	private float verticalRot = 0f;
	public Transform playerBody;
	public float maxRaycastDistance = 10f;
	public bool seesInteractable = false;
	private GameObject hud;
	private PlayerStateManager playerState;
	private DimensionManager dimension;

	public void Start() {
		Cursor.lockState = CursorLockMode.Locked;
		hud = GameObject.Find("HUD");
		playerState = PlayerStateManager.Instance();
		dimension = DimensionManager.Instance();
        orignalCamPosition = this.transform.localPosition;
	}

	public void Update() {

		// Unlock the mouse cursor if "F" is pressed
		if (Input.GetButtonDown("Free Camera")) {
			Cursor.lockState = (Cursor.lockState == CursorLockMode.Locked) ? CursorLockMode.None : CursorLockMode.Locked;
			Cursor.visible = Cursor.lockState == CursorLockMode.None;
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
		if (Physics.Raycast(transform.position, transform.forward, out raycastHit, maxRaycastDistance, LayerMask.GetMask(dimension.currentDimension.ToString(), "Default"))) {
			// Check if the object they're looking at is interactable
			GameObject obj = raycastHit.transform.gameObject;
			playerState.itemSeen = (obj.GetComponent<Interactable>() != null && obj.GetComponent<MeshRenderer>().enabled) ? obj.GetComponent<Interactable>() : null;
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

        // Apply camera shake
        transform.localPosition = CameraShake() + orignalCamPosition;
    }

    public Vector3 CameraShake()
    {
        Vector3 shakeVector = new Vector3(
            maximumPositionShake.x * (Mathf.PerlinNoise(0, Time.time * cameraShakeIntensity) * 2 - 1),
            maximumPositionShake.y * (Mathf.PerlinNoise(1, Time.time * cameraShakeIntensity) * 2 - 1),
            maximumPositionShake.z * (Mathf.PerlinNoise(2, Time.time * cameraShakeIntensity) * 2 - 1)
        );
        return shakeVector;
    }
}
