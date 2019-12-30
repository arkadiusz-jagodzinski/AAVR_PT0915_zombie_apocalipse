using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI DifficultyButtonText;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void ChangeDifficultyText()
    {
        string[] difficulty = new string[3] {"easy","normal","hard"};
        var oldText = DifficultyButtonText.text;
        var newText = "";
        var length = 3;
        for(var i = 0;i<length;i++){
            if(oldText == difficulty[i]){
                newText = difficulty[(i+1)%(length)];
            }
        }
        DifficultyButtonText.text = newText;
    }
}
