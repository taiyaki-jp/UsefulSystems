using System.Collections.Generic;
using UnityEngine;
using UsefulSystem.Common;

public class SoundManager : SingletonBase<SoundManager>
{
    [SerializeField] private List<BGMAudioData> _bgmClips = new();
    [SerializeField] private List<JingleAudioData> _jingleClips = new();
    [SerializeField] private List<SEAudioData> _seClips = new();

    private readonly List<AudioSource> _seAudioSource = new();
    private AudioSource _jingleAudioSource;
    private AudioSource _bgmAudioSource;

    protected override void Awake()
    {
        base.Awake();

        _bgmAudioSource = this.transform.Find("BGM").GetComponent<AudioSource>();
        _jingleAudioSource = this.transform.Find("Jingle").GetComponent<AudioSource>();
        _seAudioSource.AddRange(this.transform.Find("SE").GetComponentsInChildren<AudioSource>());
    }

    public void PlayBGM(BGMAudioData.BGMType type)
    {
        var data = _bgmClips.Find(bgm => bgm._type == type);
        if (data == null)
        {
            Debug.LogWarning("SoundManager::PlayBGM::BGM Not Found");
            return;
        }

        _bgmAudioSource.clip = data._audioClip;
        _bgmAudioSource.volume = data._volume;
        _bgmAudioSource.Play();
    }

    public void EndBGM()
    {
        _bgmAudioSource.Stop();
    }

    public void PlaySE(SEAudioData.SEType type)
    {
        var audioSource = _seAudioSource[0];
        var data = _seClips.Find(se => se._type == type);
        if (data == null)
        {
            Debug.LogWarning("SoundManager::PlaySE::SE Not Found");
            return;
        }

        audioSource.clip = data._audioClip;
        audioSource.volume = data._volume;
        audioSource.Play();
    }
}