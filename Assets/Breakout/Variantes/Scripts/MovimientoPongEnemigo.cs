using UnityEngine;

public class MovimientoPongEnemigo : MonoBehaviour
{
    public float maximo = 3.7f;

    float originalX;

    public GameObject bola;

    public Rigidbody2D rb;
	public float moveSpeed = 0.1f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalX = transform.position.x;
        rb = bola.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //movimientoHorizontal = Input.GetAxis("Horizontal");
        //transform.position += Vector3.right * movimientoHorizontal * velocidad * Time.deltaTime;

        int direccion = bola.transform.position.y > transform.position.y ? 1 : -1;

        transform.Translate(Vector2.up * direccion * moveSpeed * Time.deltaTime);
        
        
        
    }
}
