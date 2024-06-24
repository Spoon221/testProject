﻿using UniRx;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    private CompositeDisposable subscriptions = new CompositeDisposable();
    [SerializeField] private GameObject startUI;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject loseUI;
    [SerializeField] private GameObject settingsUI;
    private bool settingsOpen = false;

    private void OnEnable()
    {
        StartCoroutine(Subscribe());
        gameUI.SetActive(true);
        startUI.SetActive(true);
        if (settingsUI.GetComponent<Canvas>().enabled)
            settingsUI.SetActive(false);
    }

    private IEnumerator Subscribe()
    {
        yield return new WaitUntil(() => GameEvents.instance != null);

        GameEvents.instance.gameStarted.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if (value)
                    ActivateMenu(gameUI);
            })
            .AddTo(subscriptions);

        GameEvents.instance.gameWon.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if (value)
                    ActivateMenu(winUI);
            })
            .AddTo(subscriptions);

        GameEvents.instance.gameLost.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if (value)
                    ActivateMenu(loseUI);
            })
            .AddTo(subscriptions);
    }
    private void OnDisable()
    {
        subscriptions.Clear();
    }

    private void ActivateMenu(GameObject _menu)
    {
        gameUI.SetActive(false);
        startUI.SetActive(false);
        winUI.SetActive(false);
        loseUI.SetActive(false);
        //settingsUI.SetActive(false);

        _menu.SetActive(true);
    }

    public void SettingsMenu()
    {
        if (settingsOpen)
        { 
            Time.timeScale = 1f;
            settingsUI.SetActive(false);
            settingsOpen = false;
        }
        else
        {
            Time.timeScale = 0f;
            settingsUI.SetActive(true);
            settingsOpen = true;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Вышел");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        int newCurrentLevel = PlayerPrefs.GetInt("currentLevel", 1) + 1;
        int newLoadingLevel = PlayerPrefs.GetInt("loadingLevel", 1) + 1;

        if (newLoadingLevel >= SceneManager.sceneCountInBuildSettings)
            newLoadingLevel = 1;

        PlayerPrefs.SetInt("currentLevel", newCurrentLevel);
        PlayerPrefs.SetInt("loadingLevel", newLoadingLevel);

        SceneManager.LoadScene(newLoadingLevel);
    }
}