using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PersonajePrincipal : MonoBehaviour
{
    public Camera MainCamera;
    public GameObject space;

    public float moveSpeed = 5f; // Velocidad de movimiento
    private Rigidbody2D rb; // Referencia al Rigidbody2D
    private Vector2 moveDirection; // Dirección del movimiento

    GameObject canvas;
    public GameObject BarraCarga;
    GameObject barra = null;

    bool tomates = false;
    bool galletas = false;
    bool leche = false;
    bool pagar = false;


    public TMP_Text TomateText;
    public TMP_Text GalletasText;
    public TMP_Text LecheText;
    public TMP_Text PagarText;

    GameObject current_interactable = null;

    void Start()
    {
        space.SetActive(false);
        canvas = GameObject.Find("Canvas");
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && current_interactable) // Presionar Espacio para activar el shake
        {
            switch(current_interactable.name)
            {
                case "Tomates":                   
                    if(!tomates)
                    {
                        //barra.transform.position = this.transform.position + new Vector3(0,1,0); 
                        space.SetActive(true);
                        barra = Instantiate(BarraCarga);
                        barra.GetComponentInChildren<PlayerGrab>().player = this.gameObject;
                        barra.GetComponentInChildren<PlayerGrab>().type = "Tomates";
                    }
                    break;
                case "Galletas":
                    if (!galletas)
                    {
                        //barra.transform.position = this.transform.position + new Vector3(0,1,0); 
                        space.SetActive(true);
                        barra = Instantiate(BarraCarga);
                        barra.GetComponentInChildren<PlayerGrab>().player = this.gameObject;
                        barra.GetComponentInChildren<PlayerGrab>().type = "Galletas";
                    }

                    break;
                case "Leche":
                    if (!leche)
                    {
                        //barra.transform.position = this.transform.position + new Vector3(0,1,0); 
                        space.SetActive(true);
                        barra = Instantiate(BarraCarga);
                        barra.GetComponentInChildren<PlayerGrab>().player = this.gameObject;
                        barra.GetComponentInChildren<PlayerGrab>().type = "Leche";
                    }
                    break;
                case "Pagar":
                    if(leche && tomates && galletas)
                    {
                        if (!pagar)
                        {
                            //barra.transform.position = this.transform.position + new Vector3(0,1,0); 
                            space.SetActive(true);
                            barra = Instantiate(BarraCarga);
                            barra.GetComponentInChildren<PlayerGrab>().player = this.gameObject;
                            barra.GetComponentInChildren<PlayerGrab>().type = "Pagar";
                        }
                    }
                    break;
                    
            }
        }

        // Leer entrada del usuario
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Calcular dirección del movimiento
        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void FixedUpdate()
    {
        // Mover al personaje usando Rigidbody2D
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Person")
        {
            StartCoroutine(MainCamera.GetComponent<CameraShake>().Shake(0.3f, 0.2f)); // Duración 0.5s, Magnitud 0.3
            MainCamera.GetComponent<Camerazoom>().StartZoomIn(-1.0f);
        }
        if(collision.gameObject.tag == "Interactable")
        {
         
         current_interactable = collision.gameObject;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Interactable")
        {
            space.SetActive(false);
            if (barra)
            { 
                barra.SetActive(false);
                Destroy(barra);
                barra = null;
            }
            current_interactable = null;
        }
    }

    public void fin_de_barra(string type)
    { 
        switch(type)
        {
            case "Tomates":
                TomateText.text = $"<s>{TomateText.text}</s>";
                tomates = true;
                break;
            case "Galletas":
                GalletasText.text = $"<s>{GalletasText.text}</s>";
                galletas = true;
                break;
            case "Leche":
                LecheText.text = $"<s>{LecheText.text}</s>";
                leche = true;
                break;
            case "Pagar":
                PagarText.text = $"<s>{PagarText.text}</s>";
                pagar = true;               
                SceneManager.LoadScene(2);
                break;

        }
    }
}
