using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    private static TutorialManager instance;

    private void Awake() {
        instance = !instance ? this : instance;
    }

    public static TutorialManager Instance() {
        return instance;
	}

    [Serializable]
    public struct MaterialRelation {
        public LevelManager.LevelNum levelNum;
        public Material material;
    }

    [SerializeField]
    public MaterialRelation[] tutorialMaterials;

    public void SetTutorial(LevelManager.LevelNum levelNum) {
        foreach (MaterialRelation mr in tutorialMaterials) {
            if (mr.levelNum == levelNum) {
                gameObject.GetComponent<Renderer>().sharedMaterial = mr.material;
			}
		}
	}
    
}
