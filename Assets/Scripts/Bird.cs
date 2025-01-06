using UnityEngine;
using UnityEngine.EventSystems;

public class Bird : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3.7f;
    private float rotationSpeed = 5f;
    private Rigidbody2D rb;

    private Vector2 birdStartPosition;
    private void Start()
    {
        birdStartPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        GameManager.instance.OnRestartGame += ResetBirdPosition;
    }
    private void ResetBirdPosition()
    {
        transform.position = birdStartPosition;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && Time.timeScale == 1f)
        {
            rb.velocity = Vector2.up * moveSpeed;
        }
        transform.rotation = Quaternion.Euler(0, 0, rb.velocity.y * rotationSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ground ground = collision.collider.GetComponent<Ground>();
        Pipe pipe = collision.collider.GetComponentInParent<Pipe>();
        
        if (ground || pipe)
        {
            GameManager.instance.GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collider2D collider = collision;

        Pipe pipe = collider.GetComponentInParent<Pipe>();

        if (pipe != null)
        {
            if (collider.isTrigger)
            {
                Score.instance.OnScoreZoneReached();
            }
        }
    }

}
