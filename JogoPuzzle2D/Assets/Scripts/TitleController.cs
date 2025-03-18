using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleController : MonoBehaviour
{
    [SerializeField] GameObject controleMenu;
    void Start()
    {
        controleMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Começar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Sair()
    {
        Application.Quit();
    }
}
