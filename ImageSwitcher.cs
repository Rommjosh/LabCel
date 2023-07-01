using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Clase que controla el cambio de imágenes al hacer clic en un botón.
/// </summary>
public class ImageSwitcher : MonoBehaviour
{
    public Image imageToShow; // Referencia a la imagen que se mostrará al hacer clic
    public Image currentImage; // Referencia a la imagen actualmente visible

    /// <summary>
    /// Método llamado al inicio del juego.
    /// </summary>
    private void Start()
    {
        // No es necesario obtener la imagen actual a través de GetComponent,
        // ya que se asignará directamente en el Inspector de Unity.
    }

    /// <summary>
    /// Método llamado cuando se hace clic en el botón.
    /// </summary>
    public void OnButtonClick()
    {
        // Mostrar la imagen deseada
        imageToShow.gameObject.SetActive(true);

        // Ocultar la imagen actual y la imagen del botón
        currentImage.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}