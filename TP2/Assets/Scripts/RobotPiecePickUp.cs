using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RobotPiecePickUp : MonoBehaviour {

    [SerializeField]
    int TotalNbPieces = 5;


    private int currentNbPieces;
    void Start()
    {
        currentNbPieces = 0;
    }

    public void RobotPiecePickedUp()
    {
        currentNbPieces++;
        Debug.Log("Number of robot pieces found (" + currentNbPieces + "/" + TotalNbPieces + ")");

        if(currentNbPieces == TotalNbPieces)
        {
            Debug.Log("Level completed!");
            // TODO: Complete level and load next.
            SceneManager.LoadScene("RobotPartPickUp");
        }
    }
}
