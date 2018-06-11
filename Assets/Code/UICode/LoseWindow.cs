using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseWindow : DialogWindow {

    public Button menuBtn;
    public Button retryBtn;

    private string levelName;
    public string LevelName { get { return levelName; } set { levelName = value; } }

    // Use this for initialization
    void Start () {
        backgroundButton.onClick.AddListener(OnCloseButton);
        closeButton.onClick.AddListener(OnCloseButton);

        menuBtn.onClick.AddListener(OnCloseButton);
        retryBtn.onClick.AddListener(Retry);
    }

    public override void OnCloseButton()
    {
        PlayWorld();
        SceneLoadManager.current.ChangeScene("MainMenu");
    }

    void Retry()
    {
        PlayWorld();
        SceneLoadManager.current.ChangeScene(levelName);
    }

    public override DialogWindow Show(GameObject parent)
    {
        GameObject dialog = Instantiate(this.gameObject) as GameObject;
        dialog.transform.SetParent(parent.transform, false);
        PauseWorld();
        return dialog.GetComponent<LoseWindow>();
    }
}
