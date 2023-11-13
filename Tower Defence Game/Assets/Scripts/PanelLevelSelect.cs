using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelLevelSelect : MonoBehaviour
{
    [SerializeField] private Sprite baseImage, chooseImage;

    [SerializeField] private Image level1, level2, level3;

    private string nameScene;

    private void Awake() {
        nameScene = "";
    }

    public void ChooseLevel1()
    {
        level1.sprite = chooseImage;
        level2.sprite = baseImage;
        level3.sprite = baseImage;
        nameScene = "Level1";
    }

    public void ChooseLevel2()
    {
        level1.sprite = baseImage;
        level2.sprite = chooseImage;
        level3.sprite = baseImage;
        nameScene = "Level2";

    }

    public void ChooseLevel3()
    {
        level1.sprite = baseImage;
        level2.sprite = baseImage;
        level3.sprite = baseImage;
    }

    public void PlayGame()
    {
        if (nameScene == "") { return; }
        SceneManager.LoadScene(nameScene);
    }
}
