using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class AnimationSounds
{
    public string animationName;
    public AudioClip clip;
    public AudioMixerGroup mixer;
    public float volume = 1f;
    public float pitch = 1f;
    public bool loop = false;

    [HideInInspector]
    public AudioSource source;
}
