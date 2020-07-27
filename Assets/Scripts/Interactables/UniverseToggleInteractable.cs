using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniverseToggleInteractable : Interactable
{
	public override void OnInteract() {
		DimensionManager.Instance()?.ChangeDimension(color);
	}
}
