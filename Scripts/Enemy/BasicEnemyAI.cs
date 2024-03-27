using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour, IenemyAI
{
    public event Action turn_finished;

    private Map map;

    private Unit unit;
    private CharacterMovement character_movement;

    [SerializeField]
    private FlashFeedback selection_feedback;

    [SerializeField]
    private LayerMask player_detection_layer;

    public enum basic_ai_state { wander, chase}

    private basic_ai_state state;

    private List<Vector2Int> movement_range;

    [SerializeField]
    private GameObject target;
    private Vector3Int player_position;
    private Vector2Int target_position;

    public GameObject[] allies_in_scene; 
    public Vector3Int ally_position;
    public Vector2Int ally_position_to_remove;

     private void Awake()
    {
        map = FindObjectOfType<Map>();
        character_movement = FindObjectOfType<CharacterMovement>();
        unit = GetComponent<Unit>();
        selection_feedback = GetComponent<FlashFeedback>();

        allies_in_scene = GameObject.FindGameObjectsWithTag("Enemy");
    }

    public void start_turn()
    {
        allies_in_scene = GameObject.FindGameObjectsWithTag("Enemy"); // finds all enemys in scene and adds them to an array
        unit.current_movement_points = unit.max_movement_points;
        if(target == null) // sets player as target
            target = GameObject.FindWithTag("Player");

        Debug.Log($"Enemy: {gameObject.name} takes turn");
        selection_feedback.play_feedback(); // makes enemy thats taking turn flash

        player_position = Vector3Int.RoundToInt(target.transform.position); // converts player vector3 to Vector2Int
        target_position = (Vector2Int)player_position;

        if (check_if_player_is_in_range(target_position) == true) // sets state to chase if player is in range
        {
            Debug.Log("player is in range, chasing");
            state = basic_ai_state.chase;
        }
        else if (check_if_player_is_in_range(target_position) == false) // if player is not in range starts wandering
        {
            Debug.Log("player is not in range, wandering");
            state = basic_ai_state.wander;
        }

        if (state == basic_ai_state.chase)
        {
            Debug.Log("chasing");
            Dictionary<Vector2Int, Vector2Int?> movement_range = character_movement.get_movement_range_for(unit); // gets max movement range
            List<Vector2Int> path = get_path_to_target(movement_range, target_position); // creates path to player

            Queue<Vector2Int> path_queue = new Queue<Vector2Int>(path);

            StartCoroutine(move_unit_coroutine(path_queue));

        }

        if (state == basic_ai_state.wander)
        {
            Debug.Log("Wandering");
            Dictionary<Vector2Int, Vector2Int?> movement_range = character_movement.get_movement_range_for(unit);
            List<Vector2Int> path = get_path_to_random_position(movement_range); // creates path to random position

            Queue<Vector2Int> path_queue = new Queue<Vector2Int>(path);
            StartCoroutine(move_unit_coroutine(path_queue));
        }
    }

    private List<Vector2Int> get_path_to_target(Dictionary<Vector2Int, Vector2Int?> movement_range, Vector2Int target_location)
    {
        List<Vector2Int> possible_destination = check_all_destinations(movement_range);

        List<Vector2Int> list_to_return = new List<Vector2Int>();

        if (possible_destination.Contains(target_location)) // checks if target position is in list of possible possitions.
        {
            Vector2Int selected_destination = target_location;  // If target is in list, moves to target
            list_to_return = get_path_to(selected_destination, movement_range);
        }
        
        return list_to_return;

    }

    private List<Vector2Int> get_path_to_random_position(Dictionary<Vector2Int, Vector2Int?> movement_range) // Use this in Idle/Patrol state
    {
        List<Vector2Int> possible_destination = check_all_destinations(movement_range); // checks all positions in movement range

        Vector2Int selected_destination = possible_destination[UnityEngine.Random.Range(0, possible_destination.Count)];  //takes a random position in range and sets it as target
        List<Vector2Int> list_to_return = get_path_to(selected_destination, movement_range); //creates path to target

        return list_to_return;
    }

    private List<Vector2Int> check_all_destinations(Dictionary<Vector2Int, Vector2Int?> movement_range)
    {
        List<Vector2Int> possible_destination = movement_range.Keys.ToList(); //gives all possible locations unit can move to
        possible_destination.Remove(Vector2Int.RoundToInt(transform.position)); // removes current possition

        cycle_through_ally_positions(possible_destination);
        return possible_destination;
    }

    private void cycle_through_ally_positions(List<Vector2Int> possible_destination)
    {
        foreach (GameObject ally_unit in allies_in_scene) // remoces ever position occupied by a player-enemy from possible destinations from the list of destinations
        {
            ally_position = Vector3Int.FloorToInt(ally_unit.transform.position);
            ally_position_to_remove = (Vector2Int)ally_position;
            possible_destination.Remove(ally_position_to_remove);
        }
    }

    private List<Vector2Int> get_path_to(Vector2Int destination, Dictionary<Vector2Int, Vector2Int?> movement_range)
    {
        List<Vector2Int> path = new List<Vector2Int>();

        path.Add(destination); //adds destination to path
        while(movement_range[destination] != null) //works backwards towards the player to create the path player enemy takes
        {
            path.Add(movement_range[destination].Value);
            destination = movement_range[destination].Value;
        }
        path.Reverse();

        return path.Skip(1).ToList(); //returns path without player enemy current position
    }
    bool check_if_player_is_in_range(Vector2Int target) // checks if player position is in current list with possible destinations
    {
        Dictionary<Vector2Int, Vector2Int?> movement_range = character_movement.get_movement_range_for(unit);

        if (movement_range.ContainsKey(target))
        {
            return true;
        }
        else
            return false;
    }

    private IEnumerator move_unit_coroutine(Queue<Vector2Int> path)
    {
        float distance = Vector3.Distance(this.transform.position, player_position);
        if(distance <= 10f) // if player enemy is within 10 from player, delay actions to make player understand movement
        {
            yield return new WaitForSeconds(0.3f);
            if (unit.can_still_move() == false || path.Count <= 0) // if player enemy has no movement points left ends its turn
            {
                finished_movement();
                yield break;
            }
            Vector2Int pos = path.Dequeue();
            Vector3Int direction = Vector3Int.RoundToInt(new Vector3(pos.x + 0.5f, pos.y + 0.5f, 0) - transform.position); // creates new position to move to
            unit.handle_movement(direction, 0); // move to postion
            yield return new WaitForSeconds(0.3f);
            if (path.Count > 0) // if path still have positions left continue
            {
                StartCoroutine(move_unit_coroutine(path));
            }
            else
            {
                yield return new WaitForSeconds(0.3f);
                finished_movement();
            }
        }
        else if(distance > 10f) // if player enemys are more than 10 away from player dont delay between movements
        {
            if (unit.can_still_move() == false || path.Count <= 0)
            {
                finished_movement();
                yield break;
            }

            Vector2Int pos = path.Dequeue();
            Vector3Int direction = Vector3Int.RoundToInt(new Vector3(pos.x + 0.5f, pos.y + 0.5f, 0) - transform.position);
            unit.handle_movement(direction, 0);

            if (path.Count > 0)
            {
                StartCoroutine(move_unit_coroutine(path));
            }
            else
            {
                finished_movement();
            }         
        }



    }

    private void finished_movement() // ends turn
    {
        turn_finished?.Invoke();
        selection_feedback.stop_feedback();
    }
}
