using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCollectible : MonoBehaviour
{
    PlayerParentMovement move;
    Player2 move2;

    void OnTriggerEnter2D(Collider2D other)
    {
        move = other.GetComponent<PlayerParentMovement>();
        move2 = other.GetComponent<Player2>();
        if (move != null)
        {
            if (!move.pickIce)
            {
                move.pickIce = true;
                Destroy(gameObject);
            
            }
        }
        if (move2 != null)
        {
            if (!move2.pickIce)
            {
                move2.pickIce = true;
                Destroy(gameObject);

            }
        }
    }
}
