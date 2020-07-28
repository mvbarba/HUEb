using UnityEngine;

public class ObjectiveInteractable : Interactable {
	private bool isComplete = false;	

	public override void OnInteract() {
		isComplete = true;
	}

	public bool CheckComplete() {
		return isComplete;
	}
}
