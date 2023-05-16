using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private LevelSO levelSO;
    [SerializeField] private LoadingScript loading;
    private int currentlevelSelected = 0;
    private SFX sfx;
    private BackSoundManager backSoundManager;
    private void Awake()
    {
        GameManager.levelSO = levelSO;
    }
    private void Start()
    {
        UpdateLevel(currentlevelSelected);
        sfx = FindFirstObjectByType<SFX>();
        backSoundManager = FindFirstObjectByType<BackSoundManager>();
        backSoundManager.PlayMainMenuBGM();
    }
    public void stopBGM()
    {
        backSoundManager.GetComponent<BackSoundManager>().GetComponent<AudioSource>().Stop();
    }
    public void StartButton()
    {
        PlaySFX();
        loading.LoadScene(GameManager.levelSO.level[currentlevelSelected].level);
    }
    public void ExitButton()
    {
        PlaySFX();
        Application.Quit();
    }
    public void NormalDifficulty()
    {
        PlaySFX();
        GameManager.difficulty = "Normal";
    }
    public void HardDifficulty()
    {
        PlaySFX();
        GameManager.difficulty = "Hard";
    }
    public void PlaySFX()
    {
        sfx.PlayButtonSFX();
    }
    
    public void NextOption()
    {
        sfx.PlayButtonSFX();
        currentlevelSelected++;
        if(currentlevelSelected >= GameManager.levelSO.level.Length)
        {
            currentlevelSelected = 0;
        }
        UpdateLevel(currentlevelSelected);
    }
    public void BackOption()
    {
        sfx.PlayButtonSFX();
        currentlevelSelected--;
        if(currentlevelSelected < 0)
        {
            currentlevelSelected = GameManager.levelSO.level.Length - 1;
        }
        UpdateLevel(currentlevelSelected);
    }
    private void UpdateLevel(int levelSelected)
    {
        levelText.text = GameManager.levelSO.level[currentlevelSelected].level;
        scoreText.text = GameManager.levelSO.level[currentlevelSelected].score.ToString();
    }
}
