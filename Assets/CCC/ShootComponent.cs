using System.Collections;
using System.Collections.Generic;
using Steamworks;
using Mirror;
using UnityEngine;

public class ShootComponent : NetworkBehaviour
{
    [SerializeField] Transform spawnTransform = null;
    [SerializeField] FireBall projectile = null;



    public void SpawnProjectile()
    {
        if (!spawnTransform || !projectile) return;

        ShootServerRPC();
    }


    [Command]
    public void ShootServerRPC()
    {
        FireBall _fireball = Instantiate<FireBall>(projectile, spawnTransform.position, transform.rotation);
		NetworkServer.Spawn(_fireball.gameObject);
		_fireball.Init(gameObject);
    }
}
