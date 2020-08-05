using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private MouseLook mouse;

    public GameObject about;
    public GameObject settings;
    public GameObject controls;

    public Slider mouseSlider;
    public Slider soundSlider;
    public Slider musicSlider;
    public Slider fovSlider;

    public TextMeshProUGUI fovText;
    public GameObject endScreen;
    private bool isOpen;

    private static UIManager instance;

	private void Awake() {
        instance = !instance ? this : instance;
	}

    public static UIManager Instance() {
        return instance;
	}

	private void Start()
    {
        mouse = Camera.main.GetComponent<MouseLook>();
    }

    public void OpenAbout()
    {
        about.SetActive(true);
        settings.SetActive(false);
    }

    public void OpenSettings()
    {
        isOpen = true;
        about.SetActive(false);
        settings.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseUI()
    {
        about.SetActive(false);
        settings.SetActive(false);
        controls.SetActive(false);
        isOpen = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ToggleUI()
    {
        if (isOpen)
            CloseUI();
        else
            OpenSettings();
    }

    public void ChangeMusicVolume()
    {
        AudioManager.Instance().ChangeMusicVolume(musicSlider.value);
    }

    public void ChangeVolume()
    {
        AudioManager.Instance().ChangeVolume(soundSlider.value);
    }

    public void ChangeSensitivity()
    {
        mouse.mouseSensitivity = mouseSlider.value;
    }

    public void ChangeFOV()
    {
        Camera.main.fieldOfView = fovSlider.value;
        fovText.text = fovSlider.value.ToString();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenControls()
    {
        settings.SetActive(false);
        controls.SetActive(true);
    }
}
