using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] LineRenderer corda;
    [SerializeField] GameObject im�;
    void Start()
    {
        corda = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        corda.SetPosition(0, transform.position);
        corda.SetPosition(1, im�.transform.position);
    }
}
