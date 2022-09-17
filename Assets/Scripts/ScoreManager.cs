using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public Text lbScore;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        lbScore.text = "Score: " + score;
    }

    public void scoring(int scoreAdded)
    {
        score += scoreAdded;
        lbScore.text = "Score: " + score;
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
