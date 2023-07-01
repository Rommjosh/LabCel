using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Clase que controla el cambio de escenas y la acción de salir del juego.
/// </summary>
public class CambiarEscena : MonoBehaviour
{
    public int Esc;
    public AudioClip buttonClickSound; // Sonido al hacer clic en el botón
    private AudioSource audioSource; // Componente AudioSource para reproducir el sonido

    /// <summary>
    /// Método llamado al inicio del juego.
    /// </summary>
    private void Start()
    {
        // Obtener o agregar el componente AudioSource al objeto
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    /// <summary>
    /// Método llamado cuando se hace clic en el botón de iniciar.
    /// </summary>
    public void Iniciar()
    {
        StartCoroutine(PlaySoundAndLoadScene());
    }

    /// <summary>
    /// Coroutine que reproduce el sonido de clic y cambia a la siguiente escena.
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlaySoundAndLoadScene()
    {
        // Reproducir el sonido de clic
        if (buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }

        // Esperar a que el sonido termine de reproducirse
        yield return new WaitForSeconds(buttonClickSound.length);

        // Cambiar a la siguiente escena
        SceneManager.LoadScene(Esc);
    }

    /// <summary>
    /// Método llamado cuando se hace clic en el botón de salir.
    /// </summary>
    public void Salir()
    {
        Application.Quit();
    }
}