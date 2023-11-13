using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject panelLevel;
    [SerializeField] private GameObject panelMenu;

    private void Start()
    {
        AudioManager.Instance.PlayMenuMusic();
    }
    public void NewGame()
    {
        panelLevel.SetActive(true);
        AudioManager.Instance.PlayLevelSelectMusic();
        panelMenu.SetActive(false);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackHome()
    {
        panelLevel.SetActive(false);
        AudioManager.Instance.PlayMenuMusic();
        panelMenu.SetActive(true);
    }
}
