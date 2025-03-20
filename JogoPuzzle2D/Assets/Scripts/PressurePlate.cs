using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] bool activated;
    [SerializeField] Sprite[] sprites;
    SpriteRenderer spriteRenderer;
    [SerializeField] UnityEvent action;
    [SerializeField] UnityEvent revertAction;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
            activated = true;
            action?.Invoke();
            spriteRenderer.sprite = sprites[1];
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        activated = false;
        revertAction?.Invoke();
        spriteRenderer.sprite = sprites[0];
    }
}
