using UnityEngine;
using UnityEngine.Tilemaps;
/// <summary>
/// Clase que escala un Tilemap en función del tamaño de la pantalla.
/// </summary>
public class TilemapScaler : MonoBehaviour
{
    public Tilemap tilemap; // Referencia al Tilemap
    private Camera mainCamera; // Referencia a la cámara principal

    /// <summary>
    /// Método llamado al inicio del juego.
    /// </summary>
    private void Start()
    {
        // Obtener la referencia a la cámara principal
        mainCamera = Camera.main;
    }
    /// <summary>
    /// Método llamado en cada frame.
    /// </summary>
    private void Update()
    {
        // Actualizar el escalado del Tilemap si el tamaño de la pantalla cambia
        if (Screen.width != tilemap.size.x || Screen.height != tilemap.size.y)
        {
            ScaleTilemap();
        }
    }
    /// <summary>
    /// Escala el Tilemap según las dimensiones de la pantalla.
    /// </summary>
    private void ScaleTilemap()
    {
        if (tilemap == null || mainCamera == null)
        {
            Debug.LogWarning("Tilemap o cámara principal no asignados en el script TilemapScaler.");
            return;
        }

        // Obtener las dimensiones de la pantalla en unidades del mundo
        float screenHeightInWorldUnits = mainCamera.orthographicSize * 2f;
        float screenWidthInWorldUnits = screenHeightInWorldUnits * mainCamera.aspect;

        // Escalar el Tilemap según las dimensiones de la pantalla
        tilemap.size = new Vector3Int(Mathf.RoundToInt(screenWidthInWorldUnits), Mathf.RoundToInt(screenHeightInWorldUnits), 1);
    }
}