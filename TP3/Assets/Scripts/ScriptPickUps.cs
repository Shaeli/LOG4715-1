using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptPickUps : MonoBehaviour
{
    [SerializeField]
    Text countText;

    private int score;

    // Utile pour régler des valeurs aux objets
    void Start()
    {
        score = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Vis"))
        {
            other.gameObject.SetActive(false);
            score++;
            SetCountText();
        }
        else if (other.gameObject.CompareTag("VisOr"))
        {
            other.gameObject.SetActive(false);
            score+=3;
            SetCountText();
        }
        else if (other.gameObject.CompareTag("RobotPart"))
        {
            other.gameObject.SetActive(false);
            gameObject.SendMessage("RobotPiecePickedUp");
        }
    }

    void SetCountText()
    {
        if (countText != null)
        {
            countText.text = "Score : " + score.ToString();
        }
    }
}
