using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinWindow : DialogWindow {

    public Button retryBtn;
    public Button nextBtn;

    public CrystalPanel crystalPanel;
    public FruitPanel fruitPanel;
    public CoinPanel coinPanel;

    private string levelName;
    private int coins;
    private int allFruits;
    private int fruits;
    private bool[] pickedCrystals;

    public string LevelName { get { return levelName; } set { levelName = value; } }
    public int Coins { get { return coins; } set { coins = value; } }
    public int AllFruits { get { return allFruits; } set { allFruits = value; } }
    public int Fruits { get { return fruits; } set { fruits = value; } }
    public bool[] PickedCrystals { get { return pickedCrystals; } set { pickedCrystals = value; } }

    // Use this for initialization
    void Start () {
        backgroundButton.onClick.AddListener(OnCloseButton);
        closeButton.onClick.AddListener(OnCloseButton);

        retryBtn.onClick.AddListener(Retry);
        nextBtn.onClick.AddListener(OnCloseButton);

        crystalPanel.UpdateCrystalPanel(pickedCrystals);
        fruitPanel.UpdatePanel(fruits, allFruits);
        coinPanel.UpdateCoins(coins);
    }

    public override void OnCloseButton()
    {
        PlayWorld();
        SceneLoadManager.current.ChangeScene("ChooseLevel");
    }

    void Retry()
    {
        PlayWorld();
        SceneLoadManager.current.ChangeScene(levelName);
    }

    public override DialogWindow Show(GameObject parentCanvas)
    {
        GameObject dialog = Instantiate(this.gameObject) as GameObject;
        dialog.transform.SetParent(parentCanvas.transform, false);
        PauseWorld();
        return dialog.GetComponent<WinWindow>();
    }
}
