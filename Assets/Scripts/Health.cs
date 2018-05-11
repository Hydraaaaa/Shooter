using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Health : NetworkBehaviour
{
    public System.Action<int> OnDamaged;
    public System.Action OnDeath;

    public int CurrentHealth { get { return Mathf.CeilToInt(currentHealth); } }
    public int MaxHealth { get { return Mathf.CeilToInt(maxHealth); } }

    [SerializeField] protected float currentHealth = 100;
    [SerializeField] protected float maxHealth = 100;

    public void Damage(float value)
    {
        CmdDamage(value);
    }
    
    [Command]
    void CmdDamage(float value)
    {
        if (value <= 0 ||
            currentHealth == 0)
        {
            return;
        }

        int intDamage = Mathf.CeilToInt(currentHealth);

        currentHealth -= value;

        intDamage -= Mathf.CeilToInt(currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            RpcSyncHealth(currentHealth);

            RpcDeath();

            return;
        }

        RpcSyncHealth(currentHealth);

        if (intDamage > 0)
        {
            RpcDamage(intDamage);
        }
    }

    [ClientRpc]
    void RpcSyncHealth(float value)
    {
        currentHealth = value;
    }

    [ClientRpc]
    void RpcDamage(int value)
    {
        if (OnDamaged != null)
        {
            OnDamaged(value);
        }
    }

    [ClientRpc]
    void RpcDeath()
    {
        if (OnDeath != null)
        {
            OnDeath();
        }
    }
}
