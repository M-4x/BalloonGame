using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        // Ensure the slider is not null
        if (slider != null)
        {
            // Set the initial value of the slider based on the MusicManager's volume
            slider.value = MusicManager.instance.GetVolume();

            // Subscribe to the MusicManager's SetVolume event
            MusicManager.instance.OnVolumeChanged += UpdateSliderValue;

            // Subscribe to the slider's value changed event
            slider.onValueChanged.AddListener(OnSliderValueChanged);
        }
        else
        {
            Debug.LogError("Slider is not assigned to the VolumeSlider script.");
        }
    }

    private void OnSliderValueChanged(float value)
    {
        // When the slider value changes, update the MusicManager's volume
        MusicManager.instance.SetVolume(value);
    }

    private void UpdateSliderValue(float newVolume)
    {
        // Update the slider value when the MusicManager's volume changes
        slider.value = newVolume;
    }

    private void OnDestroy()
    {
        // Unsubscribe from events to prevent memory leaks
        if (MusicManager.instance != null)
        {
            MusicManager.instance.OnVolumeChanged -= UpdateSliderValue;
        }
    }
}
