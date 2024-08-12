using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Polyperfect.Common;
using TMPro;

public class Gaze : Singleton<Gaze>
{

    private static Gaze instance;

    public static List<InfoBehaviour> infos = new List<InfoBehaviour>();
    private int maxDistance = 10;
    private float sphereRad = 0.1f;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateInfos();
    }

    void Update()
    {

        RaycastHit hit;
        //RotaryHeart.Lib.PhysicsExtension.Physics.SphereCast(transform.position, sphereRad, transform.forward, out hit, maxDistance, RotaryHeart.Lib.PhysicsExtension.PreviewCondition.Both);


        if (Physics.SphereCast(transform.position, sphereRad, transform.forward, out hit, maxDistance))
        {
            GameObject go = hit.collider.gameObject;
            if (go.CompareTag("Hint"))
            {   
                OpenInfo(go.GetComponent<InfoBehaviour>());
            }
            else
            {
                CloseAll(); 
            }
        }
        
    }

    void OpenInfo(InfoBehaviour desiredInfo)
    {
  
        foreach (InfoBehaviour info in infos)
        {
            if (info == desiredInfo)
                info.OpenInfo();
            else
                info.CloseInfo();
        }
    }

    private void CloseAll()
    {
        foreach (InfoBehaviour info in infos)
            info.CloseInfo();
    }

    public static void UpdateInfos()
    {
        infos = FindObjectsOfType<InfoBehaviour>().ToList();
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(transform.position, transform.position + transform.forward * maxDistance);
        Gizmos.DrawSphere(transform.position + transform.forward * maxDistance, sphereRad);
    }
}
