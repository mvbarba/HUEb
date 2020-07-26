using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    public enum LevelNum
    {
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
        foreach (LevelContainer level in levels)
        {
            if (level.levelNum == levelNum)
            {
                Instantiate(level.levelPrefab, levelParent);
            }
        }
    }

    public void ClearLevels()
    {
        foreach (GameObject child in levelParent.transform)
        {
            Destroy(child);
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
