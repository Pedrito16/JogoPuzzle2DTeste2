using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntregarChave : MonoBehaviour
{
    [SerializeField] int inputQuantity;
    [SerializeField] int input;
    [SerializeField] GameObject chave;

    [SerializeField] Transform[] inputOutPut;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(input == 4)
        {
            Instantiate(chave, inputOutPut[1]);
            input = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out ISujeira target))
        {
            Destroy(collision.gameObject);
            input++;
            if (input == 4)
            {
                InstanciarChave();
                input = 0;
            }
        }
    }
    void InstanciarChave()
    {
        GameObject key = Instantiate(chave, inputOutPut[1]);
        key.transform.position = new Vector2(inputOutPut[1].position.x, inputOutPut[1].position.y);
    }
}
