using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MyMonoBehaviour
{

    private static SoundManager instance;
    public static SoundManager Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource backgroundAudioSource;
    [SerializeField] private List<SoundList> soundLists = new();

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAudioSource();
        this.LoadBackgroundAudioSource();
        this.InitializeSoundLists();
    }

    private void InitializeSoundLists()
    {
        // Initialize soundLists with correct size based on SoundType enum
        int enumCount = System.Enum.GetValues(typeof(SoundType)).Length;
        soundLists.Clear();

        for (int i = 0; i < enumCount; i++)
        {
            soundLists.Add(new SoundList());
            soundLists[i].soundType = (SoundType)i;
            soundLists[i].OnValidate();
        }
    }

    protected override void LoadValues()
    {
        base.LoadValues();
    }
    private void LoadAudioSource()
    {
        if (audioSource != null) return;
        audioSource = GetComponent<AudioSource>();
    }

    private void LoadBackgroundAudioSource()
    {
        if (backgroundAudioSource != null) return;

        AudioSource[] sources = GetComponents<AudioSource>();
        if (sources.Length > 1)
        {
            backgroundAudioSource = sources[1];
        }
        else
        {
            backgroundAudioSource = gameObject.AddComponent<AudioSource>();
        }

        backgroundAudioSource.loop = true;
        backgroundAudioSource.playOnAwake = false;
        // Debug.Log("Background AudioSource initialized");
    }
    public void PlaySound(SoundType soundType, float volume = 1f)
    {
        // Use background audio source for background music
        if (soundType == SoundType.BackgroundMainMenu || soundType == SoundType.BackgroundGamePlay)
        {
            PlayBackgroundMusic(soundType, volume);
            return;
        }

        this.audioSource.volume = volume;
        AudioClip clip = soundLists[(int)soundType].GetAudioClip();
        if (clip == null) return;
        this.audioSource.PlayOneShot(clip);
    }

    private void PlayBackgroundMusic(SoundType bgType, float volume = 0.5f)
    {
        AudioClip clip = soundLists[(int)bgType].GetAudioClip();
        if (clip == null)
        {
            Debug.LogWarning($"No background music found for {bgType}!");
            return;
        }

        // If already playing the same clip, don't restart
        if (backgroundAudioSource.isPlaying && backgroundAudioSource.clip == clip)
        {
            backgroundAudioSource.volume = volume;
            return;
        }

        backgroundAudioSource.clip = clip;
        backgroundAudioSource.volume = volume;
        backgroundAudioSource.Play();
        // Debug.Log($"Playing background music: {clip.name} ({bgType})");
    }

    public void StopBackgroundMusic()
    {
        if (backgroundAudioSource != null && backgroundAudioSource.isPlaying)
        {
            backgroundAudioSource.Stop();
            // Debug.Log("Background music stopped");
        }
    }

}