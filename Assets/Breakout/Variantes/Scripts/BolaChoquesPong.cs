using System;
using Unity.VisualScripting;
using UnityEngine;

public class BolaChoquesPong : MonoBehaviour
{

    public AudioSource golpe;
    public GameObject explosion;

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.linearVelocity.magnitude > 20f){
            rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, 10f);
        }
        if(rb.linearVelocity.magnitude < 5f){
            rb.linearVelocity = Vector3.Normalize(rb.linearVelocity) * 5f;
        }
        if(Math.Abs(rb.linearVelocity.x) < 3f){
            rb.linearVelocity = new Vector2(rb.linearVelocity.x*2f, rb.linearVelocity.y);
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Brick")){

            
            
            GameObject particula = Instantiate(explosion, collision.gameObject.transform.position, gameObject.transform.rotation);
            ParticleSystem ps = particula.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule ma = ps.main;
            ma.startColor = collision.gameObject.GetComponent<SpriteRenderer>().color;
            Destroy(particula, 2f);
            Destroy(collision.gameObject);

            
        }
        golpe.Play();
        
    }
    
    

}

