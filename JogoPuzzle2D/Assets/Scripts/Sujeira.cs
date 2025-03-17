using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sujeira : MonoBehaviour, ISujeira
{
    [SerializeField] float velocidade;
    [SerializeField] float velocidadeRotação;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float rotate = velocidadeRotação * Time.deltaTime;
        transform.Rotate(Vector3.forward, rotate);

        rb.velocity = Vector3.up * velocidade;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Filtro")
        {
            rb.velocity = Vector3.zero;
            transform.SetParent(collision.transform, false);
        }
    }
    public void Sujar()
    {
        gameObject.SetActive(false);
    }
}
