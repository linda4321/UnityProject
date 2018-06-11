using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance;

    private bool is_sound_on = true;
    private bool is_music_on = true;

    void Awake()
    {
        Instance = this;

        is_sound_on = (PlayerPrefs.GetInt("sound", 1) == 1);
        is_music_on = (PlayerPrefs.GetInt("music", 1) == 1);
    }

    public bool IsSoundOn()
    {  
        return this.is_sound_on;
    }

    public bool IsMusicOn()
    {
        return this.is_music_on;
    }

    public void SetSoundOn(bool val)
    {
        this.is_sound_on = val;
        PlayerPrefs.SetInt("sound", this.is_sound_on ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetMusicOn(bool val)
    {
        this.is_music_on = val;
        PlayerPrefs.SetInt("music", this.is_music_on ? 1 : 0);
        PlayerPrefs.Save();
    }

}
