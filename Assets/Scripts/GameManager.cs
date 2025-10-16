using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
    public static bool isPaused = false;
    public static GameManager Instance;
    public TMP_InputField usernameInput;
    public Button submitButton;
    public GameObject PauseMenu;

    public AudioSource audioData;
    public AudioMixer audioMixer;
    public Animator panelAnimator;

    void Start()
    {
        if (PauseMenu != null)
        {
            PauseMenu.SetActive(false);
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    public static void Pause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;
            Instance.PauseMenu.SetActive(true);
            Instance.audioMixer.SetFloat("VolumeMusic", -80);
            Instance.panelAnimator.SetTrigger("Open");
            Cursor.visible = true;

        }
        else
        {
            Time.timeScale = 1;
            Instance.PauseMenu.SetActive(false);
            GameManager.Instance.audioMixer.SetFloat("VolumeMusic", PlayerPrefs.GetFloat("musicVolume"));
            Instance.panelAnimator.SetTrigger("Close");
            Cursor.visible = false;
        }
    }
    public void LoadScene(string myScene)
    {
        SceneManager.LoadScene(myScene);
    }

    public void SetUsernamePref()
    {
        var username = usernameInput.text;
        PlayerPrefs.SetString("username", username);
        Debug.Log("Username saved succesfully");
    }

    public void ValidateButton()
    {
        var username = usernameInput.text;
        Debug.Log(username);

        if (!string.IsNullOrEmpty(username) && username.Length >= 4)
        {
            submitButton.interactable = true;
        }
        else
        {
            submitButton.interactable = false;
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

}
