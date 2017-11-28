using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RobotPiecePickUp : MonoBehaviour {

    [SerializeField]
    int TotalNbPieces = 5;
    [SerializeField]
    float DelayLoad = 3f;
    [SerializeField]
    Text GameStateText;

    static public int currentNbPieces;
    void Start()
    {
        currentNbPieces = 0;
    }

    public void RobotPiecePickedUp()
    {
        currentNbPieces++;
        Debug.Log("Number of robot pieces found (" + currentNbPieces + "/" + TotalNbPieces + ")");
        ScoreTextManager.Instance.SetPartPickedNum(currentNbPieces);
        if (currentNbPieces == TotalNbPieces)
        {
            Debug.Log("Level completed!");
            // TODO: Complete level and load next.
            if (GameStateText != null)
            {
                GameStateText.text = "Level completed!";
            }
            Invoke("LoadMenu", DelayLoad);
        }
    }

    void LoadMenu()
    {
       
        SceneManager.LoadScene("Menu");
    }
}
