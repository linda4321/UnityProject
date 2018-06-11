using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour {

    public static LevelsManager Instance;
    public int levelsNumber;

    private int allCoins = 0;

    private List<LevelStat> levelsStatistic;
    public int AllCoins { get { return allCoins; } }

    void Awake()
    {
        Instance = this;
        allCoins = PlayerPrefs.GetInt("coins", 0);

        levelsStatistic = new List<LevelStat>();
        for (int i = 0; i < levelsNumber; i++)
            levelsStatistic.Add(null);


        //PlayerPrefs.DeleteKey("coins");
        //PlayerPrefs.DeleteKey("level1");
        //PlayerPrefs.DeleteKey("level2");
    }

    public void AddCoins(int coins)
    {
        this.allCoins += coins;
        PlayerPrefs.SetInt("coins", allCoins);
    }


    public bool LevelWon(int level)
    {
        if (level - 1 < levelsNumber)
            return GetLevelStat(level).levelPassed;
        return false;
    }

    public bool PrevLevelWon(int level)
    {
        if (level - 2 < levelsNumber)
            return GetLevelStat(level - 1).levelPassed;
        return false;
    }

    public void SaveLevelStat(LevelStat levelStat, int level)
    {
        string str = JsonUtility.ToJson(levelStat);
        PlayerPrefs.SetString("level" + level, str);

        levelsStatistic[level - 1] = levelStat;
    }

    public LevelStat GetLevelStat(int level)
    {
        if (levelsStatistic[level - 1] != null)
            return levelsStatistic[level - 1];

        string str = PlayerPrefs.GetString("level" + level, null);
        LevelStat stat = JsonUtility.FromJson<LevelStat>(str);
        if (stat == null)
        {
            stat = new LevelStat();
        }
        return stat;
    }

    void OnApplicationQuit()
    {
        Debug.Log("Exit");
        PlayerPrefs.Save();
    }

}
