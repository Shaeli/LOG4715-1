using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptPickUps : MonoBehaviour
{
    [SerializeField]
    Text countText;

    [SerializeField]
    AudioClip impactpiece;

    [SerializeField]
    AudioClip impactrobotpart;

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
            AudioSource.PlayClipAtPoint(impactpiece, transform.position);
            other.gameObject.SetActive(false);
            score++;
            SetCountText();
        }
        else if (other.gameObject.CompareTag("VisOr"))
        {
            AudioSource.PlayClipAtPoint(impactpiece, transform.position);
            other.gameObject.SetActive(false);
            score +=3;
            SetCountText();
        }
        else if (other.gameObject.CompareTag("RobotPart"))
        {
            AudioSource.PlayClipAtPoint(impactrobotpart, transform.position);
            other.gameObject.SetActive(false);
            gameObject.SendMessage("RobotPiecePickedUp");
        }
    }

    void SetCountText()
    {
        ScoreTextManager.Instance.SetScore(score);
        if (countText != null)
        {
            countText.text = "Score : " + score.ToString();
        }
    }
}
