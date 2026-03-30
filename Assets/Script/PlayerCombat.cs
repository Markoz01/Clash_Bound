using UnityEngine;
using Unity.Netcode;
using System.Collections;
using Unity.VisualScripting;

public class PlayerCombat : NetworkBehaviour
{
    [SerializeField] private KeyCode attackKey = KeyCode.E;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackOffset = 1f;

    private Renderer playerRenderer;
    private Color originalColor;


    private void Awake()
    {
        playerRenderer = GetComponent<Renderer>();
        originalColor = playerRenderer.material.color;
    }

    private IEnumerator AttackFlash()
    {
        playerRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        playerRenderer.material.color = originalColor;
    
    }


    private void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKeyDown(attackKey))
        {
            Attack();
        }
    }

    private void Attack()
    {
        StartCoroutine(AttackFlash());
        
        Vector3 attackPoint = transform.position + transform.forward * attackOffset;
        Collider[] hits = Physics.OverlapSphere(attackPoint, attackRange);

        foreach(Collider hit in hits)
        {
            if(hit.gameObject == gameObject) continue;

            PlayerHealth health = hit.GetComponent<PlayerHealth>();
            if(health != null)
            {
                health.TakeDamageServerRpc(attackDamage);
            }

        }

    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 attackPoint = transform.position + transform.forward * attackOffset;
        Gizmos.DrawWireSphere(attackPoint, attackRange);
    }



}
