using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Filtro : MonoBehaviour
{
    public int quantidadeFilhos;
    [SerializeField] PlaceLock esteiraLock;
    [SerializeField] List<Sujeira> crian�as;
    float timer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        quantidadeFilhos = transform.childCount;
        if (esteiraLock.isOnPlace && quantidadeFilhos > 0)
        {
            timer += Time.deltaTime;
            if(timer > 1)
            {
                crian�as[0].newFather = false;
                crian�as[0].transform.rotation = transform.rotation;
                crian�as[0].GetComponent<BoxCollider2D>().isTrigger = false;
                crian�as[0].transform.position = new Vector2(transform.position.x - 2f, transform.position.y - 0.25f);
                crian�as[0].transform.SetParent(null);
                crian�as.Remove(crian�as[0]); 
                timer = 0;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out ISujeira target))
        {
            crian�as.Add(collision.GetComponent<Sujeira>());
        }
    }
}
