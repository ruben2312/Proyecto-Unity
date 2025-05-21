using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class MovimientoPaleta : MonoBehaviour
{

    public float velocidad = 5;
    public float maximo = 7.5f;

    public float originalY = -4;

    float movimientoHorizontal;
    private Vector3 mousePosition;
	public float moveSpeed = 0.1f;

    public float volumen = 0.5f;

    public GameObject bolaExtra, bomba;

    public AudioSource audioSource, powerup;
    public AudioClip sGrande, sPequeño;

    int agrandados = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //movimientoHorizontal = Input.GetAxis("Horizontal");
        //transform.position += Vector3.right * movimientoHorizontal * velocidad * Time.deltaTime;
        mousePosition = Input.mousePosition;
        mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(mousePosition).x, originalY);
        transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);

        if(Mathf.Abs(transform.position.x) > maximo){
            var pos = transform.position;
            pos.x = Mathf.Clamp(transform.position.x, -maximo, maximo);
            transform.position = pos;
        }


        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GameObject().CompareTag("PowerupBolas")){
            //Debug.Log("COLISION CON POWERUP");
            powerup.Play();

            Instantiate(bolaExtra, transform.position + Vector3.right * 0.66f + Vector3.up * 0.4f, transform.rotation);
            Instantiate(bolaExtra, transform.position - Vector3.right * 0.66f + Vector3.up * 0.4f, transform.rotation);

            Destroy(other.GameObject());
        }
        if(other.GameObject().CompareTag("PowerupPaleta")){
            StartCoroutine(agrandarPaleta());
            Destroy(other.GameObject());
        }
        if(other.GameObject().CompareTag("PowerupBomba")){
            powerup.Play();
            
            Instantiate(bomba, transform.position + Vector3.up * 0.4f, transform.rotation);

            Destroy(other.GameObject());
        }
    }

    IEnumerator agrandarPaleta(){
        audioSource.PlayOneShot(sGrande, volumen*0.25f);
        Vector3 aux = transform.localScale;
        float tamGrande = 3.2f, tamNormal = 2.5f;
        float espera = 0.125f, duracion = 10f;

        agrandados++;

        if(agrandados == 1){
            
            
            for(int i=0;i<2;i++){
                aux.x = tamGrande;
                transform.localScale = aux;
                yield return new WaitForSeconds(espera);
                aux.x = tamNormal;
                transform.localScale = aux;
                yield return new WaitForSeconds(espera);
            }

            aux.x = tamGrande;
            transform.localScale = aux;
            
        }
        
        yield return new WaitForSeconds(duracion);
        
        agrandados--;

        if(agrandados == 0){
            audioSource.PlayOneShot(sPequeño, volumen);
            for(int i=0;i<2;i++){
                aux.x = tamNormal;
                transform.localScale = aux;
                yield return new WaitForSeconds(espera);
                aux.x = tamGrande;
                transform.localScale = aux;
                yield return new WaitForSeconds(espera);
            }
            aux.x = tamNormal;
            transform.localScale = aux;
        }
        
    }
}
