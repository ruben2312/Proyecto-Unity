using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BolaRebote : MonoBehaviour
{

    public float minimoVertical = -5.5f;
    public float velInicial = 10f;
    float alturaOrigen = -3.15f;

    Rigidbody2D rb;

    public int vidas = 5;

    public float volumen = 0.5f;

    public GameObject paleta;
    public GameObject explosion;
    public GameObject explosionBola;

    public GameObject powerupBolas, powerupPaleta, powerupBomba;
    public GameObject[] vidasImg;

    GameObject[] puntuacionTracker;

    bool lanzada = false;
    bool principio = true;

    public GameObject imagenTuto;

    Color colorBola;

    public AudioSource audioSource, audioSourceBal;
    public AudioClip sHit, sMuerte, sBalatro;
    PuntuacionTracker trackerClase;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        puntuacionTracker = GameObject.FindGameObjectsWithTag("PuntuacionTracker");
        trackerClase = puntuacionTracker[0].GetComponent<PuntuacionTracker>();
        colorBola = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if(!lanzada){
            if(principio){
                var aux = imagenTuto.GetComponent<Image>().color;
                aux.a = Mathf.SmoothStep(1f,0.2f,Mathf.PingPong(Time.time * 1.5f,1));
                imagenTuto.GetComponent<Image>().color = aux;
            }
            transform.position = new Vector3(paleta.transform.position.x, alturaOrigen, 0);
            this.GetComponent<CircleCollider2D>().enabled = false;
            colorBola.a = 0.25f;
            GetComponent<SpriteRenderer>().color = colorBola;
            if(Input.GetMouseButton(0)){
                lanzada = true;
                principio = false;
                imagenTuto.SetActive(false);
                colorBola.a = 1;
                GetComponent<SpriteRenderer>().color = colorBola;
                this.GetComponent<CircleCollider2D>().enabled = true;
                float movimientoHorizontal = Input.GetAxis("Horizontal");
                rb.linearVelocity = Vector2.ClampMagnitude(Vector2.up * velInicial + Vector2.right * movimientoHorizontal * velInicial, velInicial);
            }
        }

        if(transform.position.y < minimoVertical){

            audioSource.PlayOneShot(sMuerte, volumen);

            GameObject particula = Instantiate(explosionBola, new Vector3(transform.position.x, minimoVertical, 0), transform.rotation);
            ParticleSystem ps = particula.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule ma = ps.main;
            ma.startColor = colorBola;
            Destroy(particula, 2f);

            
            
            QuitarVida();
            transform.position = new Vector3(paleta.transform.position.x, alturaOrigen, 0);
            lanzada = false;
            trackerClase.SetPuntosSeguidos(0);
            audioSourceBal.pitch = 1;
            //rb.linearVelocity = Vector3.zero;
            rb.linearVelocity = Vector2.zero;
                
            

            
        }

        rb.linearVelocity = Vector3.Normalize(rb.linearVelocity) * velInicial;

    }

    public void QuitarVida(){
        vidas--;
        vidasImg[vidas].SetActive(false);
        if(vidas == 0){
            Destroy(this);
            GameOver();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Brick") || collision.gameObject.CompareTag("Brick powerup")){

            audioSourceBal.pitch = 1 + trackerClase.GetPuntosSeguidos()*0.025f;
            audioSourceBal.PlayOneShot(sBalatro, volumen);
            
            GameObject particula = Instantiate(explosion, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            ParticleSystem ps = particula.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule ma = ps.main;
            ma.startColor = collision.gameObject.GetComponent<SpriteRenderer>().color;
            Destroy(particula, 2f);

            if(collision.gameObject.CompareTag("Brick powerup")){
                int randomPwp = Random.Range(0,3);

                if(randomPwp == 0){
                    GameObject powerup = Instantiate(powerupBolas, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
                    powerup.GetComponent<Rigidbody2D>().linearVelocity = Vector2.down * 2.5f;
                    Destroy(powerup, 6f);
                }else if(randomPwp == 1){
                    GameObject powerup = Instantiate(powerupPaleta, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
                    powerup.GetComponent<Rigidbody2D>().linearVelocity = Vector2.down * 2.5f;
                    Destroy(powerup, 6f);
                }else{
                    GameObject powerup = Instantiate(powerupBomba, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
                    powerup.GetComponent<Rigidbody2D>().linearVelocity = Vector2.down * 2.5f;
                    Destroy(powerup, 6f);
                }
                
            }
            
            trackerClase.sumarUno();
            
            Destroy(collision.gameObject);
            puntuacionTracker[0].GetComponent<PuntuacionTracker>().Decrementar();
            puntuacionTracker[1].GetComponent<PuntuacionTracker>().Decrementar();
        }else if(collision.gameObject.CompareTag("Paleta")){
            trackerClase.SetPuntosSeguidos(0);
            audioSourceBal.pitch = 1;
            audioSource.PlayOneShot(sHit, volumen);
            
        }
        else{
            audioSource.PlayOneShot(sHit, volumen);
        }
    }
    
    void GameOver(){
        Debug.Log("Game over");
        string escenaActual = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(escenaActual);
    }

}
