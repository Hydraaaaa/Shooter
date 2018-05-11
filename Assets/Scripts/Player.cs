using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    public NetworkIdentity Identity { get { return identity; } }
    public Health Health { get { return health; } }
    public PlayerMovement Movement { get { return movement; } }
    public MouseLook MouseLook { get { return mouseLook; } }
    public Camera Camera { get { return camera; } }

    [SerializeField] NetworkIdentity identity;
    [SerializeField] Health health;
    [SerializeField] PlayerMovement movement;
    [SerializeField] MouseLook mouseLook;
    [SerializeField] Object[] localOnly;
    [SerializeField] Object[] remoteOnly;
    [SerializeField] new Camera camera;

    [ClientRpc]
    public void RpcInitialize()
    {
        health.OnDeath += () =>
        {
            Destroy(gameObject);
        };

        if (identity.hasAuthority)
        {
            PlayerManager.Instance.OnLocalPlayerSpawn(this);

            movement.Controller.detectCollisions = false;
            health.OnDeath += PlayerManager.Instance.OnPlayerDeath;

            for (int i = 0; i < remoteOnly.Length; i++)
            {
                Renderer renderer = remoteOnly[i] as Renderer;

                if (renderer != null)
                {
                    renderer.enabled = false;
                    continue;
                }

                Behaviour behaviour = remoteOnly[i] as Behaviour;

                if (behaviour != null)
                {
                    behaviour.enabled = false;
                    continue;
                }

                GameObject gameObject = remoteOnly[i] as GameObject;

                if (gameObject != null)
                {
                    gameObject.SetActive(false);
                    continue;
                }

                Destroy(remoteOnly[i]);
            }
        }
        else
        {
            for (int i = 0; i < localOnly.Length; i++)
            {
                Renderer renderer = localOnly[i] as Renderer;

                if (renderer != null)
                {
                    renderer.enabled = false;
                    continue;
                }

                Behaviour behaviour = localOnly[i] as Behaviour;

                if (behaviour != null)
                {
                    behaviour.enabled = false;
                    continue;
                }

                GameObject gameObject = localOnly[i] as GameObject;

                if (gameObject != null)
                {
                    gameObject.SetActive(false);
                    continue;
                }

                Destroy(localOnly[i]);
            }
        }
    }
}
