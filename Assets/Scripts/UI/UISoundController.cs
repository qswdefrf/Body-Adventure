using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UISoundController : MonoBehaviour
{
    [SerializeField] Slider BGMSlider = null;
    [SerializeField] Slider EffectSlider = null;

    public void Start() {
        BGMSlider.value = SoundManager.Instance.GetVolume(Sound.BGM);
        BGMSlider.onValueChanged.AddListener(SetBGMValue);
        EffectSlider.value = SoundManager.Instance.GetVolume(Sound.Effect);
        EffectSlider.onValueChanged.AddListener(SetEffectValue);
    }
    public void SetBGMValue(float value) {
        SoundManager.Instance.SetVolume(Sound.BGM, value);
        BGMSlider.value = SoundManager.Instance.GetVolume(Sound.BGM);
    }
    public void SetEffectValue(float value) {
        SoundManager.Instance.SetVolume(Sound.Effect, value);
        EffectSlider.value = SoundManager.Instance.GetVolume(Sound.Effect);
    }
}
