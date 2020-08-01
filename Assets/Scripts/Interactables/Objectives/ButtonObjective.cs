using UnityEngine;

public class ButtonObjective : ObjectiveInteractable {
	public override void OnInteract() {
		base.OnInteract();

		// Turn the complete light on
		Transform completeLight = transform.parent.Find("Complete Light");
        Renderer clRenderer = completeLight.GetComponent<Renderer>();
        Material mat = new Material(clRenderer.sharedMaterial);
        if (mat) {
            mat.SetColor("_EmissionColor", mat.color * 6);
            clRenderer.sharedMaterial = mat;
        }
    }
}
