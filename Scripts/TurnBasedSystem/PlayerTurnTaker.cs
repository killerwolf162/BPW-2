using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnTaker : MonoBehaviour
{

    public void wait_turn()
    {
        foreach (ITurnDependant item in GetComponents<ITurnDependant>())
        {
            item.wait_turn();
        }
    }
}
