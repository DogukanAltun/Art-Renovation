using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private Canvas GameOverPanel;
    [SerializeField] private Canvas NextLevelPanel;
    public int Levelindex;
    public static CanvasManager Instance;
    void Awake()
    {
        Levelindex = PlayerPrefs.GetInt("LevelIndex");
        Instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1;
        Debug.Log(Levelindex);
    }
    public void GameOver()
    {
        Time.timeScale = 0;
        GameOverPanel.gameObject.SetActive(true);
        PlayerPrefs.SetInt("LevelIndex", Levelindex);
    }

    public void NextLevel()
    {
        Time.timeScale = 0;
        NextLevelPanel.gameObject.SetActive(true);
        Levelindex++;
        PlayerPrefs.SetInt("LevelIndex", Levelindex);
    }

    public void LoadNewScene()
    {
        SceneManager.LoadScene(0);
    }


}
