using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI scoreTextMesh;

    void Start()
    {
        if (GameController.MaxScore > -1)
        {
            scoreTextMesh.text = $"Max score: {GameController.MaxScore}";
        }
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Playground");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
