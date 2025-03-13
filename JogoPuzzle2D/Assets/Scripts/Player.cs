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
    [Header("Configurações")]
    [SerializeField] float playerMoveSpeed;
    [SerializeField] float velocidadePuxada;
    [Tooltip("Use valores altos (addForce)")]
    [SerializeField]public float forçaJogada;

    [Header("Debug")]
    [SerializeField] float tempoSegurado;
    [SerializeField] public static PlayerState state;
    [SerializeField] public bool hasMagnet;
    [SerializeField] bool canMove;

    //variaveis invisiveis
    float strength;
    Rigidbody2D rb;
    Rigidbody2D magnetRb;
    LineRenderer corda;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        magnetRb = magnet.GetComponent<Rigidbody2D>();
        corda = GetComponent<LineRenderer>();
        magnetCircleCol = magnet.GetComponent<CircleCollider2D>();
        state = PlayerState.Active;
        strength = forçaJogada;
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

        if (Input.GetButton("Fire1") && hasMagnet && tempoSegurado <= 3)
        {
            tempoSegurado += Time.deltaTime / 2;
        }
        if (Input.GetButtonUp("Fire1") && hasMagnet)
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
        corda.enabled = true;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - magnet.transform.position);
        direction = direction.normalized;
        magnet.SetActive(true);
        magnet.transform.position = transform.position;
        magnet.transform.rotation = Quaternion.Euler(new Vector3(magnet.transform.rotation.x, magnet.transform.rotation.y, GetPlayerLookingPosition(mousePos)));
        magnetRb.AddForce(direction * forçaJogada * 75, ForceMode2D.Force);
        Invoke("FollowPlayer", 1f);
    }
    void FollowPlayer()
    {
        state = PlayerState.Active;
    }
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
            magnetCircleCol.enabled = false;
        }
    }
}