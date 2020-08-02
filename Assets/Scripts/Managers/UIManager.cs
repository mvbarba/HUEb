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

    public Slider mouseSlider;
    public Slider soundSlider;
    public Slider musicSlider;
    public Slider fovSlider;

    public TextMeshProUGUI fovText;

    private bool isOpen;

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
}
