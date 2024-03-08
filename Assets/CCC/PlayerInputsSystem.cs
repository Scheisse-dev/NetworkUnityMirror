using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputsSystem : MonoBehaviour
{
    [SerializeField] PlayerInputs controls = null;
    [field: SerializeField] public InputAction Move { get; private set; } = null;
    [field: SerializeField] public InputAction Fire { get; private set; } = null;
    [field: SerializeField] public InputAction RotateYaw { get; private set; } = null;
    [field: SerializeField] public InputAction Chat { get; private set; } = null;

    private void Awake()
    {
        controls = new();
    }

    private void OnEnable()
    {
        Move = controls.Player.Move;
        Move.Enable();
        Fire = controls.Player.Fire;
        Fire.Enable();
        RotateYaw = controls.Player.RotateYaw;
        RotateYaw.Enable();
        Chat = controls.Player.OpenCloseChat;
        Chat.Enable();
    }

    void OnDisable()
    {
        Move.Disable();
        Fire.Disable();
        RotateYaw.Disable();
        Chat.Disable();
    }
}
