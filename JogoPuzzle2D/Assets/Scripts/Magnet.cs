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
        if (state == PlayerState.Throwing)
            StartCoroutine(Collider());
    }
    IEnumerator Collider()
    {
        if (!gameObject.activeSelf)
        {
            col.enabled = false;
        }
        else
        {
            yield return new WaitForSeconds(1.5f);
            col.enabled = true;
        }
    }
}
