using System;
using Unity.VisualScripting;
using UnityEngine;

public class BolaExtra : MonoBehaviour
{

    public float minimoVertical = -5.5f;
    public float velInicial = 10f;
    
    Rigidbody2D rb;

    public GameObject explosion;
    public GameObject explosionBola;

    public GameObject powerupBolas, powerupPaleta, powerupBomba;

    GameObject[] puntuacionTracker;

    public AudioSource audioSource, audioSourceBal;
    public AudioClip sHit, sMuerte, sBalatro;

    float volumen = 0.5f;
    PuntuacionTracker trackerClase;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        puntuacionTracker = GameObject.FindGameObjectsWithTag("PuntuacionTracker");
        trackerClase = puntuacionTracker[0].GetComponent<PuntuacionTracker>();

        var aux = Vector3.Normalize(UnityEngine.Random.insideUnitCircle);
        aux.y = Math.Abs(aux.y);
        aux.y = Mathf.Clamp(aux.y, 0.1f, 1.0f);
        rb.linearVelocity = aux;
    }

    // Update is called once per frame
    void Update()
    {
        

        if(transform.position.y < minimoVertical){

            GameObject particula = Instantiate(explosionBola, new Vector3(transform.position.x, minimoVertical, 0), transform.rotation);
            ParticleSystem ps = particula.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule ma = ps.main;
            ma.startColor = GetComponent<SpriteRenderer>().color;
            Destroy(particula, 2f);
            Destroy(this.GameObject());
            
        }

        rb.linearVelocity = Vector3.Normalize(rb.linearVelocity) * velInicial;
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
                int randomPwp = UnityEngine.Random.Range(0,3);

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
        }
        else{
            audioSource.PlayOneShot(sHit, volumen);
        }
    }
}
