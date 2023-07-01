using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase que controla el movimiento de la cámara siguiendo al jugador.
/// </summary>
public class Camaracontroller : MonoBehaviour
{
    [SerializeField] private Transform player; // Referencia al transform del jugador

    /// <summary>
    /// Método llamado en cada frame.
    /// </summary>
    void Update()
    {
        // Actualizar la posición de la cámara para seguir al jugador en el eje X e Y
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}