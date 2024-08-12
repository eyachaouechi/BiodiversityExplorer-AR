using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class LineProjection : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public float baseCurveFactor = 1.0f;
    public float minCurveFactor = 0.1f;
    public float maxCurveFactor = 2.0f;
    public int numPoints = 10; // Adjust the number of points for a smoother curve.

    public bool isHolding;

    Camera arCamera;
    GameObject capturedObject;
    Rigidbody capturedObjectRB;
    BounceObject bounceObject;


    Vector3 capturedObjectStartingPosition;


    void Start()
    {

        arCamera = Camera.main;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;

    }

    void Update()
    {

       if (isHolding)
        {
            // Define the start point at the bottom center of the screen
            Vector3 start = arCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.0f, arCamera.nearClipPlane));

           

 
            if(capturedObject != null)
            {



                // Calculate the control points for the Bezier curve
                Vector3 controlPoint1 = start;
                Vector3 controlPoint2 = Vector3.Lerp(start, capturedObject.transform.position, 0.5f) + Vector3.up * baseCurveFactor * capturedObjectRB.mass;

                // Clamp control points within the screen boundaries
                controlPoint1 = ClampToScreenSize(controlPoint1);
                controlPoint2 = ClampToScreenSize(controlPoint2);

                // Set the Line Renderer's positions to create a curve with more points
                lineRenderer.enabled = true;
                lineRenderer.positionCount = numPoints;

                for (int i = 0; i < numPoints; i++)
                {
                    float t = i / (float)(numPoints - 1);
                    Vector3 point = BezierCurve(start, controlPoint1, controlPoint2, capturedObject.transform.position, t);
                    lineRenderer.SetPosition(i, point);
                }


            }
            else
            {   
                // Raycast from the center of the screen
                Ray ray = arCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                 {
                    if (!hit.collider.GetComponent<BounceObject>())
                        return;
                    
                    hit.rigidbody.isKinematic = true;
                    hit.rigidbody.useGravity = false;

                    // Calculate the control points for the Bezier curve
                    Vector3 controlPoint1 = start;
                    Vector3 controlPoint2 = Vector3.Lerp(start, hit.point, 0.5f) + Vector3.up * baseCurveFactor * hit.rigidbody.mass;

                    // Clamp control points within the screen boundaries
                    controlPoint1 = ClampToScreenSize(controlPoint1);
                    controlPoint2 = ClampToScreenSize(controlPoint2);

                    // Set the Line Renderer's positions to create a curve with more points
                    lineRenderer.enabled = true;
                    lineRenderer.positionCount = numPoints;

                    for (int i = 0; i < numPoints; i++)
                    {
                        float t = i / (float)(numPoints - 1);
                        Vector3 point = BezierCurve(start, controlPoint1, controlPoint2, hit.point, t);
                        lineRenderer.SetPosition(i, point);
                    }

                    // Reference the hitted oject 
                    capturedObject = hit.rigidbody.gameObject;
                    capturedObjectRB = capturedObject.GetComponent<Rigidbody>();
                    bounceObject = capturedObject.GetComponent<BounceObject>();
                    bounceObject.enabled = true;


                    // Make the hitted object child of the camera 
                    capturedObject.transform.parent = arCamera.transform;

                 }
                else
                {
                    // If the raycast doesn't hit anything, set the Line Renderer's positions to a straight line
                    lineRenderer.enabled = true;
                    lineRenderer.positionCount = 2;
                    lineRenderer.SetPosition(0, arCamera.transform.position);
                    lineRenderer.SetPosition(1, arCamera.transform.position);
                 }

            }
        }

  
    }




    Vector3 BezierCurve(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttu = tt * u;
        float uut = uu * t;

        Vector3 p = uuu * p0;
        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += tt * t * p3;

        return p;
    }

    Vector3 ClampToScreenSize(Vector3 position)
    {
        Vector3 clampedPosition = position;

        float halfScreenWidth = arCamera.orthographicSize * arCamera.aspect;
        float halfScreenHeight = arCamera.orthographicSize;

        clampedPosition.x = Mathf.Clamp(clampedPosition.x, arCamera.transform.position.x - halfScreenWidth, arCamera.transform.position.x + halfScreenWidth);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, arCamera.transform.position.y - halfScreenHeight, arCamera.transform.position.y + halfScreenHeight);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, arCamera.transform.position.z - arCamera.orthographicSize, arCamera.transform.position.z + arCamera.orthographicSize);

        return clampedPosition;
    }



    public void onPick()
    {
        isHolding =! isHolding; 

        if(!isHolding)
        {
            capturedObject.transform.parent = null;
            bounceObject.enabled = false;
            bounceObject = null;
            capturedObjectRB.isKinematic = false;
            capturedObjectRB.useGravity = true;
            capturedObjectRB = null;
            capturedObject = null;

            lineRenderer.enabled = true;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, arCamera.transform.position);
            lineRenderer.SetPosition(1, arCamera.transform.position);

        }

    }

}


