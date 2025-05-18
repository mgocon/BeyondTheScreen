using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReEnableBoundary : MonoBehaviour
{
    public Collider2D[] mountainColliders;
    public Collider2D[] boundaryColliders;

    private void Start()
    {
        // Ensure mountain colliders are disabled at the start
        foreach (Collider2D mountain in mountainColliders)
        {
            mountain.enabled = false;
        }

        // Ensure boundary colliders are enabled at the start
        foreach (Collider2D boundary in boundaryColliders)
        {
            boundary.enabled = true;
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
