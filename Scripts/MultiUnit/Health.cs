using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{

    public UnityEvent OnDie;

    [SerializeField]
    protected int health;

    public void get_hit(int damage)
    {
        health -= damage;
        Debug.Log(gameObject.name + " health is" + health);
        if (health <= 0)
        {
            OnDie?.Invoke();
            Debug.Log(gameObject.name + " died!");
        }

    }

}
