using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject[] Levels;
    private GameObject SavedLevel;

    public UIController UI;
    public SnakeHeadMovement snakeHead;

    public int ScorePerLevel = 0;
    public int LevelNumber;

    private void Start()
    {
        SavedLevel = Instantiate(Levels[LevelNumber], new Vector3(0f, 0f, 0f), Quaternion.identity);
        SavedLevel.SetActive(true);
    }

    public void LevelComplete()
    {
        UI.WinMenuUI.SetActive(true);
        UI.WinScoreTextUI.SetText(ScorePerLevel.ToString());
        Time.timeScale = 0f;
    }

    public void LevelLose()
    {
        UI.LoseMenuUI.SetActive(true);
        UI.LoseScoreTextUI.SetText(ScorePerLevel.ToString());
        Time.timeScale = 0f;
    }

    public void NextLevel(int levelNumber)
    {
        snakeHead.ResetSnake();
        LevelNumber++;
        if (LevelNumber >= Levels.Length) LevelNumber = 0;
        Destroy(SavedLevel);
        SavedLevel = Instantiate(Levels[LevelNumber], new Vector3(0f, 0f, 0f), Quaternion.identity);
        SavedLevel.SetActive(true);
        ScorePerLevel = 0;
        Time.timeScale = 1f;
    }

    public void RestartLevel()
    {
        snakeHead.ResetSnake();
        Destroy(SavedLevel);
        SavedLevel = Instantiate(Levels[LevelNumber], new Vector3(0f, 0f, 0f), Quaternion.identity);
        SavedLevel.SetActive(true);
        ScorePerLevel = 0;
        Time.timeScale = 1f;
    }
}