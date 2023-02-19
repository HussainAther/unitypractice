using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour {
    [SerializeField] private Slider _volumeSlider;
    public void OnVolumeChanged()
    {
        _volumeSlider.value = AudioListener.volume;
        Debug.Log("Volume is being adjusted!");
    }
}