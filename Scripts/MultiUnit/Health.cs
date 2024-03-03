using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{

    public UnityEvent OnDie;

    [SerializeField]
    protected int health;

    [SerializeField]
    private GameObject hitFX;
    [SerializeField]
    private GameObject missFX;
    public void get_hit(int damage)
    {
        health -= damage;
        Debug.Log(gameObject.name + " health is " + health);
        Instantiate(hitFX, transform.position, Quaternion.identity);
        if (health <= 0)
        {
            OnDie?.Invoke();
            Debug.Log(gameObject.name + " died!");
        }

    }

    public void dodge_attack()
    {
        Debug.Log(gameObject.name + "dodged the attack");
        Instantiate(missFX, transform.position, Quaternion.identity);
    }

    public void destroy_unit()
    {
        Destroy(gameObject);
    }

}
