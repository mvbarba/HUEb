using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopInHandler : MonoBehaviour
{
	public float popInRate = 0.15f;

	public void OnEnable() {
		StartCoroutine(PopInChildren());
	}

	private IEnumerator PopInChildren() {
		// Disable all the children
		foreach (Transform child in transform) {
			child.gameObject.SetActive(false);
		}

		// Re-enable each child after waiting the defined time
		foreach (Transform child in transform) {
			yield return new WaitForSeconds(popInRate);
			child.gameObject.SetActive(true);
		}
	}
}
