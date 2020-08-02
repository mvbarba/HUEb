using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
	private static PlayerStateManager instance;

	public PickupInteractable itemHeld = null;
	public Interactable itemSeen = null;
	public bool isPlayerFrozen {
		get;
		private set;
	}

	private void Awake() {
		instance = !instance ? this : instance;
	}

	public static PlayerStateManager Instance() {
		return instance;
	}

	public void FreezePlayer(bool frozen) {
		isPlayerFrozen = frozen;
	}

	public void Start() {
		StartCoroutine(FreezePlayerOnStart());
	}

	public IEnumerator FreezePlayerOnStart() {
		FreezePlayer(true);
		yield return new WaitForSeconds(2f);
		FreezePlayer(false);
	}
}
