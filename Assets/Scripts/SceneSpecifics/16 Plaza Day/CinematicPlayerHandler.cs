using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicPlayerHandler : MonoBehaviour
{
    [SerializeField] Waypoint _movementDestination;

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "Player"){
            
            PlayerMovement[] player = FindObjectsOfType<PlayerMovement>();

            GameStateData.Instance.gameData.isInCinematic = true;

            for (int i = 0; i < player.Length; i++)
            {
                player[i].CalculateMovement(_movementDestination);
            }
            
            Destroy(gameObject, 2f);
        }
    }
}
