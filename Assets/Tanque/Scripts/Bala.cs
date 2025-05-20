using UnityEngine;

public class Bala : MonoBehaviour
{
    public GameObject explosionPrefab;
    public AudioClip sonidoBala; // Sonido de la bala
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (explosionPrefab != null)
        {
            AudioSource.PlayClipAtPoint(sonidoBala, Camera.main.transform.position, 0.5f); // Reproduce el sonido de la bala
            ContactPoint2D contacto = collision.GetContact(0);
            Vector3 posicion = new Vector3(contacto.point.x, contacto.point.y, -415f); // z = 0
            Instantiate(explosionPrefab, posicion, Quaternion.identity);
        }
        // Buscar el tanque y llamar a RecogerBala+
        MovTanque tanque = FindFirstObjectByType<MovTanque>();
        Boss boss = collision.gameObject.GetComponent<Boss>();

        if (boss != null)
        {
            boss.Flash(); // Hace el flash rojo
        }
        if (tanque != null)
        {
            tanque.RecogerBala();
        }
    }
}
