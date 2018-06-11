using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelDoor : MonoBehaviour {

    public SpriteRenderer doorState;

    public Sprite doorLock;
    public Sprite doorCheked;
    public Sprite emptyCrystalSprite;
    public Sprite emptyFruitSprite;
    public Sprite crystalSprite;
    public Sprite fruitSprite;

    public SpriteRenderer crystal;
    public SpriteRenderer fruit;

    public string levelName;
    public int levelNumber;

    private bool rabitCollided = false;
    private HeroRabit rabit;

    private bool isClosed = true;
	// Use this for initialization
	void Start ()
    {
        isClosed = (levelNumber != 1 && !LevelsManager.Instance.PrevLevelWon(levelNumber));
   //     Debug.Log("Is closed " + LevelsManager.Instance.LevelsWin.Count);
        if (isClosed)
            doorState.sprite = doorLock;
        else if (LevelsManager.Instance.LevelWon(levelNumber))
            doorState.sprite = doorCheked;
        else
            doorState.sprite = null;

        LevelStat currLevelStat = LevelsManager.Instance.GetLevelStat(levelNumber);
        if (currLevelStat.hasCrystals)
            crystal.sprite = crystalSprite;
        else
            crystal.sprite = emptyCrystalSprite;

        if (currLevelStat.hasAllFruits)
            fruit.sprite = fruitSprite;
        else
            fruit.sprite = emptyFruitSprite;
    }

    // Update is called once per frame
    void Update () {
        if (rabitCollided)
        {
            if (rabit.IsGrounded)
                SceneLoadManager.current.ChangeScene(levelName);
        }
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!isClosed)
        {
            HeroRabit rabit = collider.GetComponent<HeroRabit>();
            if (rabit != null)
            {
                this.rabit = rabit;
                rabitCollided = true;
                Debug.Log("Rabit collided");
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        rabitCollided = false;
    }
}
