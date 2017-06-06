using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PointSystem : MonoBehaviour {

    public int points = 0;    // player score
    private Text playerScore; // text that holds score

    void Start()
    {
        playerScore = GetComponent<Text>();
        Restart();
    }

    public void Points(int score)
    {
        points += score;
        playerScore.text = points.ToString();
    }

    public void Restart()
    {
        points = 0;
        playerScore.text = points.ToString();
    }

    public void DoubleScore()
    {
        points = points * 2;
        playerScore.text = points.ToString();
    }
}
