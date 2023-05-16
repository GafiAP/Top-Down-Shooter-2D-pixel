using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static string difficulty;
    public static LevelSO levelSO;

    private void Start()
    {
        //check JSON exist or not
        if (File.Exists(Application.dataPath + "/ScoreLevel.json"))
        {
            LoadFromJson();
        }
        else
        {
            SaveScore();
        }
    }
    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //Save Data To JSON
    public static void SaveScore()
    {
        ScoreData scoreData = new ScoreData();

        for (int i = 0; i < levelSO.level.Length; i++)
        {
            scoreData.level1 = levelSO.level[0].level;
            scoreData.score1 = levelSO.level[0].score;
            scoreData.level2 = levelSO.level[1].level;
            scoreData.score2 = levelSO.level[1].score;
        }
        string json = JsonUtility.ToJson(scoreData,true);
        File.WriteAllText(Application.dataPath + "/ScoreLevel.json", json);
     } 

    //Load data from JSON
    public static void LoadFromJson()
    {
        string json = File.ReadAllText(Application.dataPath + "/ScoreLevel.json");
        ScoreData scoreData = JsonUtility.FromJson<ScoreData>(json);
        for (int i = 0; i < levelSO.level.Length; i++)
        {


            levelSO.level[0].level = scoreData.level1;
            levelSO.level[0].score = scoreData.score1;
            levelSO.level[1].level = scoreData.level2;
            levelSO.level[1].score = scoreData.score2;
        }
    }
}
