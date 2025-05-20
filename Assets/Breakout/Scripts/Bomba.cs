using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    Rigidbody2D rb;

    GameObject[] puntuacionTracker;
    PuntuacionTracker trackerClase;

    Color colorBola;

    public float velocidad = 8f;

    public GameObject explosionAnim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        puntuacionTracker = GameObject.FindGameObjectsWithTag("PuntuacionTracker");
        trackerClase = puntuacionTracker[0].GetComponent<PuntuacionTracker>();
        colorBola = GetComponent<SpriteRenderer>().color;

        rb.linearVelocity = Vector2.up * velocidad;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Brick") || other.gameObject.CompareTag("Brick powerup")){
            Instantiate(explosionAnim, other.gameObject.transform.position, other.gameObject.transform.rotation);
            Destroy(this.GameObject());
        }
    }
}
