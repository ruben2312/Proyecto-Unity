using UnityEngine;
using UnityEngine.SceneManagement;

public class MovTanque : MonoBehaviour
{
    public Transform rotacion; 
    public GameObject balaPrefab;
    public Transform puntoDisparo;
    public float velocidadBala = 20f;
    public float minRotation = -10f;
    public float maxRotation = 35f;
    private GameObject bala;
    private Rigidbody2D rbBala;
    private bool balaLista = true;

    void Start()
    {
        bala = Instantiate(balaPrefab, puntoDisparo.position, puntoDisparo.rotation);
        rbBala = bala.GetComponent<Rigidbody2D>();
        bala.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - rotacion.position;

        direction.z = 0;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, minRotation, maxRotation);

        rotacion.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (Input.GetMouseButtonDown(0) && balaLista)
        {
            Disparar();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Huevo"))
        {
            gameObject.GetComponent<BarraVida>().QuitarVida(10f);
            if (gameObject.GetComponent<BarraVida>().vidaActual <= 0)
            {
                SceneManager.LoadScene("NivelTanque");
            }
        }
    }

    void Disparar()
    {
        bala.transform.position = puntoDisparo.position;
        bala.transform.rotation = puntoDisparo.rotation;
        bala.SetActive(true);
        rbBala.linearVelocity = -puntoDisparo.right * velocidadBala;
        balaLista = false;
    }
    
    public void RecogerBala()
    {
        rbBala.linearVelocity = Vector2.zero;
        bala.SetActive(false);
        balaLista = true;
    }
}
