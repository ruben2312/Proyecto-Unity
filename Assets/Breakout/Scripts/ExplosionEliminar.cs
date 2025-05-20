using Unity.VisualScripting;
using UnityEngine;

public class ExplosionEliminar : MonoBehaviour
{
    public AudioSource audioSource;
    GameObject[] puntuacionTracker;
    PuntuacionTracker trackerClase;

    public GameObject powerupBolas, powerupPaleta, powerupBomba;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        puntuacionTracker = GameObject.FindGameObjectsWithTag("PuntuacionTracker");
        trackerClase = puntuacionTracker[0].GetComponent<PuntuacionTracker>();
        Destroy(this.GameObject(), 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D other){
        if(other.gameObject.CompareTag("Brick") || other.gameObject.CompareTag("Brick powerup")|| other.gameObject.CompareTag("Wall")){
            audioSource.Play();

            if(other.gameObject.CompareTag("Brick powerup")){
                int randomPwp = Random.Range(0,3);

                if(randomPwp == 0){
                    GameObject powerup = Instantiate(powerupBolas, other.gameObject.transform.position, other.gameObject.transform.rotation);
                    powerup.GetComponent<Rigidbody2D>().linearVelocity = Vector2.down * 2.5f;
                    Destroy(powerup, 6f);
                }else if(randomPwp == 1){
                    GameObject powerup = Instantiate(powerupPaleta, other.gameObject.transform.position, other.gameObject.transform.rotation);
                    powerup.GetComponent<Rigidbody2D>().linearVelocity = Vector2.down * 2.5f;
                    Destroy(powerup, 6f);
                }else{
                    GameObject powerup = Instantiate(powerupBomba, other.gameObject.transform.position, other.gameObject.transform.rotation);
                    powerup.GetComponent<Rigidbody2D>().linearVelocity = Vector2.down * 2.5f;
                    Destroy(powerup, 6f);
                }
                
            }
            if(other.gameObject.CompareTag("Brick") || other.gameObject.CompareTag("Brick powerup")){
                
                puntuacionTracker[0].GetComponent<PuntuacionTracker>().Decrementar();
                puntuacionTracker[1].GetComponent<PuntuacionTracker>().Decrementar();
                Destroy(other.GameObject());
            }
        }
    }
}
