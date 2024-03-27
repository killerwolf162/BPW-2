using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetEndScore : MonoBehaviour
{
    ScoreManager score_manager;

    [SerializeField] public TextMeshProUGUI score_text;

    private void Awake()
    {
        score_manager = GameObject.FindWithTag("Main_Canvas").GetComponent<ScoreManager>();
    }

    void Start()
    {
        score_text.text = "Your score is: " + score_manager.score;
    }
}
