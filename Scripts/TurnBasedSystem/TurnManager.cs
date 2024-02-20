using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnManager : MonoBehaviour
{

    public UnityEvent on_block_player_input, on_unblock_player_input;

    public void next_turn()
    {
        Debug.Log("Waiting..");
        on_block_player_input?.Invoke();

        foreach (PlayerTurnTaker turn_taker in FindObjectsOfType<PlayerTurnTaker>())
        {
            turn_taker.wait_turn();
            Debug.Log($"Unit {turn_taker.name} is waiting");
        }
        Debug.Log("New turn is ready!");
        on_unblock_player_input?.Invoke();

    }


}

public interface ITurnDependant
{
    void wait_turn();
}
