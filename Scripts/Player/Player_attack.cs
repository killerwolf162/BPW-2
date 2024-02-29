using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player_attack : MonoBehaviour
{

    [SerializeField]
    private Map map;

    private Unit selected_unit;
    private List<Vector2Int> attack_range;

    [SerializeField]
    private AttackRangeHighlight attack_range_highlight;

    public void handle_attack(Vector3 end_position)
    {
        if (this.selected_unit == null)
            return;

        if (this.selected_unit.can_still_move() == false)
            return;

        if (Vector2.Distance(end_position, this.selected_unit.transform.position) > 0)
        {
            Vector2 direction = calculate_attack(end_position);

            if (map.can_i_move_to((Vector2)this.selected_unit.transform.position, direction))
            {
                this.selected_unit.handle_attack(direction);
                if (this.selected_unit.can_still_move())
                {
                    prepare_attack_range();
                }
                else
                {
                    attack_range_highlight.clear_attack_highlight();
                }
            }
        }
    }

    private Vector2 calculate_attack(Vector3 end_position)
    {
        Vector2 direction = (end_position - this.selected_unit.transform.position);
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            float sign = Mathf.Sign(direction.x);
            direction = Vector2.right * sign;
        }
        else
        {
            float sign = Mathf.Sign(direction.y);
            direction = Vector2.up * sign;
        }

        return direction;
    }

    public void handle_selection(GameObject detected_object)
    {

        if (detected_object == null)
        {
            reset_character_attack();
            return;
        }

        if (detected_object.CompareTag("Player"))
            this.selected_unit = detected_object.GetComponent<Unit>();
        else
            this.selected_unit = null;


        if (this.selected_unit == null)
            return;

        if (this.selected_unit.can_still_move())
        {
            prepare_attack_range();
        }

        else
        {
            attack_range_highlight.clear_attack_highlight();
        }

        foreach (Vector2Int position in attack_range)
        {
            Debug.Log(position);
        }


    }
        private void prepare_attack_range()
    {
        attack_range = map.get_attack_range(this.selected_unit.transform.position, this.selected_unit.Attack_range).Keys.ToList();
        attack_range_highlight.highlight_attack_tiles(attack_range);
    }

        private void reset_character_attack()
    {
        attack_range_highlight.clear_attack_highlight();
        this.selected_unit = null;
    }

}
