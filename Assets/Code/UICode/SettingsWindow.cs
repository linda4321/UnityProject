using UnityEngine;
using UnityEngine.UI;

public class SettingsWindow : DialogWindow {
    public GameObject sound;
    public GameObject music;

    public Sprite soundOn;
    public Sprite musicOn;
    public Sprite soundOff;
    public Sprite musicOff;

    private Image soundIm;
    private Image musicIm;

    
    // Use this for initialization
    void Start () {
        backgroundButton.onClick.AddListener(OnCloseButton);
        closeButton.onClick.AddListener(OnCloseButton);

        sound.GetComponent<Button>().onClick.AddListener(OnSound);
        music.GetComponent<Button>().onClick.AddListener(OnMusic);

        soundIm = sound.GetComponent<Image>();
        musicIm = music.GetComponent<Image>();

        UpdateSoundSprite();
        UpdateMusicSprite();
        //soundOn = Resources.Load<Sprite>("Content/UI/popups/settings/sound-on");
        //musicOn = Resources.Load<Sprite>("music-on");
        //musicOff = Resources.Load<Sprite>("music-off");
        //soundOff = Resources.Load<Sprite>("Content/UI/popups/settings/sound-off");
    }


    public void OnSound()
    {
        SoundManager.Instance.SetSoundOn(!SoundManager.Instance.IsSoundOn());
        UpdateSoundSprite();
    }

    public void OnMusic()
    {
        SoundManager.Instance.SetMusicOn(!SoundManager.Instance.IsMusicOn());
        UpdateMusicSprite();
    }

    private void UpdateSoundSprite()
    {
        if (SoundManager.Instance.IsSoundOn())
            soundIm.sprite = soundOn;
        else
            soundIm.sprite = soundOff;
    }

    private void UpdateMusicSprite()
    {
        if (SoundManager.Instance.IsMusicOn())
            musicIm.sprite = musicOn;
        else
            musicIm.sprite = musicOff;
    }
}
