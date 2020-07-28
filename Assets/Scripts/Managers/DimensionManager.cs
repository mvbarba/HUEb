using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Rendering.PostProcessing;

public class DimensionManager : MonoBehaviour
{
    private static DimensionManager instance;

    private Camera mainCamera;

    public Constants.Color currentDimension {
        get;
        private set;
    } = Constants.Color.None;

    [System.Serializable]
    public struct ProfileContainer
    {
        public PostProcessProfile profile;
        public Constants.Color dimension;
        public Material skybox;
    }

    [SerializeField]
    public ProfileContainer[] profileContainers;

    public static DimensionManager Instance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public void ChangeDimension(Constants.Color color)
    {
        currentDimension = color;
        
        List<GameObject> objects= LevelManager.Instance().GetLevelObjects();
        foreach (GameObject obj in objects)
        {
            Interactable interactable = obj.GetComponent<Interactable>();
            if (interactable)
            {
                bool objectVisible = interactable.color == Constants.Color.White || interactable.color == color;
                obj.GetComponent<MeshRenderer>().enabled = objectVisible;
                Physics.IgnoreCollision(obj.GetComponent<Collider>(), PlayerStateManager.Instance().gameObject.GetComponent<CharacterController>(), !objectVisible);
            }
        }

        foreach (ProfileContainer container in profileContainers)
        {
            if (container.dimension == currentDimension)
            {
                mainCamera.GetComponent<PostProcessVolume>().profile = container.profile;
                mainCamera.GetComponent<Skybox>().material = container.skybox;
                break;
            }
        }
    }

    public void EnterForcefield()
    {
        EnableAllRenderers(true);
    }

    public void ExitForcefield()
    {
        ChangeDimension(currentDimension);
    }

    private void EnableAllRenderers(bool set)
    {
        List<GameObject> objects = LevelManager.Instance().GetLevelObjects();
        foreach (GameObject obj in objects)
        {
            Interactable interactable = obj.GetComponent<Interactable>();
            if (interactable)
            {
                obj.GetComponent<MeshRenderer>().enabled = set;
            }
        }
    }
}
