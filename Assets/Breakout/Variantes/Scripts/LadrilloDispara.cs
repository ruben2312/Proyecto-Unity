using System.Collections;
using UnityEngine;

public class LadrilloDispara : MonoBehaviour
{
    public GameObject disparo;

    public AudioSource audioSource;

    public AudioClip gun1, gun2, gun3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(disparar());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator disparar(){
        float duracion;
        while(true){
            duracion = Random.Range(5f, 20f);
            yield return new WaitForSeconds(duracion);


            RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.down * 1f, -Vector2.up);

            if(hit){
                if(!hit.collider.gameObject.CompareTag("Brick") && !hit.collider.gameObject.CompareTag("Brick powerup")){
                    Instantiate(disparo, this.transform.position + Vector3.down * 1f, this.transform.rotation);
                }
            }
            else{
                Instantiate(disparo, this.transform.position + Vector3.down * 1f, this.transform.rotation);
                AudioClip disparoSonido;
                if(Random.Range(0,3) == 0){
                    disparoSonido = gun1;
                }
                else if(Random.Range(0,2) == 0){
                    disparoSonido = gun2;
                }
                else{
                    disparoSonido = gun3;
                }
                audioSource.PlayOneShot(disparoSonido, 0.6f);
            }
            
        }
        
    }
}
