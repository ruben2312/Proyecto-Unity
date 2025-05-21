using UnityEngine;
using System.Collections; 
public class Cesta : MonoBehaviour
{
    private int HuevosRecogidos = 0;

    public bool CestaLlena = false;
    private bool esperando = false;
    public GameObject prefabHuevo;
    public BoxCollider2D colliderA;
    public BoxCollider2D colliderB;
    public BoxCollider2D colliderC;
    public EdgeCollider2D colliderCesta; 
    public AudioSource encestar;
    public AudioSource romper;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {    

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        ControlarHuevo huevo = other.GetComponent<ControlarHuevo>();

        if (other.CompareTag("Huevo") && !huevo.EnCesta && !CestaLlena && !esperando)
        {
            esperando = true;
            StartCoroutine(RecogerHuevoConDelay(other.gameObject));
            GameObject nuevoHuevo = Instantiate(prefabHuevo, new Vector3(-0.3f, 5.1f, -0.2f), Quaternion.identity);

            Rigidbody2D rbNuevo = nuevoHuevo.GetComponent<Rigidbody2D>();
            rbNuevo.gravityScale = 0f;

            ControlarHuevo nuevoHuevoControlador = nuevoHuevo.GetComponent<ControlarHuevo>();
            nuevoHuevoControlador.EnCesta = false;
            huevo.EnCesta = true;
        }
    }

     System.Collections.IEnumerator RecogerHuevoConDelay(GameObject huevo)
    {
        encestar.Play(); 
        yield return new WaitForSeconds(1f); // Esperamos un segundo
        
        HuevosRecogidos++;
        Debug.Log("Huevos recogidos: " + HuevosRecogidos);

        if (HuevosRecogidos >= 3)
        {
            romper.Play();
            StartCoroutine(RotarPadre());
            string nombreDelPadre = transform.parent.name;
            CestaLlena = true;
            colliderCesta.enabled = false; 
            if(nombreDelPadre == "Cesta1")
                colliderA.enabled = false;
            else if(nombreDelPadre == "Cesta2")
                colliderB.enabled = false;
            else if(nombreDelPadre == "Cesta3")
                colliderC.enabled = false;

             
            Debug.Log("Cesta llena!");
        }

        esperando = false; // Permitimos recoger otro después
    }

    IEnumerator RotarPadre()
{
    Transform objetoARotar = transform.parent.Find("Tapa"); // El padre del objeto actual
    float anguloFinal = -90f;
    float duracion = 1f; // duración de la animación en segundos
    float tiempo = 0f;

    Quaternion rotacionInicial = objetoARotar.rotation;
    Quaternion rotacionFinal = Quaternion.Euler(0f, 0f, anguloFinal);

    while (tiempo < duracion)
    {
        tiempo += Time.deltaTime;
        float t = tiempo / duracion;
        objetoARotar.rotation = Quaternion.Lerp(rotacionInicial, rotacionFinal, t);
        yield return null;
    }

    objetoARotar.rotation = rotacionFinal; // Asegura que termine en el ángulo exacto
}
}
