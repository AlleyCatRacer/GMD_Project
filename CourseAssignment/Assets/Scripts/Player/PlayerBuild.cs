using System;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerBuild : MonoBehaviour
{
    [SerializeField] private GameObject[] buildablePrefabs;
    [SerializeField] private Transform buildRenderer;

    private GameObject _selectedObject;

    void Update()
    {
        // Check if Player has requested a change to the active item
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Select(1);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            Select(2);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            Select(3);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            Select(4);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            Select(5);
        if (Input.GetKeyDown(KeyCode.Alpha6))
            Select(6);
        if (Input.GetKeyDown(KeyCode.Alpha7))
            Select(7);
        if (Input.GetKeyDown(KeyCode.Alpha8))
            Select(8);
        if (Input.GetKeyDown(KeyCode.Alpha9))
            Select(9);

        // Fix transform
        buildRenderer.transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);

        // Check if it should be built
        if (Input.GetKeyDown(KeyCode.Mouse0))
            BuildSelected();
    }

    private void Select(int index)
    {
        if (index <= buildablePrefabs.Length)
        {
            index--;
            // If selecting same as currently selected, set selection to null, otherwise to selected item
            _selectedObject = buildablePrefabs[index] == _selectedObject ? null : buildablePrefabs[index];

            // Perform Rendering
            RenderSelected();
        }
        else
        {
            // Arguments... might be useful
            throw new ArgumentOutOfRangeException(nameof(index), $"index was greater than {buildablePrefabs.Length}");
        }
    }

    private void RenderSelected()
    {
        // Remove all current children
        var children = buildRenderer.GetComponentsInChildren<Transform>()[1..];
        foreach (var child in children)
            Destroy(child.gameObject);

        // Show / Hide item
        if (_selectedObject == null)
        {
            // Hide Renderer
        }
        else
        {
            // Show Renderer
            var selectedGameObject = Instantiate(_selectedObject, buildRenderer.transform);
            selectedGameObject.GetComponent<Rigidbody>().useGravity = false;

            // Disable Shadows for all rendered items
            var renderers = selectedGameObject.GetComponentsInChildren<MeshRenderer>();
            foreach (var meshRenderer in renderers) meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
        }
    }

    private void BuildSelected()
    {
        if (_selectedObject == null) return;
        
        // Spawn new Tower
        var spawnedItem = Instantiate(_selectedObject, buildRenderer.transform.position, buildRenderer.transform.rotation);
        
        // Clear the Rendering
        _selectedObject = null;
        RenderSelected();
    }
    
}