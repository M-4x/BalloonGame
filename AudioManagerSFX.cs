using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManagerSFX : MonoBehaviour
{
    public AudioSource[] allAudioSources;

    void Start()
    {
        // Set the initial slider value based on saved preferences (if any)
        float savedVolume = SFXManager.instance.GetVolume();
        UpdateVolume(savedVolume);
    }

    void UpdateVolume(float newVolume)
    {
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (var audioSource in allAudioSources)
        {
            if (audioSource != null)
            {
                float volumePercentage = newVolume * 100f;
                Debug.Log(volumePercentage);
                audioSource.volume = newVolume;
            }
        }

        // Save the volume setting
        PlayerPrefs.SetFloat("Volume", newVolume);
    }
}
