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
            col.enabled = true;
        }
           
    }
    private void OnEnable()
    {
        col.enabled =false;
        Invoke("enableCollider", 1);
    }
    void enableCollider()
    {
        col.enabled = true;
    }
}
