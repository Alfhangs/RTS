using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectorComponent : MonoBehaviour
{
    private MeshCollider _meshCollider = null;
    private Vector3 _startPosition;
    private List<UnitComponent> units = new List<UnitComponent>();
    private float distanceBetweenUnits = 2.0f;

    private void Awake()
    {
        GameObject plane = GameObject.FindGameObjectWithTag("Plane");
        if (plane != null)
        {
            _meshCollider = plane.GetComponent<MeshCollider>();
        }
        if (_meshCollider == null)
        {
            Debug.LogError("Missing tag and/or MeshCollider reference!");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _startPosition = GetMousePosition();
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Vector3 endPosition = GetMousePosition();
            SelectUnits(_startPosition, endPosition);
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            Vector3 movePosition = GetMousePosition();
            MoveSelectedUnits(movePosition);
        }
    }

    private Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;
        if (_meshCollider.Raycast(ray, out hitData, 1000))
        {
            return hitData.point;
        }
        return Vector3.zero;
    }

    private void SelectUnits(Vector3 startPosition, Vector3 endPosition)
    {
        foreach (UnitComponent unit in units)
        {
            unit.Selected(false);
        }
        units.Clear();
        Vector3 center = (startPosition + endPosition) / 2;
        float distance = Vector3.Distance(center, endPosition);
        Vector3 halfExtents = new Vector3(distance, distance, distance);


        Collider[] colliders = Physics.OverlapBox(center, halfExtents);
        foreach (Collider collider in colliders)
        {
            UnitComponent unit = collider.GetComponent<UnitComponent>();
            if (unit != null)
            {
                unit.Selected(true);
                units.Add(unit);
                Debug.Log(unit.name);
            }
        }
    }
    private void MoveSelectedUnits(Vector3 movePosition)
    {
        int rows = Mathf.RoundToInt(Mathf.Sqrt(units.Count));
        int counter = 0;
        for (int i = 0; i < units.Count; i++)
        {
            if (i > 0 && (1 % rows) == 0)
            {
                counter++;
            }
            float offsetX = (i % rows) * distanceBetweenUnits;
            float offsetZ = counter * distanceBetweenUnits;
            Vector3 offset = new Vector3(offsetX, 0, offsetZ);
            units[i].MoveTo(movePosition + offset);
        }
    }
}