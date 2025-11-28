using UnityEngine;

public class PlayerControllerMobile : MonoBehaviour
{
    public float slideForce = 10f;
    private Rigidbody2D rb;
    private bool canInput = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Teclado para probar en PC
        if (!canInput) return;

        Vector2 dir = Vector2.zero;

        if (Input.GetKeyDown(KeyCode.UpArrow)) dir = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.DownArrow)) dir = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) dir = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.RightArrow)) dir = Vector2.right;

        if (dir != Vector2.zero)
        {
            StartSlide(dir);
        }
    }

    public void SlideUp()
    {
        if (!canInput) return;
        StartSlide(Vector2.up);
    }

    public void SlideDown()
    {
        if (!canInput) return;
        StartSlide(Vector2.down);
    }

    public void SlideLeft()
    {
        if (!canInput) return;
        StartSlide(Vector2.left);
    }

    public void SlideRight()
    {
        if (!canInput) return;
        StartSlide(Vector2.right);
    }

    private void StartSlide(Vector2 dir)
    {
        rb.linearVelocity = Vector2.zero; // resetea movimiento actual
        rb.AddForce(dir.normalized * slideForce, ForceMode2D.Impulse);
        canInput = false;           // bloquea input mientras se desliza
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si choca contra pared, se detiene y puedes volver a poner dirección
        if (collision.collider.CompareTag("Wall"))
        {
            rb.linearVelocity = Vector2.zero;
            canInput = true;
        }

        // Más adelante: hazards, etc.
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Goal"))
        {
            Debug.Log("LEVEL COMPLETE!");
            // Más adelante aquí avisamos a un GameManager
        }
    }
}
