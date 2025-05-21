using UnityEngine;
using System.Collections;

public class Huevo : MonoBehaviour
{
    public Boss boss; // Referencia al boss para avisar
    public GameObject HuevoRotoPrefab; // Prefab para el huevo roto

    private Rigidbody2D rb;
    private Collider2D col;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Instancia el huevo roto un poco más abajo
        Vector3 posicionRota = transform.position + new Vector3(0f, -0.2f, 0f);
        if (!collision.gameObject.CompareTag("Bala"))
            Instantiate(HuevoRotoPrefab, posicionRota, Quaternion.identity);

        // Devuelve el huevo a la posición inicial y desactiva
            transform.position = boss.puntoDisparo.position;
        gameObject.SetActive(false);
        boss.HuevoDevuelto();
    }

    public void Lanzar(Vector2 direccion, float velocidad)
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (col == null) col = GetComponent<Collider2D>();

        gameObject.SetActive(true);
        col.enabled = false;

        float anguloAleatorio = Random.Range(-15f, 15f); // Rango en grados
        Vector2 direccionConAngulo = Quaternion.Euler(0, 0, anguloAleatorio) * direccion.normalized;

        rb.linearVelocity = direccionConAngulo * velocidad;

        StartCoroutine(ReactivarCollider(0.1f));
    }

    private IEnumerator ReactivarCollider(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (col != null)
            col.enabled = true;  // Activa collider para que empiece a detectar colisiones
    }
}
