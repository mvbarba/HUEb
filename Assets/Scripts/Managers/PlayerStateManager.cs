using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
	private static PlayerStateManager instance;

	public PickupInteractable itemHeld = null;
	public Interactable itemSeen = null;

	private void Awake() {
		instance = !instance ? this : instance;
	}

	public static PlayerStateManager Instance() {
		return instance;
	}
}
