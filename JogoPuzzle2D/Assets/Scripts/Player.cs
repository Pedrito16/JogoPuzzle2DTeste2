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
    [Header("Configura��es")]
    [SerializeField] float playerMoveSpeed;
    [SerializeField] float velocidadePuxada;
    [SerializeField] float for�aJogada;
    [SerializeField] Color[] modeColors;

    [Header("Debug")]
    [SerializeField] float tempoSegurado;
    [SerializeField] public static PlayerState state;
    [SerializeField] public bool hasMagnet;
    [SerializeField] bool hasKey;
    [SerializeField] Color activeColor;
    bool canMove;
    public static bool magnetMode;

    //variaveis invisiveis
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Rigidbody2D magnetRb;
    LineRenderer corda;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        magnetRb = magnet.GetComponent<Rigidbody2D>();
        corda = GetComponent<LineRenderer>();
        spriteRenderer.color = modeColors[0];
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
            rb.velocity = playerMovementVector * playerMoveSpeed;

        else
            rb.velocity = Vector2.zero;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        magnet.transform.rotation = Quaternion.Euler(new Vector3(magnet.transform.rotation.x, magnet.transform.rotation.y, GetPlayerLookingPosition(mousePos, magnet.transform.position)));
        if (Input.GetButton("Fire1") && hasMagnet && tempoSegurado <= 4)
        {
            tempoSegurado += Time.deltaTime * 2f;
            StartCoroutine(ChargedColor());
        }
        if (Input.GetButtonUp("Fire1") && hasMagnet)
        {
            ThrowMagnet();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            magnetMode = !magnetMode;
            spriteRenderer.color = playerColor();
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
        Vector2 direction = (mousePos - transform.position);
        direction = direction.normalized;
        magnet.SetActive(true);
        magnet.transform.position = transform.position;
        magnetRb.AddForce(direction * tempoSegurado * 130, ForceMode2D.Force);
        Invoke("FollowPlayer", 1f);
    }
    void FollowPlayer()
    {
        spriteRenderer.color = playerColor();
        tempoSegurado = 0;
        state = PlayerState.Active;
    }
    Color playerColor()
    {
        if (magnetMode)
            return modeColors[1];
        else
            return modeColors[0];
    }
    float GetPlayerLookingPosition(Vector3 mousePos, Vector3 position)
    {
        Vector3 direction = position - mousePos;
        float calculo = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        return calculo + 90;
    }
    IEnumerator ChargedColor()
    {
        float iterador = 0;
        Color cor = spriteRenderer.color;
        while(iterador <= 30f)
        {
            cor = Color.Lerp(spriteRenderer.color, Color.white, iterador / 30f);
            iterador += Time.deltaTime;
            spriteRenderer.color = cor;
            yield return null;
        }
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
        }
        if(collision.gameObject.TryGetComponent(out ICollectable target))
        {
            ICollectable coletavel;
            coletavel = target;
            coletavel.Collectable();
            hasKey = true;
        }
        if (collision.gameObject.TryGetComponent(out IDoor obj))
        {
            if (hasKey)
            {
                IDoor door = obj;
                door.Open();
                hasKey = false;
            }
        }
    }
}