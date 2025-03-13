using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSegment : MonoBehaviour
{
    [SerializeField] Transform nextSegment;
    [SerializeField] Transform previousSegment;
    [SerializeField] float maxDistance;
    [SerializeField] float distance;
    [SerializeField] PlayerState state;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        state = Player.state;

        if (Player.state == PlayerState.Active)
            distance = Vector2.Distance(transform.position, nextSegment.position);
        else if(Player.state == PlayerState.Pulling || Player.state == PlayerState.Throwing)
            distance = Vector2.Distance(transform.position, previousSegment.position);

        if (distance >= maxDistance && state == PlayerState.Active)
        {
            StartCoroutine(PlayerDirection());
        }
        else if (distance >= maxDistance && state == PlayerState.Pulling)
        {
            if (gameObject.tag != "Magnet")
            {
                StartCoroutine(MagnetDirection());
            }
        }
        else if(distance >= maxDistance && state == PlayerState.Throwing)
        {
            if (gameObject.tag != "Magnet")
            {
                transform.position = previousSegment.position;
                StartCoroutine(MagnetDirection());
            }
        }
    }
    IEnumerator MagnetDirection()
    {
        float iterador = 0;
        //print("PuxarImã");
        while (distance >= maxDistance)
        {
            transform.position = Vector2.Lerp(transform.position, previousSegment.position, iterador / 10);
            iterador += Time.deltaTime;
            yield return null;
        }
    }
    IEnumerator PlayerDirection()
    {
        float iterador = 0;
        //print("Voltar Range");
        while (distance >= maxDistance)
        {
            transform.position = Vector2.Lerp(transform.position, nextSegment.position, iterador / 10);
            iterador += Time.deltaTime;
            yield return null;
        }
    }
}
