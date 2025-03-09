using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSystem : MonoBehaviour
{
    [SerializeField] Transform[] pontosCorda;
    [SerializeField] Transform magnet;
    [SerializeField] LineRenderer corda;
    [SerializeField] Transform player;
    void Start()
    {
        corda = player.gameObject.GetComponent<LineRenderer>();
        corda.positionCount = 8;
    }

    void Update()
    {
        corda.SetPosition(0, player.position);
        corda.SetPosition(1, pontosCorda[0].position);
        corda.SetPosition(2, pontosCorda[1].position);
        corda.SetPosition(3, pontosCorda[2].position);
        corda.SetPosition(4, pontosCorda[3].position);
        corda.SetPosition(5, pontosCorda[4].position);
        corda.SetPosition(6, pontosCorda[5].position);
        corda.SetPosition(7, magnet.position);
    }
}
