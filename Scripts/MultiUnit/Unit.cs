using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Unit : MonoBehaviour, ITurnDependant
{


    [SerializeField]
    private int max_movement_points = 5;

    [SerializeField]
    private int current_movement_points;

    [SerializeField]
    private int attack_range;

    TurnManager turn_manager;

    [SerializeField]
    private int dexterity;

    [SerializeField]
    private int armour_value;


    public int Current_movement_points { get => current_movement_points;}
    public int Attack_range { get => attack_range;}

    public event Action OnMove;

    [SerializeField]
    private LayerMask enemy_detection_layer;

    void Start()
    {
        reset_movement_points();
        turn_manager = GameObject.Find("Turn_Manager").GetComponent<TurnManager>();
    }

    private void reset_movement_points()
    {
        current_movement_points = max_movement_points;
    }

    public bool can_still_move()
    {
        return current_movement_points > 0;
    }

    public void wait_turn()
    {
        reset_movement_points();
    }

    public void handle_movement(Vector3 cardinal_direction, int move_cost)
    {

        GameObject enemy_unit = check_if_enemy_in_direction(cardinal_direction);
        if(enemy_unit == null)
        {
            current_movement_points -= move_cost;
            transform.position += cardinal_direction;
            OnMove?.Invoke();
        }
        else
        {
            int RNG_attacker = Random.Range(1, 10); // attackrol attacker
            int RNG_defender = Random.Range(1, 10); // defencerol defender

            Debug.Log("Attacker rol");
            Debug.Log(RNG_attacker + dexterity);
            Debug.Log("Defender roll");
            Debug.Log(RNG_defender + enemy_unit.GetComponent<Unit>().dexterity);

            if (RNG_attacker + dexterity > enemy_unit.GetComponent<Unit>().dexterity + RNG_defender) // if attackrol + dex of attacker is higher than defence rol + dex defender, attack hits
            {
                perform_attack(enemy_unit.GetComponent<Health>());
            }
            else if (RNG_attacker + dexterity < enemy_unit.GetComponent<Unit>().dexterity + RNG_defender)
            {
                perform_dodge(enemy_unit.GetComponent<Health>());
            }

            current_movement_points = 0;

        }

        //if (current_movement_points <= 0)
        //    turn_manager.next_turn();

    }

    public void game_over()
    {
        SceneManager.LoadScene("Game_Over");
    }

    // eventually change to an attack window where player can choose different weapons to perform attack with if player has these weapons
    // player then chooses weapon, weapon range will be highlighted.
    //  if enemy is inside highlight(weapon) range and player clicks on enemy, attack with said weapon is performed.
    // also make it so damage is tied to equiped weapon that deals a random value between 2 parameters
    // also make it so there are seperate action and movement points. For now this works

    private void perform_attack(Health health)
    {

        health.get_hit(Random.Range(1,2)); // changes this so it corresponds to weapon damge
        current_movement_points = 0;
    }
    private void perform_dodge(Health health)
    {
        health.dodge_attack();
    }

    private GameObject check_if_enemy_in_direction(Vector3 cardinal_direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, cardinal_direction, 1, enemy_detection_layer);
        if (hit.collider != null)
        {
            return hit.collider.gameObject;
        }
        return null;
    }
}
