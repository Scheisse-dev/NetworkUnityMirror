using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class FireBall : NetworkBehaviour
{
    [SerializeField] float moveForce = 500;
    Rigidbody rb = null;
    GameObject owner = null;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        InvokeRepeating(nameof(DestroyDelay), 10, 10);
        
    }
    void Update()
    {
        rb.velocity = transform.forward * moveForce;
    }

    public void Init(GameObject _owner)
    {
        owner = _owner;
    }


    private void OnTriggerEnter(Collider other)
    {
        TypeId _id = other.GetComponent<TypeId>();
        if (_id && _id.owner == owner || !owner) return;
        DestroyServerRpc();
    }


    void DestroyDelay()
    {
        DestroyServerRpc();
        CancelInvoke(nameof(DestroyDelay));
    }

    [Command(requiresAuthority = false)]
    public void DestroyServerRpc()
    {
        Destroy(gameObject);
    }
}
