using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

	public static LevelController current;

    public AudioClip winLevel;
    public AudioClip loseLevel;

	public Vector3 rabitStartPos;

    public LifePanel lifePanel;
    public CoinPanel coinPanel;
    public CrystalPanel crystalPanel;
    public FruitPanel fruitPanel;

    public WinWindow winWindow;
    public LoseWindow loseWindow;

    public int level;
    public string levelName;

    private GameObject canvas;

	private int coins = 0;
	private int fruits = 0;
	private int crystals = 0;
	private int mushrooms = 0;
	private int bombs = 0;
    private bool[] pickedCrystals = new bool[3];
    private List<Vector3> pickedFruits = new List<Vector3>();
    private int allFruitsOnLevel = 0;
    private GameObject[] allFruits;

    private LevelStat levelStat;
    public LevelStat LevelStatictic { get { return levelStat; } }

    void Awake() {
        
		current = this;
        if(level > 0)
            levelStat = LevelsManager.Instance.GetLevelStat(level);
        canvas = GameObject.Find("Canvas");

        allFruits = GameObject.FindGameObjectsWithTag("Fruit");
        Color fruitColor;
        foreach (GameObject fruit in allFruits)
        {
            if (levelStat.collectedFruits.Contains(fruit.transform.position))
            {
                Debug.Log("Fruit opacity");
                fruitColor = fruit.GetComponent<SpriteRenderer>().color;
                fruitColor.a = 0.5f;
                fruit.GetComponent<SpriteRenderer>().color = fruitColor;
            }
        }

        allFruitsOnLevel = allFruits.Length;
        if (fruitPanel != null)
            fruitPanel.SetAllFruitsLabel(allFruitsOnLevel);
    }

	public void SetStartPosition(Vector3 position) {
		rabitStartPos = position;
	}

	public void OnRabitDeath(HeroRabit rabit){
		rabit.transform.position = rabitStartPos;
        if (lifePanel != null)
        {
            rabit.MinusLife();
            lifePanel.MinusLife(rabit.Lifes);
            if (rabit.Lifes == 0)
            {
                OnPlayerLose();              
            }
        }
    }

    public void OnPlayerLose()
    {
        if (SoundManager.Instance.IsSoundOn())
            Music.music.PlayCLip(loseLevel);
  //      levelStat.collectedFruits.Clear();
        LevelsManager.Instance.SaveLevelStat(levelStat, level);

        LoseWindow currWindow = (LoseWindow)loseWindow.Show(canvas);
        currWindow.LevelName = levelName;
    }

    public void OnPlayerWin()
    {
        if (SoundManager.Instance.IsSoundOn())
            Music.music.PlayCLip(winLevel);
        levelStat.levelPassed = true;

        if (AllCrystalsPicked())
            levelStat.hasCrystals = true;

        if (fruits == allFruitsOnLevel)
            levelStat.hasAllFruits = true;

        levelStat.collectedFruits = pickedFruits;

        LevelsManager.Instance.SaveLevelStat(levelStat, level);
        LevelsManager.Instance.AddCoins(this.coins);
     
        WinWindow currWindow = (WinWindow)winWindow.Show(canvas);
        currWindow.LevelName = levelName;
        currWindow.PickedCrystals = pickedCrystals;
        currWindow.Coins = coins;
        currWindow.AllFruits = allFruitsOnLevel;
        currWindow.Fruits = fruits;
    }

    public bool AllCrystalsPicked()
    {
        foreach (bool crystal in pickedCrystals)
            if (!crystal)
                return false;
        return true;
    }

    public void AddCoins(int coins){
		this.coins += coins;
		Debug.Log ("Coin added");
        coinPanel.UpdateCoins(this.coins);
	}

	public void AddFruits(int fruits, Vector3 fruitPos){
		this.fruits += fruits;
		Debug.Log ("Fruit added");
        fruitPanel.UpdatePanel(this.fruits);

        if (!pickedFruits.Contains(fruitPos))
            pickedFruits.Add(fruitPos);
	}

	public void AddCrystals(string crystalType, int crystals){
		this.crystals += crystals;
		Debug.Log ("Crystal added");
        switch (crystalType)
        {
            case "BlueCrystal":
                pickedCrystals[0] = true;
                break;
            case "GreenCrystal":
                pickedCrystals[1] = true;
                break;
            case "RedCrystal":
                pickedCrystals[2] = true;
                break;
            default:
                break;
        }
        crystalPanel.AddCrystal(crystalType);
    }

	public void AddMushrooms(int mushrooms){
		this.mushrooms += mushrooms;
		Debug.Log ("Mushroom added");
	}

	public void AddBombs(int bombs){
		this.bombs += bombs;
		Debug.Log ("Bomb added");
	}

}
