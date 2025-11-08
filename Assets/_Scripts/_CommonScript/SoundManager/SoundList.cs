using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SoundList
{
    [HideInInspector] public string sName;
    [HideInInspector] public SoundType soundType;
    [SerializeField] private List<AudioClip> audioClips = new();

    private void LoadAudioClips()
    {
        audioClips.Clear();
        string path = "_Sound/" + this.sName;
        AudioClip[] clips = Resources.LoadAll<AudioClip>(path);

        if (clips.Length > 0)
        {
            audioClips.AddRange(clips);
            Debug.Log($"Loaded {clips.Length} audio clips from {path}");
        }
        else
        {
            Debug.LogWarning($"No audio clips found at Resources/{path}");
        }
    }
    public AudioClip GetAudioClip()
    {
        if (audioClips.Count == 0) return null;
        int index = UnityEngine.Random.Range(0, audioClips.Count);
        return audioClips[index];
    }

    public void OnValidate()
    {
        this.sName = soundType.ToString();
        this.LoadAudioClips();
    }

}