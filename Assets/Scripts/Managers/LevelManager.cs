using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    public Transform directionalLight;
    public GameObject particleParent;
    public GameObject forcefield;
    private List<Collider> forcefieldColliders;
    private PlayerMovement player;
    private LevelNum currentLevelNum = LevelNum.Level1;

    public float levelTransitionDelay;

    public enum LevelNum
    {
        None,
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
        Level6
    }

    [System.Serializable]
    public struct LevelContainer
    {
        public GameObject levelPrefab;
        public LevelNum levelNum;
        public LevelChangeInteractable button;
    }

    [SerializeField]
    public LevelContainer[] levels;

    private static LevelManager instance;

    // Where to instantiate levels
    public Transform levelParent;

    private List<GameObject> instantiatedLevels;

    // Buttons for changing dimensions
    public UniverseToggleInteractable[] dimensionButtons;

    public static LevelManager Instance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }

    public void StartLevel(LevelNum levelNum)
    {
        currentLevelNum = levelNum;
        StartCoroutine(LevelTransition(levelNum));
    }

    private IEnumerator LevelTransition(LevelNum levelNum)
    {
        DimensionManager dimension = DimensionManager.Instance();
        dimension.locked = true;
        List<GameObject> objects = GetLevelObjects();
        foreach (GameObject obj in objects)
        {
            if (obj.tag == "DestroyOnLoad")
            {
                Destroy(obj);
            }
        }

        PlayerPrefs.SetInt(levelNum.ToString(), 1);
        ToggleForcefield(false);
        float movementSpeed = player.movementSpeed;
        player.movementSpeed = 0f;
        AudioManager.Instance().Play("Elevator");
        Camera.main.GetComponent<Animator>().SetTrigger("Shake");
        foreach (LevelContainer level in levels)
            level.button.SetOn(false);
        particleParent.SetActive(true);
        directionalLight.GetComponent<Animator>().SetTrigger("LightDown");
        DimensionManager.Instance().ChangeDimension(Constants.Color.None, true);

        yield return new WaitForSeconds(levelTransitionDelay);

        player.movementSpeed = movementSpeed;
        ClearLevels();
        ToggleForcefield(true);
        dimension.ChangeDimension(Constants.Color.White, true);
        AudioManager.Instance().Play("Ding");
        Instantiate(GetLevel(levelNum), levelParent);
        //At some point, we need to only enable the buttons for levels that are unlocked here
        UpdateLevelButtonLights();
        particleParent.SetActive(false);
        dimension.locked = false;
        yield break;
    }

    public GameObject GetLevel(LevelNum levelNum)
    {
        foreach (LevelContainer level in levels)
        {
            if (level.levelNum == levelNum)
            {
                return level.levelPrefab;
            }
        }
        return null;
    }

    public void ClearLevels()
    {
        foreach (Transform child in levelParent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    // Gets a list of every gameobject under the levelParent gameobject
    public List<GameObject> GetLevelObjects()
    {
        List<GameObject> objects = new List<GameObject>();
        Interactable[] interactableList = levelParent.GetComponentsInChildren<Interactable>();
        foreach (Interactable interactable in interactableList)
            objects.Add(interactable.gameObject);
        return objects;
    }

    public void ToggleForcefield(bool set)
    {
        foreach (Collider col in forcefieldColliders)
        {
            //col.enabled = set;
            col.gameObject.SetActive(set);
        }
    }

    private void UpdateLevelButtonLights() {
        foreach (LevelContainer level in levels) {
            level.button.SetOn(PlayerPrefs.GetInt(level.levelNum.ToString()) == 1); 
        }
    }

    public bool CheckComplete() {
        Transform currentLevel = instance.levelParent;
        ObjectiveInteractable[] objectives = currentLevel.gameObject.GetComponentsInChildren<ObjectiveInteractable>();
        foreach (ObjectiveInteractable o in objectives) {
            if (!o.CheckComplete()) {
                return false;
			}
		}

        // At this point, we can assume the level is complete
        PlayerPrefs.SetInt((currentLevelNum + 1).ToString(), 1);
        UpdateLevelButtonLights();
        DimensionManager dimension = DimensionManager.Instance();
        dimension.ChangeDimension(Constants.Color.None, true);
        dimension.locked = true;
        return true;
	}

    // Start is called before the first frame update
    void Start()
    {
        forcefieldColliders = new List<Collider>();
        foreach (Collider col in forcefield.GetComponentsInChildren<Collider>())
        {
            if (col.gameObject.name != "Hitbox")
                forcefieldColliders.Add(col);
        }
        player = PlayerMovement.Instance();

    }


}
