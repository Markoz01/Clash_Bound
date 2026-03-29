using UnityEngine;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
    
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 15f;
    private Rigidbody rb;
    private Vector3 movDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        if(!IsOwner) return;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        movDirection = new Vector3(horizontal, 0f, vertical).normalized;

    }


    void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movDirection * moveSpeed * Time.fixedDeltaTime);
    }

    private void Rotate()
    {
        if(movDirection != Vector3.zero) return;
        
        Quaternion targetRotation = Quaternion.LookRotation(movDirection);
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));

    }

}
