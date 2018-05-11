using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject thing;
    [SerializeField] MaterialEffects effects;
    [SerializeField] Collider[] ignoredColliders;

	void Update ()
    {
        if (Input.GetMouseButtonDown(0) && player.Identity.hasAuthority)
        {
            CmdShoot(player.Camera.transform.position, player.Camera.transform.forward);
        }
	}

    [Command]
    void CmdShoot(Vector3 position, Vector3 direction)
    {
        Ray ray = new Ray(position, direction);
        RaycastHit hit;

        for (int i = 0; i < ignoredColliders.Length; i++)
        {
            ignoredColliders[i].enabled = false;
        }

        if (Physics.Raycast(ray, out hit))
        {
            Health health = hit.transform.GetComponentInParent<Health>();

            if (health != null)
            {
                health.Damage(20);
            }

            Renderer hitRenderer = hit.transform.GetComponent<Renderer>();
            Material hitMaterial = null;

            if (hitRenderer != null)
            {
                hitMaterial = hitRenderer.sharedMaterial;
            }

            int effectIndex = effects.GetIndex(hitMaterial);

            RpcSpawnThing(hit.point, hit.normal, effectIndex);
        }

        for (int i = 0; i < ignoredColliders.Length; i++)
        {
            ignoredColliders[i].enabled = true;
        }
    }

    [ClientRpc]
    void RpcSpawnThing(Vector3 position, Vector3 normal, int effectIndex)
    {
        GameObject effect = effects.GetEffect(effectIndex);

        if (effect != null)
        {
            GameObject theThing = Instantiate(effect, position, Quaternion.identity);
            theThing.transform.forward = normal;
        }
    }
}
