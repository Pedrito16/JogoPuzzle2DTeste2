using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceLock : MonoBehaviour
{
    /*[HideInInspector]*/ public bool isOnPlace;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Filtro")
        {
            isOnPlace = true;
            collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.transform.position = new Vector2(transform.position.x, transform.position.y);
            collision.transform.rotation = transform.rotation;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Filtro") isOnPlace = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isOnPlace = false;
    }
}
