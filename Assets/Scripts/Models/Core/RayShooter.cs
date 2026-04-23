using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class RayShooter
{
    private Camera _camera;
    private Mouse _mouse;

    public RayShooter()
    {
        _camera = Camera.main;
        _mouse = Mouse.current;
    }

    public bool RaycastWorld(out RaycastHit hit)
    {
        Ray ray = _camera.ScreenPointToRay(_mouse.position.ReadValue());

        return Physics.Raycast(ray, out hit, 1000);
    }

    public bool RaycastUI(out Transform result)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = _mouse.position.ReadValue();

        List<RaycastResult> results = new List<RaycastResult>();

        EventSystem.current.RaycastAll(pointerEventData, results);

        result = results[0].gameObject.transform;

        return results.Count > 0;
    }
}