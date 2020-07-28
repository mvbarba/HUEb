using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    private PlayerStateManager playerState;

    private void Start() {
        playerState = PlayerStateManager.Instance();
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Trigger entry");
        if (other.gameObject == playerState.gameObject) {
            Debug.Log("player is here");
        }
    }

    private void OnTriggerExit(Collider other) {
        
    }
}
