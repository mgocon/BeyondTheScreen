using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//when object exit the trigger, put it to the assigned layer and sorting layers
//used in the stair objects for player to travel between layers
public class LayerTrigger : MonoBehaviour
{
    public string layer;
    public string sortingLayer;
    public Collider2D[] mountainColliders;
    public Collider2D[] boundaryColliders;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {            
            foreach (Collider2D mountain in mountainColliders)
            {
                mountain.enabled = false;
            }
            foreach (Collider2D boundary in boundaryColliders)
            {
                boundary.enabled = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        other.gameObject.layer = LayerMask.NameToLayer(layer);

        other.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = sortingLayer;
        SpriteRenderer[] srs = other.gameObject.GetComponentsInChildren<SpriteRenderer>();
        foreach ( SpriteRenderer sr in srs)
        {
            sr.sortingLayerName = sortingLayer;
        }
    }

}
