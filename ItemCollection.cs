using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Representa el comportamiento de recolección de objetos.
/// </summary>
public class ItemCollection : MonoBehaviour
{
    /// <summary>
    /// Referencia al objeto Texto para mostrar los puntos.
    /// </summary>
    public Text points;

    /// <summary>
    /// Nombre de la escena a la que se cambiará.
    /// </summary>
    public string nextSceneName;

    /// <summary>
    /// Sonido a reproducir cuando se come un nutriente.
    /// </summary>
    public AudioClip eatSound;

    /// <summary>
    /// Duración del sonido en segundos.
    /// </summary>
    public float soundDuration = 1f;

    private AudioSource audioSource;

    private int score = 0;

    private void Start()
    {
        // Obtener o agregar el componente AudioSource al objeto.
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Nutriente"))
        {
            Destroy(collision.gameObject);
            score += 10; // Sumar 10 puntos cuando se "come" un objeto.
            points.text = score.ToString(); // Actualizar el texto de los puntos.

            // Reproducir el sonido de comer.
            if (eatSound != null)
            {
                StartCoroutine(PlaySoundAndDestroy(eatSound));
            }

            // Verificar si ya no hay nutrientes.
            if (GameObject.FindGameObjectsWithTag("Nutriente").Length == 0)
            {
                // Cambiar a la siguiente escena.
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }

    private System.Collections.IEnumerator PlaySoundAndDestroy(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
        yield return new WaitForSeconds(soundDuration);
        audioSource.Stop();
    }

    private void Update()
    {
        // Verificar si ya no hay nutrientes.
        if (GameObject.FindGameObjectsWithTag("Nutriente").Length == 0)
        {
            // Cambiar a la siguiente escena.
            SceneManager.LoadScene(nextSceneName);
        }
    }
}

/// <summary>
/// Representa un objeto nutriente.
/// </summary>
public class Nutriente : MonoBehaviour
{
    // Agrega cualquier funcionalidad o propiedades específicas para el nutriente aquí.
}

/// <summary>
/// Representa un gestor de escenas para cargar escenas.
/// </summary>
public static class MiSceneManager
{
    /// <summary>
    /// Carga la escena especificada por su nombre.
    /// </summary>
    /// <param name="sceneName">El nombre de la escena a cargar.</param>
    public static void LoadScene(string sceneName)
    {
        // Agrega cualquier lógica personalizada para cargar escenas aquí.
        SceneManager.LoadScene(sceneName);
    }
}
