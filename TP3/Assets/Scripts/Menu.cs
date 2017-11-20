using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    [SerializeField]
    string SingleLevel;
    [SerializeField]
    string MultiLevel;

	public void LoadSingleLevel()
    {
        if (SingleLevel != null)
            SceneManager.LoadScene(SingleLevel);
    }

    public void LoadMultiLevel()
    {
        if (MultiLevel != null)
            SceneManager.LoadScene(MultiLevel);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
