using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AttachedSoundEffect : MonoBehaviour
{
    private AudioSource _audSrc;
    public AudioClip[] soundsToChooseFrom;

    private void Awake()
    {
        _audSrc = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        _audSrc.clip = soundsToChooseFrom[Random.Range(0, soundsToChooseFrom.Length)];
        _audSrc.Play();
    }
}
