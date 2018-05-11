using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerManager : NetworkBehaviour
{
    public static PlayerManager Instance;

    [SerializeField] NetworkIdentity identity;
    [SerializeField] Player playerPrefab;

    [SerializeField] Camera deadCamera;
    [SerializeField] Canvas deadCanvas;
    
    Player player;

    void Start()
    {
        if (identity.isLocalPlayer)
        {
            if (Instance != null)
            {
                Debug.LogError("PlayerManager Instance already exists!!!!!!");
            }

            Instance = this;
        }
        else
        {
            Destroy(deadCamera.gameObject);
            Destroy(deadCanvas.gameObject);
        }

        transform.position = new Vector3(0, -10, 0);
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void OnPlayerDeath()
    {
        if (isLocalPlayer)
        {
            deadCamera.enabled = true;
        }
    }

    public void OnLocalPlayerSpawn(Player player)
    {
        this.player = player;

        deadCamera.enabled = false;
    }

    [Command]
    void CmdSpawnPlayer()
    {
        Transform spawnPoint = NetworkManager.singleton.GetStartPosition();

        player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);

        NetworkServer.SpawnWithClientAuthority(player.gameObject, gameObject);

        player.RpcInitialize();
    }

    void Update()
    {
        if (identity.hasAuthority)
        {
            if (player == null)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    CmdSpawnPlayer();
                }
            }
        }
    }
}
