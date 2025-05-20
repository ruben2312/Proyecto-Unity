using UnityEngine;
using System.Collections; // Necesario para usar IEnumerator

public class ControlarHuevo : MonoBehaviour
{
    public float velocidad = 5f;
    private float moveInput;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool EnCesta = false;
    bool reiniciando = false;
    private AudioSource Gallina;

    public bool sonidoEnReproduccion = false;

    public GameObject prefabHuevoRoto;
    public GameObject tapadera;
    void Start()
    {
        EnCesta = false;
        sonidoEnReproduccion = false;
        rb = GetComponent<Rigidbody2D>();
        GameObject gallinaObj = GameObject.Find("Gallina1");
        Gallina = gallinaObj.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && rb.gravityScale == 0f)
            rb.gravityScale = 2f; // activa la gravedad

        if (rb.linearVelocity == Vector2.zero && !reiniciando && !EnCesta)
        {
            reiniciando = true;
            StartCoroutine(ReiniciarSiDetenido());
        }
        
        if(transform.position.y < -6f && !EnCesta) //Si el huevo cae demasiado
        {
            rb.linearVelocity = Vector2.zero;      
            rb.angularVelocity = 0f;              
            rb.gravityScale = 0f;            

            transform.rotation = Quaternion.identity; 

            transform.position = new Vector3(-0.3f, 5.1f, -0.2f);
        }
    }

    void FixedUpdate()
    {
        if(rb.gravityScale != 0f) return; // no se mueve si la gravedad no está activa
        Vector2 movimiento = new Vector3(moveInput, 0f, 0f) * velocidad * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movimiento);
    }

   void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Suelo") && EnCesta == false)
        {
            rb.linearVelocity = Vector2.zero;      
            rb.angularVelocity = 0f;              
            rb.gravityScale = 0f;            

            transform.rotation = Quaternion.identity; 

            Vector3 posicionRota = transform.position + new Vector3(0f, -0.2f, 0f); // 1 unidad hacia abajo
            GameObject roto = Instantiate(prefabHuevoRoto, posicionRota, Quaternion.identity);
            AudioSource crack = gameObject.GetComponent<AudioSource>();
            crack.Play();
            StartCoroutine(FadeOutSprite(roto.GetComponent<SpriteRenderer>()));
            transform.position = new Vector3(-0.3f, 5.1f, -0.2f);


        } else if (other.gameObject.CompareTag("Gallina")){
                if (!sonidoEnReproduccion) // Verifica si el sonido no se está reproduciendo
                {
                    StartCoroutine(SonidoGallina()); // Inicia la corrutina para reproducir el sonido
                }
                Vector2 fuerzaRebote = new Vector2(0, 150f); // Ajusta el número para más o menos rebote
                rb.AddForce(fuerzaRebote);
            }
    }

    IEnumerator ReiniciarSiDetenido()
    {
        // Espera un pequeño tiempo para dar tiempo a que el huevo comience a moverse
        yield return new WaitForSeconds(1f);
        if(rb.linearVelocity.magnitude < 0.1f && rb.gravityScale != 0f){

            rb.linearVelocity = Vector2.zero;      
            rb.angularVelocity = 0f;              
            rb.gravityScale = 0f;            

            transform.rotation = Quaternion.identity; 

            transform.position = new Vector3(-0.3f, 5.1f, -0.2f);

        }
        reiniciando = false;
    }

    IEnumerator SonidoGallina()
    {
        sonidoEnReproduccion = true;
        Gallina.Play();
        yield return new WaitForSeconds(2f); 
        sonidoEnReproduccion = false;
    }

    IEnumerator FadeOutSprite(SpriteRenderer sr)
{
    float duration = 2f;
    float elapsed = 0f;
    Color originalColor = sr.color;
    yield return new WaitForSeconds(2f); 

    while (elapsed < duration)
    {
        float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
        sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
        elapsed += Time.deltaTime;
        yield return null;
    }

    Destroy(sr.gameObject); // Elimina el objeto cuando termina el fade
}

}
