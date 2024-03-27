using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI score_text;

    public int score;

    void Start()
    {
        score_text.text = "Score: " + score;
    }


    public int increase_score() // adds score and updates canvas
    {
        score += 1;
        score_text.text = "Score: " + score;

        return score;
    }
}
