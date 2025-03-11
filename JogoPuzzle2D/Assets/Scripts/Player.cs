using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerState
{
    notPulling,
    Pulling
}
public class Player : MonoBehaviour
{
    [SerializeField] GameObject magnet;
    [SerializeField] float playerMoveSpeed;
    [SerializeField] float maxDistanceToPullBack;
    [Header("Configurações")]
    [SerializeField] float tempoSegurado;
    [SerializeField] float velocidadePuxada;

    [Header("Debug")]
    [SerializeField] float distance;
    [SerializeField] public static PlayerState state;
    [SerializeField] bool haveMagnet;

    //variaveis invisiveis
    Rigidbody2D rb;
    LineRenderer corda;
    SpringJoint2D magnetJoint;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        corda = GetComponent<LineRenderer>();
        magnetJoint = magnet.GetComponent<SpringJoint2D>();
        magnetJoint.distance = maxDistanceToPullBack;
        state = PlayerState.notPulling;
    }
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 playerMovementVector = new Vector2(horizontal, vertical);
        if(playerMovementVector != Vector2.zero)
        {
            playerMovementVector = playerMovementVector.normalized;
        }
        rb.velocity = playerMovementVector * playerMoveSpeed;
    }
    private void FixedUpdate()
    {
        if (Input.GetButton("Jump") && !haveMagnet)
        {
            StartCoroutine(pullMagnet());
            state = PlayerState.Pulling;
        }
        else
        {
            state = PlayerState.notPulling;
            StopAllCoroutines();
        }
    }
    void ThrowMagnet()
    {

    }
    IEnumerator pullMagnet()
    {
        float iterador = 0;
        while(iterador < 100)
        {
            magnet.transform.position = Vector2.Lerp(magnet.transform.position, transform.position, velocidadePuxada * iterador / 100);
            iterador += Time.deltaTime;
            yield return null;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Magnet") //to usando com tag pois: não sei usar interface "direito" e porque magnet não tem script para aplicar interface :P
        {
            magnet.SetActive(false);
            corda.enabled = false;
            haveMagnet = true;
        }
    }
}
