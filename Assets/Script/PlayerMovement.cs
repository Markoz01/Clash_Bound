using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody rb;
    private Vector3 movDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        movDirection = new Vector3(horizontal, 0f, vertical).normalized;

    }


    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movDirection * moveSpeed * Time.fixedDeltaTime);
    }

}
