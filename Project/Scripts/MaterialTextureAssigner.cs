using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

public class MaterialTextureAssigner : MonoBehaviour
{
    public List<Material> materialsToAssign = new List<Material>(); // Assign materials in the Inspector
    string texturesFolder; // Specify the folder with your textures

    void Start()
    {
        // Get all textures in the specified folder
        texturesFolder = Application.persistentDataPath + "/FreePaintedTexture"; // Change "*.png" to match your texture file format
        string[] texturePaths = Directory.GetFiles(texturesFolder, "*.png"); // Change "*.png" to match your texture file format

        // Loop through the list of materials
        foreach (Material material in materialsToAssign)
        {
            if (material != null)
            {
                // Find a matching texture by name
                string materialName = material.name; // Assuming material name matches texture name
                string matchingTexturePath = Array.Find(texturePaths, path => Path.GetFileNameWithoutExtension(path) == materialName);

                if (matchingTexturePath != null)
                {
                    // Load the texture
                    Texture2D texture = LoadTexture(matchingTexturePath);

                    if (texture != null)
                    {
                        // Assign the texture to the material's main texture (albedo)
                        material.mainTexture = texture;
                    }
                }
            }
        }
    }

    Texture2D LoadTexture(string path)
    {
        byte[] fileData = File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(2, 2); // You can specify dimensions if needed
        if (texture.LoadImage(fileData))
        {
            return texture;
        }
        return null;
    }
}
