using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip music;
    public AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        audioSrc.clip = music;
        StartCoroutine(musicPlayer());
    }

    IEnumerator musicPlayer()
    {
        while (true)
        {
            audioSrc.Play();
            yield return new WaitForSeconds(audioSrc.clip.length);
        }
    }

}
