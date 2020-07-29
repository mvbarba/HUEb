using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    public Transform directionalLight;
    public LevelChangeInteractable[] levelButtons;
    public GameObject particleParent;

    public float levelTransitionDelay;

    public enum LevelNum
    {
        None,
        Level1,
        Level2,
        Level3,
        Level4
    }

    [System.Serializable]
    public struct LevelContainer
    {
        public GameObject levelPrefab;
        public LevelNum levelNum;
    }

    [SerializeField]
    public LevelContainer[] levels;

    private static LevelManager instance;

    // Where to instantiate levels
    public Transform levelParent;

    private List<GameObject> instantiatedLevels;

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
        StartCoroutine(LevelTransition(levelNum));
    }

    private IEnumerator LevelTransition(LevelNum levelNum)
    {
        AudioManager.Instance().Play("Elevator");
        Camera.main.GetComponent<Animator>().SetTrigger("Shake");
        foreach (LevelChangeInteractable button in levelButtons)
            button.SetOn(false);
        ClearLevels();
        particleParent.SetActive(true);
        directionalLight.GetComponent<Animator>().SetTrigger("LightDown");
        yield return new WaitForSeconds(levelTransitionDelay);

        AudioManager.Instance().Play("Ding");
        Instantiate(GetLevel(levelNum), levelParent);
        //At some point, we need to only enable the buttons for levels that are unlocked here
        foreach (LevelChangeInteractable button in levelButtons)
            button.SetOn(true);
        particleParent.SetActive(false);
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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
