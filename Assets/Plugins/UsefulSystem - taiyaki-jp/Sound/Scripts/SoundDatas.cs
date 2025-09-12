using UnityEngine;

[System.Serializable]
public class BGMAudioData
{
    public enum BGMType
    {
        Title,
        Game,
    }

    public BGMType _type;
    public AudioClip _audioClip;
    [Range(0, 1)] public float _volume = 1;
}
[System.Serializable]
public class JingleAudioData
{
    public enum JingleType
    {
        Button,
        Damage,
        Death,
    }

    public JingleType _type;
    public AudioClip _audioClip;
    [Range(0, 1)] public float _volume = 1;
}

[System.Serializable]
public class SEAudioData
{
    public enum SEType
    {
        Button,
        Damage,
        Death,
    }

    public SEType _type;
    public AudioClip _audioClip;
    [Range(0, 1)] public float _volume = 1;
}
