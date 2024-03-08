using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;
using Steamworks;

[RequireComponent(typeof(PlayerInputsSystem))]
public class PlayerComponent : NetworkBehaviour
{
    [SerializeField] PlayerInputsSystem inputs = null;
    [SerializeField, Range(.1f, 100)] float movementSpeed = 2;
    [SerializeField] float rotateSpeed = .5f;
    [SerializeField] CharacterController controller = null;
    [SerializeField] ShootComponent shoot = null;
    [SerializeField] Camera cam = null;

    bool canMove = false;


    void Start()
    {
		canMove = true;
		inputs = GetComponent<PlayerInputsSystem>();
        controller = GetComponent<CharacterController>();
        shoot = GetComponent<ShootComponent>();
        //if (isServer)
        //    inputs.Fire.performed += Shoot;
        InitCam();
    }

	public override void OnStartClient()
	{
		//transform.position = NavZone.Instance.GetNavigablePoint();
		canMove = true;
		base.OnStartClient();
	}

	//public override void OnNetworkSpawn()
 //   {
 //       transform.position = NavZone.Instance.GetNavigablePoint();
 //       canMove = true;
 //       base.OnNetworkSpawn();
 //   }


    void InitCam()
    {
        if (isOwned)
            cam.enabled = true;

    }
    private void Update()
    {
        Move();
        RotateYaw();
    }
    private void Move()
    {
        if (!isOwned || !canMove) return;
        Vector2 _axis = inputs.Move.ReadValue<Vector2>();
        Vector3 _movement = transform.forward * _axis.y + transform.right * _axis.x;
        transform.position += _movement * movementSpeed * Time.deltaTime;
    }
    void RotateYaw()
    {
        if (!isOwned) return;
        float _axis = inputs.RotateYaw.ReadValue<float>();
        transform.eulerAngles += new Vector3(0, _axis * rotateSpeed, 0);
    }
    void Shoot(InputAction.CallbackContext _context)
    {
        bool _test = _context.ReadValueAsButton();
        if (!_test) return;

        shoot.SpawnProjectile();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawRay(new Ray(transform.position, transform.forward));
        Gizmos.DrawSphere(transform.position + transform.forward, .1f);
    }


    [Command]
    void DestroyPlayerServerRpc()
    {
        Debug.Log("despawn Player");
        Destroy(gameObject);
    }
}
