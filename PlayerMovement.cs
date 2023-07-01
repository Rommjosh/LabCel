using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Clase que controla el movimiento y las acciones del jugador.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;

    private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 8f; // Ajusta el valor para modificar la intensidad del salto
    private int lives = 3; // Cantidad de vidas del jugador
    public Text livesText; // Referencia al objeto Text para mostrar las vidas

    private bool isJumping = false; // Variable para controlar el salto

    /// <summary>
    /// Método llamado al inicio del juego. Obtiene las referencias necesarias y actualiza el texto de las vidas.
    /// </summary>
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        UpdateLivesText();
    }

    /// <summary>
    /// Método llamado en cada frame. Controla el movimiento del personaje y actualiza el estado de la animación.
    /// </summary>
    private void Update()
    {
        float dirx = Input.GetAxisRaw("Horizontal");

        // Use Vector2.MoveTowards() to smoothly move the character towards the desired direction
        rb.velocity = Vector2.MoveTowards(rb.velocity, new Vector2(dirx * moveSpeed, 0f), Time.deltaTime);

        UpdateAnimationState();
    }

    /// <summary>
    /// Método que actualiza el estado de la animación del personaje.
    /// </summary>
    private void UpdateAnimationState()
    {
        if (rb.velocity.x > 0f)
        {
            anim.SetBool("running", true);
            sprite.flipX = false;
        }
        else if (rb.velocity.x < 0f)
        {
            anim.SetBool("running", true);
            sprite.flipX = true;
        }
        else
        {
            anim.SetBool("running", false);
        }
    }

    /// <summary>
    /// Método llamado cuando se realiza un salto.
    /// </summary>
    public void Jump()
    {
        if (!isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true;
        }
    }

    /// <summary>
    /// Método llamado cuando se presiona el botón de movimiento hacia la derecha.
    /// </summary>
    public void Right()
    {
        rb.velocity += new Vector2(moveSpeed, 0f);
    }

    /// <summary>
    /// Método llamado cuando se presiona el botón de movimiento hacia la izquierda.
    /// </summary>
    public void Left()
    {
        rb.velocity += new Vector2(-moveSpeed, 0f);
    }

    /// <summary>
    /// Método llamado para detener el movimiento del personaje.
    /// </summary>
    public void StopMovement()
    {
        rb.velocity = new Vector2(0f, 0f);
    }

    /// <summary>
    /// Método llamado cuando el jugador pierde una vida.
    /// </summary>
    public void LoseLife()
    {
        if (lives <= 0)
        {
            // Realizar acciones cuando el jugador pierde la última vida
            lives = 0;
            UpdateLivesText();
            // Cargar la escena "Inicio"
            SceneManager.LoadScene("Scenes/Nivel 1");
        }
        else
        {
            SceneManager.LoadScene("Scenes/Nivel 1");
        }
    }

    /// <summary>
    /// Método que actualiza el texto de las vidas en pantalla.
    /// </summary>
    private void UpdateLivesText()
    {
        // Actualizar el texto de las vidas
        livesText.text = lives.ToString();
    }

    /// <summary>
    /// Método llamado cuando se produce una colisión.
    /// </summary>
    /// <param name="collision">Información de la colisión.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}