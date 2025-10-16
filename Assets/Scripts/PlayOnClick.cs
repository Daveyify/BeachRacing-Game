using UnityEngine;
using UnityEngine.Audio;

public class PlayOnClick : MonoBehaviour
{
    public AudioSource audioData;
    public AudioClip audioClip;

    void Start()
    {
        audioData = FindFirstObjectByType<AudioSource>();
    }

    public void PlaySound()
    {
        
        audioData.PlayOneShot(audioClip);
    }
}
