using System.Collections;
using UnityEngine;
public enum PlayerState
{
    Inactive,
    Active,
    Pulling,
    Throwing
}
public class Player : MonoBehaviour
{
    [SerializeField] GameObject magnet;

    [Header("Configurações")]
    [SerializeField] float playerMoveSpeed;
    [SerializeField] float velocidadePuxada;
    [SerializeField] float forçaJogada;

    [Header("Debug")]
    [SerializeField] float tempoSegurado;
    [SerializeField] public static PlayerState state;
    [SerializeField] public bool hasMagnet;
    [SerializeField] bool canMove;

    //variaveis invisiveis
    Rigidbody2D rb;
    Rigidbody2D magnetRb;
    LineRenderer corda;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        magnetRb = magnet.GetComponent<Rigidbody2D>();
        corda = GetComponent<LineRenderer>();
        state = PlayerState.Active;
        canMove = true;
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

        if(canMove)
        {
            rb.velocity = playerMovementVector * playerMoveSpeed;
        }
        else
            rb.velocity = Vector2.zero;

        if (Input.GetButton("Fire1") && tempoSegurado < 3)
        {
            tempoSegurado += Time.deltaTime;
            state = PlayerState.Throwing;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            ThrowMagnet(tempoSegurado);
        }
    }
    private void FixedUpdate()
    {
        if (Input.GetButton("Jump") && !hasMagnet)
        {
            canMove = false;
            StartCoroutine(pullMagnet());
            state = PlayerState.Pulling;
        }
        else 
        {
            StopAllCoroutines();
            canMove = true;
        }
    }
    void ThrowMagnet(float força)
    {
        state = PlayerState.Throwing;
        hasMagnet = false;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        magnet.SetActive(true);
        magnet.transform.position = transform.position;
        magnet.transform.rotation = Quaternion.Euler(new Vector3(magnet.transform.rotation.x, magnet.transform.rotation.y, GetPlayerLookingPosition(mousePos)));

        magnetRb.velocity = mousePos * força * forçaJogada;
        StartCoroutine(JogarImã(força, mousePos));
        corda.enabled = true;
    }
    /*IEnumerator JogarImã(float força, Vector2 paraOnde)
    {
        float iterador = 0;
        while(iterador < 3)
        {
            magnetRb
        }
    }*/
    float GetPlayerLookingPosition(Vector3 mousePos)
    {
        Vector3 direction = transform.position - mousePos;
        float calculo = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        return calculo + 90;
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
            hasMagnet = true;
        }
    }
}
