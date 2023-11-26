using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    public delegate void VolumeChanged(float newVolume);
    public event VolumeChanged OnVolumeChanged;

    public Slider volumeSlider; // Attach your volume slider to this field
    private float volume = 1.0f; // Default volume

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public float GetVolume()
    {
        return volume;
    }

    public void SetVolume(float newVolume)
    {
        volume = newVolume;
    }
}
