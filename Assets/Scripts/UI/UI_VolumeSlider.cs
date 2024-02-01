using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_VolumeSlider : MonoBehaviour
{
    public Slider slider;
    public string parameter;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private float multiplier;

    public void SliderValue(float _value)
    {
        _value = slider.value;
        audioMixer.SetFloat(parameter, _value * multiplier);
    }

    public void LoadSlider(float _value)
    {
        if(_value >= -1f)
            slider.value = _value;
    }
}
