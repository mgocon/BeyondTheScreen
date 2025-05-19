using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReEnableBoundary : MonoBehaviour
{
    public Collider2D[] mountainColliders;
    public Collider2D[] boundaryColliders;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        int layer3Index = LayerMask.NameToLayer("Layer 3");
        int playerLayer = player != null ? player.layer : -1;
        Debug.Log($"Player Layer: {playerLayer}");
        if (player != null)
        {
            Debug.Log($"Player name: {player.name}, Layer: {player.layer}, Sorting Layer: {player.GetComponent<SpriteRenderer>()?.sortingLayerName}");
        }

        // At start, always enable Layer 2 - Stone and disable Layer 3 - Boundary
        foreach (Collider2D mountain in mountainColliders)
        {
            mountain.enabled = true;
        }
        foreach (Collider2D boundary in boundaryColliders)
        {
            boundary.enabled = false;
        }

        // If player is on Layer 3, disable Layer 2 - Stone and enable Layer 3 - Boundary
        if (playerLayer == layer3Index)
        {
            Debug.Log("Player is on Layer 3, switching colliders");
            foreach (Collider2D mountain in mountainColliders)
            {
                mountain.enabled = false;
            }
            foreach (Collider2D boundary in boundaryColliders)
            {
                boundary.enabled = true;
            }
        }
        else
        {
            Debug.Log("Player is NOT on Layer 3, using default collider state");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {            
            foreach (Collider2D mountain in mountainColliders)
            {
                mountain.enabled = true;
            }
            foreach (Collider2D boundary in boundaryColliders)
            {
                boundary.enabled = false;
            }
        }
    }
}
