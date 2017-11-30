using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DebugHPScriptShield : MonoBehaviour {

    public int ReloadDelay = 3;
    public Text GameStateText;
    [SerializeField]
    AudioClip LoseSound;
    

    private bool isDead = false;
    

    public string SceneToLoad = "Gym_Shield";

    // Update is called once per frame
    void Update () {

        PlayerHPShield o = gameObject.GetComponent<PlayerHPShield>();
        if (o != null && o.CurrentHP <= 0 && !isDead)
        {
            Debug.Log("Battery levels depleted: Game Over!");
            isDead = true;
            GameOver();
        }
	}

    void GameOver()
    {
        if (GameStateText != null)
        {
            GameStateText.text = "Game Over!";
            AudioSource.PlayClipAtPoint(LoseSound, transform.position);
        }
        Decompose d = GetComponent<Decompose>();
        if (d != null)
        {
            d.DecomposeMe();
        }
        Invoke("ReloadLevel", ReloadDelay);
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneToLoad);
    }
}
