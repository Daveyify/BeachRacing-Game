using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider FXSlider;
    public AudioMixer audioMixer;

    void Start()
    {
        ResetVolumeValues();
        LoadVolumePref();
        ChangeVolume();
        ChangeMusicVolume();
        ChangeFXVolume();
    }

    public void ResetVolumeValues()
    {
        if (!PlayerPrefs.HasKey("masterVolume"))
        {
            PlayerPrefs.SetFloat("masterVolume", 1);
            PlayerPrefs.SetFloat("musicVolume", 1);
            PlayerPrefs.SetFloat("FXVolume", 1);
        }
    }

    public void SetVolumePref()
    {
        PlayerPrefs.SetFloat("masterVolume", volumeSlider.value);
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("FXVolume", FXSlider.value);
        PlayerPrefs.Save();
    }

    public void LoadVolumePref()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        FXSlider.value = PlayerPrefs.GetFloat("FXVolume");
    }

    public void ChangeVolume()
    {
        audioMixer.SetFloat("VolumeMaster", volumeSlider.value);
        SetVolumePref();
    }

    public void ChangeMusicVolume()
    {
        audioMixer.SetFloat("VolumeMusic", musicSlider.value);
        SetVolumePref();
    }

    public void ChangeFXVolume()
    {
        audioMixer.SetFloat("VolumeFX", FXSlider.value);
        SetVolumePref();
    }
}
