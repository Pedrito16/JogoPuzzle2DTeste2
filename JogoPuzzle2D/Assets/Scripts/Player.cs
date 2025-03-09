using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject imã;
    [SerializeField] float playerMoveSpeed;
    [SerializeField] float maxDistanceToPullBack;
    [SerializeField] bool haveMagnet;
    [SerializeField] Transform anchor;

    [Header("Debug")]
    [SerializeField] float distance;

    //variaveis invisiveis
    Rigidbody2D rb;
    LineRenderer corda;
    Rigidbody2D magnetRb;
    SpringJoint2D magnetJoint;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        corda = GetComponent<LineRenderer>();
        magnetJoint = imã.GetComponent<SpringJoint2D>();
        magnetRb = imã.GetComponent<Rigidbody2D>();
        magnetJoint.distance = maxDistanceToPullBack;
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
        /*distance = Vector2.Distance(transform.position, imã.transform.position);

        if (distance >= maxDistanceToPullBack)
        {
            magnetJoint.enabled = true;
            magnetRb.angularDrag = 0.1f;
            magnetRb.drag = 0.1f;
        }
        else
        {
            magnetJoint.enabled = false;
            magnetRb.angularDrag = 5;
            magnetRb.drag = 5;
        }
            corda.SetPosition(0, transform.position);
        corda.SetPosition(1, imã.transform.position);*/
    }
    void ThrowMagnet()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Magnet") //to usando com tag pois: não sei usar interface "direito" e porque magnet não tem script para aplicar interface :P
        {
            imã.SetActive(false);
            corda.enabled = false;
            haveMagnet = true;
        }
    }
    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(imã.transform.position, maxDistanceToPullBack);
    }
}
