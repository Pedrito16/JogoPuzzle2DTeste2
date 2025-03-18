using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IDoor
{
    HingeJoint2D joint;
    [SerializeField] Sprite unlockedSprite;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        joint = GetComponent<HingeJoint2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        joint.enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
    }
    public void Open()
    {
        joint.enabled = true;
        spriteRenderer.sprite = unlockedSprite;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
    void Update()
    {
        
    }

}
