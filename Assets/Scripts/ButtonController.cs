using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void QuitGame(){
        Application.Quit();
    }

    public void GoToMainMenu(){
        SceneManager.LoadScene("MenuScene");
    }
}
