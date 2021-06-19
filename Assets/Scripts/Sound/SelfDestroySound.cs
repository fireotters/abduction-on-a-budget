using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class SelfDestroySound : MonoBehaviour
{
    // Start is called before the first frame update
    // Update is called once per frame
    [SerializeField] private AudioClip[] sounds;
    
    private void Start()
    {
        if (sounds.Length != 0)
        {
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.clip = sounds[Random.Range(0, sounds.Length)];
            audioSource.Play();
        }
        Invoke(nameof(Bye),4f);
    }

    private void Bye()
    {
        Destroy(gameObject);
    }
}
