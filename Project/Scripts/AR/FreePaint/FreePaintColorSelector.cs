using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FreePaintColorSelector : MonoBehaviour
{

    Color finalColor;


    static FreePaintColorSelector myslf;

    void Awake()
    {
        myslf = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        finalColor = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ChangeMaterial(string RGB)
    {
        if (UnityEngine.ColorUtility.TryParseHtmlString("#" + RGB, out Color color))
        {
            finalColor = color;
        }else
        Debug.LogWarning("Invalid hexadecimal color: " + RGB);     
    }


    public static Color GetColor()
    {
        return myslf.finalColor;
    }
}
