using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAnimation : MonoBehaviour
{
    [SerializeField] float velocidade;
    Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float animation = Mathf.Sin(Time.time * Mathf.PI * velocidade) * 10;
        transform.position = startPos + new Vector3(0, animation, 0);
    }
}
