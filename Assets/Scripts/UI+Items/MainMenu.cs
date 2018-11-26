using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    GameObject buttons;
    GameObject levels;
    private void Awake()
    {
        buttons = GameObject.Find("Buttons");
        levels = GameObject.Find("Levels");

        levels.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LevelSelect()
    {
        buttons.SetActive(false);
        levels.SetActive(true);
    }

    public void Back()
    {
        levels.SetActive(false);
        buttons.SetActive(true);
    }

    public void StartLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
