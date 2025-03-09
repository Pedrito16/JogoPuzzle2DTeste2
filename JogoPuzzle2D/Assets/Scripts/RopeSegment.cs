using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSegment : MonoBehaviour
{
    [SerializeField] Transform nextSegment;
    [SerializeField] Transform previousSegment;
    [SerializeField] float maxDistance;
    [SerializeField] float distance;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, nextSegment.position);
        if(distance >= maxDistance)
        {
            StartCoroutine(VoltarRange());
        }
    }
    IEnumerator VoltarRange()
    {
        float iterador = 0;
        while(distance >= maxDistance)
        {
            
            transform.position = Vector2.Lerp(transform.position, nextSegment.position, iterador / 10);
            iterador += Time.deltaTime;
            yield return null;
        }
    }
}
