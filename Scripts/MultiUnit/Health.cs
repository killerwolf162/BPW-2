using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{

    public UnityEvent OnDie;

    [SerializeField]
    public int max_health = 10, current_health;

    [SerializeField]
    private GameObject hitFX;
    [SerializeField]
    private GameObject missFX;

    private void Awake()
    {
        current_health = max_health;
    }
    public void get_hit(int damage)
    {
        current_health -= damage;
        Debug.Log(gameObject.name + " health is " + current_health);
        Instantiate(hitFX, transform.position, Quaternion.identity);
        if (current_health <= 0)
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

    public void spawn_drops()
    {
        int RNG = Random.Range(0, 100);
        if(RNG < 65)
            Instantiate(this.GetComponent<Unit>().coin, this.transform.position, this.transform.rotation);
        else if( RNG >= 65)
            Instantiate(this.GetComponent<Unit>().heart, this.transform.position, this.transform.rotation);

    }

}
