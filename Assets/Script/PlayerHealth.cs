using Unity.Netcode;
using UnityEngine;

public class PlayerHealth : NetworkBehaviour
{
    [SerializeField] private float maxHealth = 100f;

    public NetworkVariable<float> CurrentHealth = new NetworkVariable<float>(
        0f,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server
    );

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            CurrentHealth.Value = maxHealth;
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void TakeDamageServerRpc(float damage)
    {
        CurrentHealth.Value -= damage;
        Debug.Log($"Player took {damage} damage, current health: {CurrentHealth.Value}");

        if(CurrentHealth.Value <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        Debug.Log("Player died!");
    }


}