using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugHPScript : MonoBehaviour {

    public int ReloadDelay = 3;

    private bool isDead = false;


	// Update is called once per frame
	void Update () {

        PlayerHP o = gameObject.GetComponent<PlayerHP>();
        if (o != null && o.CurrentHP <= 0 && !isDead)
        {
            Debug.Log("Battery levels depleted: Game Over!");
            isDead = true;
            GameOver();
        }
	}

    void GameOver()
    {
        Invoke("ReloadLevel", ReloadDelay);
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene("HP");
    }
}
