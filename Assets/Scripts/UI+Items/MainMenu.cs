using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    GameObject buttons;
    GameObject levels;
    GameObject instructions;
    MenuAudioManager menuAudioManager;

    Rigidbody2D[] buttonsRigidbodies;
    List<Rigidbody2D> buttonsRigidbodiesList;

    Rigidbody2D[] levelsRigidbodies;
    List<Rigidbody2D> levelsRigidbodiesList;

    private void Awake()
    {
        buttons = GameObject.Find("Buttons");
        levels = GameObject.Find("Levels");
        instructions = GameObject.Find("Instructions");

        buttonsRigidbodies = buttons.transform.GetComponentsInChildren<Rigidbody2D>();
        buttonsRigidbodiesList = new List<Rigidbody2D>(buttonsRigidbodies);

        levelsRigidbodies = levels.transform.GetComponentsInChildren<Rigidbody2D>();
        levelsRigidbodiesList = new List<Rigidbody2D>(levelsRigidbodies);

        levels.SetActive(false);
        instructions.SetActive(false);

        menuAudioManager = GetComponent<MenuAudioManager>();
    }

    private void Start()
    {
        Cursor.visible = true;
    }

    public void StartGame()
    {
        menuAudioManager.Play("Start");
        foreach (Rigidbody2D body in buttonsRigidbodiesList.GetRange(1, 2)) 
        {
            body.WakeUp();
            body.GetComponentInParent<UIButton>().enabled = false;
            body.GetComponentInParent<Button>().interactable = false;
        }
        StartCoroutine(ButtonFalloffTimer(Load, 1));
    }

    public void LevelSelect()
    {
        buttons.SetActive(false);
        levels.SetActive(true);
    }

    public void Back()
    {
        menuAudioManager.Play("Back");
        levels.SetActive(false);
        buttons.SetActive(true);
        instructions.SetActive(false);
    }

    public void Instructions()
    {
        buttons.SetActive(false);
        instructions.SetActive(true);
    }

    public void StartLevel(int level)
    {
        menuAudioManager.Play("Start");

        if (level >= 8) // Dirty
        {
            levelsRigidbodiesList.RemoveAt(level);
        }
        else
        {
            levelsRigidbodiesList.RemoveAt(level - 1);
        }
        foreach (Rigidbody2D body in levelsRigidbodiesList)
        {
            body.WakeUp();
            // Disabling interaction with other buttons
            body.GetComponentInParent<UIButton>().enabled = false;
            body.GetComponentInParent<Button>().interactable = false;
        }
        StartCoroutine(ButtonFalloffTimer(Load, level));
    }

    public void Load(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator ButtonFalloffTimer(Action<int> callbackMethod, int level)
    {
        yield return new WaitForSeconds(2);
        callbackMethod(level);
    }
}
