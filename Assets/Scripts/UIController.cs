using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public bool IsGamePaused = false;

    public GameObject PauseMenuUI;
    public GameObject WinMenuUI;
    public GameObject LoseMenuUI;

    public TextMeshProUGUI ScorePerLevelTextUI;
    public TextMeshProUGUI WinScoreTextUI;
    public TextMeshProUGUI LoseScoreTextUI;

    public Game game;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu();
        }
    }

    public void PauseMenu()
    {
        if (PauseMenuUI.activeSelf == true)
        {
            PauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            IsGamePaused = false;
        }
        else if (WinMenuUI.activeSelf == false & LoseMenuUI.activeSelf == false)
        {
            PauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            IsGamePaused = true;
        }
    }

    public void RestartLevel()
    {
        game.RestartLevel();
        LoseMenuUI.SetActive(false);
        ScorePerLevelTextUI.SetText(game.ScorePerLevel.ToString());
    }

    public void NextLevel()
    {
        game.NextLevel(game.LevelNumber);
        WinMenuUI.SetActive(false);
        ScorePerLevelTextUI.SetText(game.ScorePerLevel.ToString());
    }
}
