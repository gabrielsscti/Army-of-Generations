using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string loadSceneName;

    public void Load()
    {
        SceneManager.LoadScene(loadSceneName);
    }
}
