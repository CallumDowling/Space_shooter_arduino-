using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioController : MonoBehaviour {

    private AudioSource audioSource;
    public AudioClip[] music;
    private AudioClip shootClip;
    int index =0;
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    void Update()
    {
        if (index >= music.Length - 1)
        {
            index = 0;
        }
        if (!audioSource.isPlaying)
        {
            shootClip = music[index];
            audioSource.clip = shootClip;
            audioSource.Play();
            index += 1;
        }
            
        
    }
}
