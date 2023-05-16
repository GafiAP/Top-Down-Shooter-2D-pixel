using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class VictoryOrDefeat : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI scoreText1;
    [SerializeField] private LoadingScript loadingScript;

    private int totalScore;
    private SFX sfx;
    private void Start()
    {
        sfx = FindFirstObjectByType<SFX>();
    }
    private void Update()
    {
        if (Player.instance.victoryOrDefeat)
        {
            sfx.StopPlay();
        }
    }
    public void PlaySFX()
    {
        sfx.PlayButtonSFX();
    }
    public void RestartThisScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void UpdateVictoryOrDefeatScore()
    {
        totalScore = Player.instance.GetScore();
        scoreText.text = totalScore.ToString();
        scoreText1.text = totalScore.ToString();

        for (int i = 0; i < GameManager.levelSO.level.Length; i++)
        {
            if (GameManager.levelSO.level[i].level == SceneManager.GetActiveScene().name)
            {
                if (GameManager.levelSO.level[i].score > totalScore)
                {
                    //score di SO lebih besar dari totalscore
                }
                else
                {
                    GameManager.levelSO.level[i].score = totalScore;
                    GameManager.SaveScore();
                }
            }
        }
    }

}
