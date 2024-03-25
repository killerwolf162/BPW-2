using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI money_text;

    public int score;

    void Start()
    {
        money_text.text = "Money: " + score;
    }


    public int increase_money()
    {
        score += 1;
        money_text.text = "Score: " + score;

        return score;
    }
}
