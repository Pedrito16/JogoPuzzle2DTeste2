using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] LineRenderer corda;
    [SerializeField] GameObject imã;
    void Start()
    {
        corda = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        corda.SetPosition(0, transform.position);
        corda.SetPosition(1, imã.transform.position);
    }
}
