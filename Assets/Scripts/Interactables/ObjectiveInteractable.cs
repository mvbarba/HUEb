using UnityEngine;

public class ObjectiveInteractable : Interactable {
	private bool isComplete = false;
    protected DissolveScript dissolve;

    private void Start()
    {
        dissolve = GetComponent<DissolveScript>();
    }

    public override void OnInteract() {
        if (!isComplete)
        {
            if (!dissolve.isDissolving)
            {
                isComplete = true;
                AudioManager.Instance().Play("Button");
                LevelManager.Instance().CheckComplete();
            }
        }
	}

	public bool CheckComplete() {
		return isComplete;
	}
}
