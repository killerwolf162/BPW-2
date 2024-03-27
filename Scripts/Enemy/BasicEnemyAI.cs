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
    private GameObject target; // assing player as target for AI
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
        allies_in_scene = GameObject.FindGameObjectsWithTag("Enemy");
        unit.current_movement_points = unit.max_movement_points;
        if(target == null)
            target = GameObject.FindWithTag("Player");

        Debug.Log($"Enemy: {gameObject.name} takes turn");
        selection_feedback.play_feedback();

        player_position = Vector3Int.RoundToInt(target.transform.position);
        target_position = (Vector2Int)player_position;

        if (check_if_player_is_in_range(target_position) == true)
        {
            Debug.Log("player is in range, chasing");
            state = basic_ai_state.chase;
        }
        if (check_if_player_is_in_range(target_position) == false)
        {
            Debug.Log("player is not in range, wandering");
            state = basic_ai_state.wander;
        }

        if (state == basic_ai_state.chase)
        {
            Debug.Log("chasing");
            Dictionary<Vector2Int, Vector2Int?> movement_range = character_movement.get_movement_range_for(unit);
            List<Vector2Int> path = get_path_to_target(movement_range, target_position);

            Queue<Vector2Int> path_queue = new Queue<Vector2Int>(path);

            StartCoroutine(move_unit_coroutine(path_queue));

        }

        if (state == basic_ai_state.wander)
        {
            Debug.Log("Wandering");
            Dictionary<Vector2Int, Vector2Int?> movement_range = character_movement.get_movement_range_for(unit);
            List<Vector2Int> path = get_path_to_random_position(movement_range);

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
        //else // if target is not in the list, moves to random position
        //{
        //    Debug.Log("target was not in the list");
        //    Vector2Int selected_destination = possible_destination[UnityEngine.Random.Range(0, possible_destination.Count)];
        //    list_to_return = get_path_to(selected_destination, movement_range);
        //}
        return list_to_return;

    }

    private List<Vector2Int> get_path_to_random_position(Dictionary<Vector2Int, Vector2Int?> movement_range) // Use this in Idle/Patrol state
    {
        List<Vector2Int> possible_destination = check_all_destinations(movement_range);

        Vector2Int selected_destination = possible_destination[UnityEngine.Random.Range(0, possible_destination.Count)];  //make other variant so this targets the player in chase state
        List<Vector2Int> list_to_return = get_path_to(selected_destination, movement_range);

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
        foreach (GameObject ally_unit in allies_in_scene)
        {
            ally_position = Vector3Int.FloorToInt(ally_unit.transform.position);
            ally_position_to_remove = (Vector2Int)ally_position;
            possible_destination.Remove(ally_position_to_remove);
        }
    }

    private List<Vector2Int> get_path_to(Vector2Int destination, Dictionary<Vector2Int, Vector2Int?> movement_range)
    {
        List<Vector2Int> path = new List<Vector2Int>();

        path.Add(destination);
        while(movement_range[destination] != null)
        {
            path.Add(movement_range[destination].Value);
            destination = movement_range[destination].Value;
        }
        path.Reverse();

        return path.Skip(1).ToList();
    }
    bool check_if_player_is_in_range(Vector2Int target)
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
        if(distance <= 10f)
        {
            yield return new WaitForSeconds(0.3f);
            if (unit.can_still_move() == false || path.Count <= 0)
            {
                finished_movement();
                yield break;
            }
            Vector2Int pos = path.Dequeue();
            Vector3Int direction = Vector3Int.RoundToInt(new Vector3(pos.x + 0.5f, pos.y + 0.5f, 0) - transform.position);
            unit.handle_movement(direction, 0);
            yield return new WaitForSeconds(0.3f);
            if (path.Count > 0)
            {
                StartCoroutine(move_unit_coroutine(path));
            }
            else
            {
                yield return new WaitForSeconds(0.3f);
                finished_movement();
            }
        }
        else if(distance > 10f)
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

    private void finished_movement()
    {
        turn_finished?.Invoke();
        selection_feedback.stop_feedback();
    }
}
