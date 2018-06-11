using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {

    public static Music music;
    public AudioClip backgroundMusic = null;

    private AudioSource musicSource = null;

    private AudioSource soundSource;

    void Awake()
    {
        if (music != null && music != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            music = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }


    // Use this for initialization
    void Start () {
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();

        soundSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (!SoundManager.Instance.IsMusicOn())
        {
            musicSource.Stop();
        }
        else
        {
            if (!musicSource.isPlaying)
            {
                musicSource.Play();
            }
                
        }
	}

    public void PlayCLip(AudioClip clip)
    {
        soundSource.PlayOneShot(clip);
    }

    void OnApplicationQuit()
    {
        Destroy(this.gameObject);
    }
}
