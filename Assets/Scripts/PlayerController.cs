using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float movementSpeed = 5f;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandlePlayerMovement();
    }

    void HandlePlayerMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        movement = Vector2.ClampMagnitude(movement, 1f);

        _rb.velocity = movement * movementSpeed;

    }
}
