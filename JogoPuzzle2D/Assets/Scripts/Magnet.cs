using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] public Collider2D col;
    [SerializeField] PlayerState state;
    bool ativador;
    void Start()
    {
        ativador = true;
    }

    void Update()
    {
        state = Player.state;
        if (state == PlayerState.Pulling)
        {
            Invoke("ActivateCollider", 1.5f);
        }
           
    }
    void ActivateCollider()
    {
        col.enabled = true;
    }
    void DeactivateCollider()
    {
        col.enabled = false;
    }
}
