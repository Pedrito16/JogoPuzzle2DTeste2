using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sujeira : MonoBehaviour, ISujeira
{
    [SerializeField] float velocidade;
    [SerializeField] float velocidadeRotação;
    Rigidbody2D rb;
    [SerializeField] bool canMove;
    public bool newFather;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            float rotate = velocidadeRotação * Time.deltaTime;
            transform.Rotate(Vector3.forward, rotate);

            rb.velocity = Vector3.up * velocidade;
        }
        if (newFather)
        { 
            transform.position = Vector3.Lerp(transform.position, transform.parent.position, Time.deltaTime * 10);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Filtro"))
        {
            canMove = false;
            rb.velocity = Vector3.zero;
            transform.SetParent(collision.transform, false);
            newFather = true;
        }
    }
    public void Sujar()
    {
        gameObject.SetActive(false);
    }
}
