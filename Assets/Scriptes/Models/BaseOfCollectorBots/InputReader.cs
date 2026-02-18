using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    private PlayerInput _playerInput;
    private RayShooter _rayShooter;

    public event Action<Transform> OnClick;

    public void Initialize()
    {
        _playerInput = new PlayerInput();
        _rayShooter = new RayShooter();

        gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        if (_playerInput == null)
            return;

        _playerInput.Enable();

        _playerInput.Player.Mouse.performed += OnLeftButtonDown;
    }

    private void OnDisable()
    {
        if (_playerInput == null)
            return;

        _playerInput.Player.Mouse.performed -= OnLeftButtonDown;

        _playerInput.Disable();
    }

    private void OnLeftButtonDown(InputAction.CallbackContext context)
    {
        Transform uiHit = _rayShooter.CameraShootToUI();
        Transform hit = _rayShooter.CameraShoot();

        Transform result;

        if (uiHit != null)
            result = uiHit;
        else
            result = hit;

        if (result.TryGetComponent(out IClickeble clickeble))
            clickeble.OnClick();
    }
}
