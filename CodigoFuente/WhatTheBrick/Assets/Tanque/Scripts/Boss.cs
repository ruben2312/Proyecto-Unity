using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour
{
    public GameObject Escudo;
    public GameObject cargadorNivel = null;

    private SpriteRenderer spriteRenderer;
    public Color hitColor = Color.red;
    public float flashDuration = 0.1f;
    public Huevo huevo;

    private Color originalColor;

    public GameObject huevoPrefab;
    public Transform puntoDisparo;
    public float velocidadHuevo = 10f;
    public float tiempoEntreDisparos = 2f;

    private float contadorDisparo;
    private bool huevoDisponible = true;

    void Start()
    {
        if(ModoJuego.desdeSeleccionNivel == false)
            CongelarDisparo(4f);
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        contadorDisparo = tiempoEntreDisparos;
        huevo.gameObject.SetActive(false); // Iniciamos con el huevo desactivado
        huevo.boss = this;
    }

    void Update()
    {
        if (!huevoDisponible) return;
        // Disparar huevos
        contadorDisparo -= Time.deltaTime;
        if (contadorDisparo <= 0)
        {
            DispararHuevo();
            contadorDisparo = tiempoEntreDisparos;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bala"))
        {
            gameObject.GetComponent<BarraVida>().QuitarVida(5f);
            if (gameObject.GetComponent<BarraVida>().vidaActual <= 50)
            {
                Escudo.SetActive(true);
            }
            if (gameObject.GetComponent<BarraVida>().vidaActual <= 0)
            {
                cargadorNivel.GetComponent<CargadorNivel>().CargarSiguiente();
            }
        }
    }

    private void DispararHuevo()
    {
        Vector2 direccion = -puntoDisparo.right;
        huevo.Lanzar(direccion, velocidadHuevo);
        huevoDisponible = false;
    }
    public void HuevoDevuelto()
    {
        huevoDisponible = true;
    }

    public void Flash()
    {
        StopAllCoroutines(); // Por si ya estaba flasheando
        StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        spriteRenderer.color = hitColor;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
    }
    
    public void CongelarDisparo(float duracion)
    {
        StartCoroutine(CongelarCoroutine(duracion));
    }

    private IEnumerator CongelarCoroutine(float duracion)
    {
        huevoDisponible = false;       // Bloquea disparar
        yield return new WaitForSeconds(duracion);
        huevoDisponible = true;        // Vuelve a permitir disparar
        
    }
}
