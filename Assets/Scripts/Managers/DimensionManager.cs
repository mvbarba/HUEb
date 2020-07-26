using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DimensionManager : MonoBehaviour
{
    private static DimensionManager instance;

    private Constants.Color currentDimension = Constants.Color.None;

    public static DimensionManager Instance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }

   public void ChangeDimension(Constants.Color color)
    {
        List<GameObject> objects= LevelManager.Instance().GetLevelObjects();
        foreach (GameObject obj in objects)
        {
            Interactable interactable = obj.GetComponent<Interactable>();
            if (interactable)
            {
                if (interactable.color == Constants.Color.None)
                    continue;
                if (interactable.color == Constants.Color.White)
                    continue;
                if (interactable.color == color){/* do what we do when the color matches*/}
                else {/* do what we do when the colors DONT matches*/}
            }
        }
    }
}
