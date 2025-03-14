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
    public Collider2D magnetCircleCol;
    [Header("Configura��es")]
    [SerializeField] float playerMoveSpeed;
    [SerializeField] float velocidadePuxada;
    [Tooltip("Use valores altos (addForce)")]
    [SerializeField]public float for�aJogada;
    [SerializeField] Gradient chargedColor;

    [Header("Debug")]
    [SerializeField] float tempoSegurado;
    [SerializeField] public static PlayerState state;
    [SerializeField] public bool hasMagnet;
    [SerializeField] bool canMove;
    Collider col;
    bool verifica��o;

    //variaveis invisiveis
    float strength;
    Rigidbody2D rb;
    Rigidbody2D magnetRb;
    LineRenderer corda;

    Color corAtual;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        magnetRb = magnet.GetComponent<Rigidbody2D>();
        corda = GetComponent<LineRenderer>();
        magnetCircleCol = magnet.GetComponent<BoxCollider2D>();
        state = PlayerState.Active;
        strength = for�aJogada;
        canMove = true;
        corAtual = gameObject.GetComponent<SpriteRenderer>().color;
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
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        magnet.transform.rotation = Quaternion.Euler(new Vector3(magnet.transform.rotation.x, magnet.transform.rotation.y, GetPlayerLookingPosition(mousePos)));
        if (Input.GetButton("Fire1") && hasMagnet && tempoSegurado <= 5)
        {
            tempoSegurado += Time.deltaTime;
        }
        if (Input.GetButtonUp("Fire1") && hasMagnet)
        {
            ThrowMagnet();
        }
    }
    private void FixedUpdate()
    {
        if (Input.GetButton("Jump") && !hasMagnet)
        {
            canMove = false;
            StartCoroutine(pullMagnet());
        }
        else 
        {
            StopAllCoroutines();
            canMove = true;
        }
    }
    void ThrowMagnet()
    {
        state = PlayerState.Throwing;
        hasMagnet = false;
        corda.enabled = true;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - magnet.transform.position);
        direction = direction.normalized;
        magnet.SetActive(true);
        magnet.transform.position = transform.position;
        magnetRb.AddForce(direction * tempoSegurado * 90, ForceMode2D.Force);
        Invoke("FollowPlayer", 1f);
    }
    void FollowPlayer()
    {
        tempoSegurado = 0;
        state = PlayerState.Active;
    }
    float GetPlayerLookingPosition(Vector3 mousePos)
    {
        Vector3 direction = magnet.transform.position - mousePos;
        float calculo = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        return calculo + 90;
    }
    IEnumerator pullMagnet()
    {
        float iterador = 0;
        state = PlayerState.Pulling;
        while (iterador < 100)
        {
            magnet.transform.position = Vector2.Lerp(magnet.transform.position, transform.position, velocidadePuxada * iterador / 100);
            iterador += Time.deltaTime;
            yield return null;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Magnet") //to usando com tag pois: n�o sei usar interface "direito" e porque magnet n�o tem script para aplicar interface :P
        {
            magnet.SetActive(false);
            corda.enabled = false;
            hasMagnet = true;
            magnetCircleCol.enabled = false;
        }
    }
}