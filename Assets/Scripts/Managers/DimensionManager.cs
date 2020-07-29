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

    string[] layers;

    public void ChangeDimension(Constants.Color color, bool forcefield = false)
    {
        switch (color)
        {
            case Constants.Color.White:
            case Constants.Color.None:
                {
                    layers = new string[] { "Default", "Ground" };
                    break;
                }
            case Constants.Color.Red:
            case Constants.Color.Blue:
            case Constants.Color.Green:
                {
                    layers = new string[] { "Default", "Ground", "White", color.ToString() };
                    break;
                }
            default:
                {
                    break;
                }
        }

        PlayerMovement.Instance().groundMask = LayerMask.GetMask(layers);

        // Don't change current dimension when we are entering the forcefield
        if (color != Constants.Color.White)
            currentDimension = color;
        if (forcefield)
        {
            List<GameObject> objects = LevelManager.Instance().GetLevelObjects();
            foreach (GameObject obj in objects)
            {
                Interactable interactable = obj.GetComponent<Interactable>();
                if (interactable)
                {
                    bool objectVisible = (interactable.color == Constants.Color.White || interactable.color == color || color == Constants.Color.White) && color != Constants.Color.None;
                    Physics.IgnoreCollision(obj.GetComponent<Collider>(), PlayerStateManager.Instance().gameObject.GetComponent<CharacterController>(), !objectVisible);

                    DissolveScript dissolve = obj.GetComponent<DissolveScript>();
                    if (dissolve)
                    {
                        dissolve.SetDissolve(!objectVisible);
                    }
                }
            }

        }
        if (color != Constants.Color.White)
        {
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
    }

    public void EnterForcefield()
    {
        ChangeDimension(Constants.Color.White, true);
    }

    public void ExitForcefield()
    {
        ChangeDimension(currentDimension, true);
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
