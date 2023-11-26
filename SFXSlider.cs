using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXSlider : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        // Ensure the slider is not null
        if (slider != null)
        {
            slider.value = SFXManager.instance.GetVolume();

            SFXManager.instance.OnVolumeChanged += UpdateSliderValue;

            slider.onValueChanged.AddListener(OnSliderValueChanged);
        }
        else
        {
            Debug.LogError("Slider is not assigned to the VolumeSlider script.");
        }
    }

    private void OnSliderValueChanged(float value)
    {
        SFXManager.instance.SetVolume(value);
    }

    private void UpdateSliderValue(float newVolume)
    {
        slider.value = newVolume;
    }

    private void OnDestroy()
    {
        // Unsubscribe from events to prevent memory leaks
        if (SFXManager.instance != null)
        {
            SFXManager.instance.OnVolumeChanged -= UpdateSliderValue;
        }
    }
}
