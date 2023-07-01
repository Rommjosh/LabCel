using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Clase que controla el comportamiento de un enemigo.
/// </summary>
public class Enemy : MonoBehaviour
{
    public float speed = 0.5f; // Velocidad de movimiento del enemigo
    public Text livesText; // Referencia al objeto Text para mostrar las vidas
    public Vector3 playerStartPosition = new Vector3(-10.59f, -2.95f, 0f); // Posición inicial del jugador
    public AudioClip collisionSound; // Sonido de colisión
    public float collisionSoundDuration = 1f; // Duración de reproducción del sonido de colisión

    private int lives = 3; // Cantidad de vidas
    private Rigidbody2D rb;
    private float movementRange = 2f; // Rango máximo de movimiento del enemigo
    private AudioSource audioSource;

    /// <summary>
    /// Método llamado al inicio del juego.
    /// </summary>
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        UpdateLivesText();
    }

    /// <summary>
    /// Método llamado en cada frame.
    /// </summary>
    private void Update()
    {
        // Obtener la dirección de movimiento
        Vector2 movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        // Realizar un Raycast para detectar obstáculos
        RaycastHit2D hit = Physics2D.Raycast(rb.position, movement, movementRange);
        if (hit.collider != null && !hit.collider.CompareTag("Player"))
        {
            // Ajustar la dirección de movimiento si se detecta un obstáculo que no sea el jugador
            movement = Vector2.Reflect(movement, hit.normal);
        }

        // Mover al enemigo utilizando el Rigidbody y la velocidad
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }

    /// <summary>
    /// Método llamado cuando se produce una colisión.
    /// </summary>
    /// <param name="collision">Información de la colisión.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si colisiona con el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            // Reducir una vida
            lives--;
            UpdateLivesText();

            if (lives <= 0)
            {
                // Destruir al jugador si no quedan vidas
                //Destroy(collision.gameObject);
                //Instead of destroying the player, we will make them lose a life
                collision.gameObject.GetComponent<PlayerMovement>().LoseLife();
            }
            else
            {
                // Reposicionar al jugador en la posición inicial
                collision.gameObject.transform.position = playerStartPosition;
            }

            // Reproducir el sonido de colisión
            if (collisionSound != null)
            {
                audioSource.PlayOneShot(collisionSound);
                StartCoroutine(StopCollisionSound());
            }
        }
    }

    /// <summary>
    /// Coroutine que detiene la reproducción del sonido de colisión después de un tiempo.
    /// </summary>
    /// <returns></returns>
    private IEnumerator StopCollisionSound()
    {
        yield return new WaitForSeconds(collisionSoundDuration);

        // Detener la reproducción del sonido de colisión
        audioSource.Stop();
    }

    /// <summary>
    /// Método que actualiza el texto de las vidas en pantalla.
    /// </summary>
    private void UpdateLivesText()
    {
        // Actualizar el texto de las vidas
        livesText.text = "" + lives.ToString();
    }
}