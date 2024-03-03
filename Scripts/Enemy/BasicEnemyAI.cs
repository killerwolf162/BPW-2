using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour, IenemyAI
{
    public event Action turn_finished;

    private Unit unit;
    private CharacterMovement character_movement;

    [SerializeField]
    private FlashFeedback selection_feedback;


    private void Awake()
    {
        character_movement = FindObjectOfType<CharacterMovement>();
        unit = GetComponent<Unit>();
        selection_feedback = GetComponent<FlashFeedback>();
    }


    public void start_turn()
    {
        Debug.Log($"Enemy: {gameObject.name} takes turn");
        selection_feedback.play_feedback();

        Dictionary<Vector2Int, Vector2Int?> movement_range = character_movement.get_movement_range_for(unit);
        List<Vector2Int> path = get_path_to_random_position(movement_range);

        Queue<Vector2Int> path_queue = new Queue<Vector2Int>(path);

        StartCoroutine(move_unit_coroutine(path_queue));
    }

    private List<Vector2Int> get_path_to_random_position(Dictionary<Vector2Int, Vector2Int?> movement_range)
    {
        Debug.Log(movement_range.Keys.ToList()[2]);
        return new List<Vector2Int> { movement_range.Keys.ToList()[2] };
    }

    private IEnumerator move_unit_coroutine(Queue<Vector2Int> path)
    {
        yield return new WaitForSeconds(0.5f);
        if (unit.can_still_move() == false || path.Count <= 0)
        {
            finished_movement();
            yield break;
        }
        Vector2Int pos = path.Dequeue();
        Vector3Int direction = Vector3Int.RoundToInt(new Vector3(pos.x + 0.5f, pos.y + 0.5f, 0) - transform.position);
        unit.handle_movement(direction, 0);
        yield return new WaitForSeconds(0.5f);
        if(path.Count > 0)
        {
            StartCoroutine(move_unit_coroutine(path));
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            finished_movement();
        }


    }

    private void finished_movement()
    {
        turn_finished?.Invoke();
        selection_feedback.stop_feedback();
    }
}
