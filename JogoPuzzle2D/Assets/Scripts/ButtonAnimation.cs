using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] string selectedName;
    [SerializeField] TextMeshProUGUI buttonText;
    string originalName;

    void Start()
    {
        originalName = buttonText.text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.text = selectedName;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.text = originalName;
    }
}
