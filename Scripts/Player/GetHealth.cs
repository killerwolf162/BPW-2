using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class GetHealth : MonoBehaviour
{
    [SerializeField] 
    public TextMeshProUGUI health_text;
    
    private int health;

    Health player_health;

    private void Awake()
    {
        health_text = GameObject.FindWithTag("Health_text").GetComponent<TextMeshProUGUI>();
        player_health = GetComponent<Health>();
    }

    private void Start()
    {
        health = player_health.current_health;
        health_text.text = "Health:" + health;
    }

    public int decrease_health()
    {
        health = player_health.current_health;
        return health;
    }
}
