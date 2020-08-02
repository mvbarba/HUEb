using UnityEngine;

public class ObjectiveInteractable : Interactable {
	private bool isComplete = false;	

	public override void OnInteract() {
        if (!isComplete)
        {
            isComplete = true;
            AudioManager.Instance().Play("Button");
            LevelManager.Instance().CheckComplete();
        }
	}

	public bool CheckComplete() {
		return isComplete;
	}
}
