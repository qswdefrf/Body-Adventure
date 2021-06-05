using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameSave;
public enum Sound {
    BGM,
    Effect,
    MaxCount
}

public class SoundManager : DontDestroySingletonBehaviour<SoundManager> {
    [SerializeField] AudioSource[] _audioSources = new AudioSource[(int)Sound.MaxCount];
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();
    bool isLoad = false;
    public override void Awake() {
        base.Awake();
        if (isLoad == false) {
            Init();
            Load();
        }
    }
    public void Init() {
        string[] soundNames = Enum.GetNames(typeof(Sound));
        for (int i = 0; i < soundNames.Length - 1; i++) {
            if (_audioSources[i] != null)
                continue;
            GameObject go = new GameObject { name = soundNames[i] };
            _audioSources[i] = go.AddComponent<AudioSource>();
            _audioSources[i].volume = 0.5f;
            go.transform.parent = transform;
        }
        _audioSources[(int)Sound.BGM].loop = true; //
        isLoad = true;
    }

    public void Clear() {
        // 재생기 전부 재생 스탑, 음반 빼기
        foreach (AudioSource audioSource in _audioSources) {
            audioSource.clip = null;
            audioSource.Stop();
        }
        // 효과음 Dictionary 비우기
        _audioClips.Clear();
    }
    public float GetVolume(Sound soundType) {
        return _audioSources[(int)soundType].volume;
    }
    public void SetVolume(Sound soundType, float volume) {
        _audioSources[(int)soundType].volume = volume;
        Debug.Log(_audioSources[(int)soundType].volume);
    }
    public void PlayBGM(AudioClip clip, float pitch) {
        if (clip == null)
            return;
        Sound soundType = Sound.BGM;
        AudioSource audioSource = _audioSources[(int)soundType];
        if (audioSource.isPlaying)
            audioSource.Stop();

        audioSource.pitch = pitch;
        audioSource.clip = clip;
        audioSource.Play();
    }
    public void PlayEffect(AudioClip clip, float pitch, Vector2 pos) {
        if (clip == null)
            return;
        if (!CameraController.Instance.IsInSideCamera(pos, 1)) {
            return;
        }
        Sound soundType = Sound.Effect;
        AudioSource audioSource = _audioSources[(int)soundType];
        audioSource.pitch = pitch;
        audioSource.PlayOneShot(clip);
    }
    private bool Load() {
        SaveData soundData = SaveManager.LoadDeSerailizedData("Option", "OptionData");
        if (soundData != null) {
            string[] soundNames = Enum.GetNames(typeof(Sound));
            for (int i = 0; i < soundNames.Length - 1; i++) {
                SaveData volumeData = soundData.GetData(soundNames[i]);
                float volume = volumeData.GetFloat();
                _audioSources[i].volume = volume;
            }
            SaveManager.SaveSerailizeData("Option", "OptionData", soundData);
            return true;
        }
        return false;
    }
    void Save() {
        SaveData soundData = new SaveData();
        string[] soundNames = Enum.GetNames(typeof(Sound));
        for (int i = 0; i < soundNames.Length - 1; i++) {
            if (_audioSources[i] == null)
                continue;
            SaveData volumeData = new SaveData(_audioSources[i].volume);
            soundData.AddData(soundNames[i], volumeData);
        }
        SaveManager.SaveSerailizeData("Option", "OptionData", soundData);
    }
    ~SoundManager() {
        Save();
    }
}
