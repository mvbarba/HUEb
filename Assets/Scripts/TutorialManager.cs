using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    private static TutorialManager instance;
    public static TutorialManager Instance() {
        return instance;
    }

    private void Awake() {
        instance = !instance ? this : instance;
    }

    [Serializable]
    public struct MaterialRelation {
        public LevelManager.LevelNum levelNum;
        public Material material;
    }

    [SerializeField]
    public MaterialRelation[] tutorialMaterials;
    public Material defaultMaterial;

    public void SetTutorial(LevelManager.LevelNum levelNum) {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        foreach (MaterialRelation mr in tutorialMaterials) {
            if (mr.levelNum == levelNum) {
                renderer.sharedMaterial = mr.material;
                return;
			}
		}
        renderer.sharedMaterial = defaultMaterial;
	}
    
}
