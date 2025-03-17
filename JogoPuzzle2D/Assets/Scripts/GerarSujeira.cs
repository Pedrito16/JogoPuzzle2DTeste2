using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GerarSujeira : MonoBehaviour
{
    [SerializeField] GameObject sujeira;
    [SerializeField] Transform localSpawn;
    [SerializeField] Transform localDespawn;
    [Space]
    [SerializeField] float numeroMaxPool;
    [SerializeField] float maxTimer;

    Queue<GameObject> pool = new Queue<GameObject>();
    float timer;
    void Start()
    {
        for(int i = 0; i < numeroMaxPool; i++)
        {
            GameObject obj = Instantiate(sujeira);
            obj.transform.SetParent(transform, false);
            obj.transform.position = localSpawn.position;
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > maxTimer)
        {
            SpawnarSujeira(Random.Range(1, 2));
            timer = 0;
        }
    }
    void SpawnarSujeira(int spawnQuantity)
    {
        for (int i = 0; i < spawnQuantity; i++)
        {
            GameObject obj = pool.Dequeue();
            Transform objTransform = obj.transform;
            obj.SetActive(true);
            objTransform.position = new Vector3(objTransform.position.x + Random.Range(-0.8f, 0.75f), objTransform.position.y, objTransform.position.z);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ISujeira target))
        {
            target.Sujar();
            collision.gameObject.SetActive(false);
            collision.transform.position = localSpawn.position;
            pool.Enqueue(collision.gameObject);
        } 
    }
}
