using UnityEngine;

public class ObjectiveInteractable : Interactable {
	private bool isComplete = false;	

	public override void OnInteract() {
		isComplete = true;
		LevelManager.Instance().CheckComplete();
	}

	public bool CheckComplete() {
		return isComplete;
	}
}
