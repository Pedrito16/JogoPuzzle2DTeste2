using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] public Collider2D col;
    [SerializeField] PlayerState state;
    [SerializeField] PointEffector2D magnetEffect;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        magnetEffect = GetComponent<PointEffector2D>();
    }

    void Update()
    {
        state = Player.state;
        if (state == PlayerState.Pulling)
        {
            col.enabled = true;
        }
        if (Player.magnetMode)
        {
            magnetEffect.forceMagnitude = Mathf.Abs(magnetEffect.forceMagnitude);
            spriteRenderer.flipX = false;
        }
        else
        {
            magnetEffect.forceMagnitude = -Mathf.Abs(magnetEffect.forceMagnitude);
            spriteRenderer.flipX = true;
        }
    }
    private void OnEnable()
    {
        col.enabled =false;
        Invoke("enableCollider", 0.3f);
    }
    void enableCollider()
    {
        col.enabled = true;
    }
}
