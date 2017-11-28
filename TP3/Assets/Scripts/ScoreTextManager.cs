using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextManager : MonoBehaviour {
    public Text ScoreText;

    public static ScoreTextManager Instance;

    int score = 0;
    int partNum = 0;

    void Start()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        Instance = this;
        SetText();
    }

    public void SetScore(int score)
    {
        this.score = score;
        SetText();
    }

    public void SetPartPickedNum(int num)
    {
        this.partNum = num;
        SetText();
    }

    private void SetText()
    {
        ScoreText.text = "Parts found: " + partNum + "/5\n" + "Score: " + score;
    }
	
}
