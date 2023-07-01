using UnityEngine;
/// <summary>
/// Clase que controla el audio de fondo de la escena.
/// </summary>
public class SceneAudio : MonoBehaviour
{
    public AudioClip backgroundMusic; // Sonido de fondo para la escena
    public bool loop = true; // Indica si el sonido de fondo debe repetirse en bucle
    public float volume = 1f; // Volumen del sonido de fondo

    private AudioSource audioSource; // Componente AudioSource para reproducir el sonido de fondo

    /// <summary>
    /// Método llamado al inicio de la escena.
    /// </summary>
    private void Start()
    {
        // Obtener o agregar el componente AudioSource a la cámara principal
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Configurar los parámetros del sonido de fondo
        audioSource.clip = backgroundMusic;
        audioSource.loop = loop;
        audioSource.volume = volume;

        // Reproducir el sonido de fondo
        audioSource.Play();
    }
}