using UnityEngine;

public class UpdateChildMaterials : MonoBehaviour
{
    public Material newMaterial; // Assign the new material in the Inspector
    public GameObject indicator;
    void Start()
    {
        // Start the recursive process with the parent object
        UpdateMaterialsRecursively(indicator.transform);
    }

    void UpdateMaterialsRecursively(Transform parent)
    {
        // Loop through all immediate child objects of the current parent
        foreach (Transform child in parent)
        {
            Renderer renderer = child.GetComponent<Renderer>();

            if (renderer != null)
            {
                // Assign the new material to the renderer of each child object
                renderer.material = newMaterial;
            }

            // Recursively process the child's children
            UpdateMaterialsRecursively(child);
        }
    }
}
