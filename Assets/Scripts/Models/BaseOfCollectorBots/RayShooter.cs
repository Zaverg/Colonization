using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class RayShooter
{
    private Camera _main;

    public RayShooter()
    {
        _main = Camera.main;
    }

    public Transform CameraShoot()
    {
        Ray ray = _main.ScreenPointToRay(Mouse.current.position.ReadValue());

        Physics.Raycast(ray, out RaycastHit hit, 1000);

        return hit.transform;
    }

    public Transform CameraShootToUI()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Mouse.current.position.ReadValue();

        List<RaycastResult> results = new List<RaycastResult>();

        EventSystem.current.RaycastAll(pointerEventData, results);

        Transform result = null;

        if (results.Count > 0)
            result = results[0].gameObject.transform;

        return result;
    }
}