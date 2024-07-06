using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager Instance = null;

    private AudioSource audioSource;

    public SoundEffect[] soundsEffect;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundEffect(string soundEffectName)
    {
        for (int i = 0; i < soundsEffect.Length; i++)
        {
            if (soundsEffect[i].name == soundEffectName)
            {
                audioSource.PlayOneShot(soundsEffect[i].clip);
                return;
            }
        }
    }
}

[System.Serializable]
public class SoundEffect
{
    public string name;
    public AudioClip clip;
}