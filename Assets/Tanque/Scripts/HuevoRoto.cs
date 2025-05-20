using UnityEngine;
using System.Collections;

public class HuevoRoto : MonoBehaviour
{
    public AudioSource SonidoHuevoRoto; // Sonido del huevo roto
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        SonidoHuevoRoto.Play(); // Reproduce el sonido del huevo roto
        StartCoroutine(FadeOutSprite());
    }

    private IEnumerator FadeOutSprite()
    {
        float duration = 2f;
        float elapsed = 0f;
        Color originalColor = sr.color;

        yield return new WaitForSeconds(2f); // Espera un poco antes de empezar el fade

        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject); // Elimina el objeto cuando termina el fade
    }
}

