using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sources")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Header("Clips")]
    public List<AudioClip> bgmClips;
    public List<AudioClip> sfxClips;

    private Dictionary<string, AudioClip> _bgmDict;
    private Dictionary<string, AudioClip> _sfxDict;

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Init();
        } else {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        PlayBGM("Main");
    }

    void Init()
    {
        _bgmDict = new Dictionary<string, AudioClip>();
        foreach (var clip in bgmClips)
            _bgmDict[clip.name] = clip;

        _sfxDict = new Dictionary<string, AudioClip>();
        foreach (var clip in sfxClips)
            _sfxDict[clip.name] = clip;
    }

    // ====== Public APIs ======
    public void PlayBGM(string name, bool loop = true)
    {
        if (_bgmDict.TryGetValue(name, out var clip)) {
            bgmSource.clip = clip;
            bgmSource.loop = loop;
            bgmSource.Play();
        } else {
            Debug.LogWarning($"BGM '{name}' not found");
        }
    }

    public void StopBGM() => bgmSource.Stop();

    public void PlaySFX(string name)
    {
        if (_sfxDict.TryGetValue(name, out var clip)) {
            sfxSource.PlayOneShot(clip);
        } else {
            Debug.LogWarning($"SFX '{name}' not found");
        }
    }

    public void SetBGMVolume(float volume) => bgmSource.volume = volume;
    public void SetSFXVolume(float volume) => sfxSource.volume = volume;
}