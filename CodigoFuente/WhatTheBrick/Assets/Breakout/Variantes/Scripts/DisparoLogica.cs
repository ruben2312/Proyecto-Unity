using Unity.VisualScripting;
using UnityEngine;

public class DisparoLogica : MonoBehaviour
{
    Rigidbody2D rb;
    public float velocidad = 6f;
    public float minimoVertical = -5.5f;

    public GameObject explosion;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.down * velocidad;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < minimoVertical){
            Destroy(this.GameObject());
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Paleta")){
            //Debug.Log("Golpe");
            
            if(collision.gameObject.GetComponent<VidaPlayer>() == null){
                collision.gameObject.transform.parent.GetComponent<VidaPlayer>().Daño();
            }
            else{
                collision.gameObject.GetComponent<VidaPlayer>().Daño();
            }

            GameObject expInstancia = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(expInstancia, 2f);
            
            Destroy(this.GameObject());
        }
    }

}
