using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;


[RequireComponent(typeof(ARPlaneManager))]
[RequireComponent(typeof(ARPointCloudManager))]
public class ARCloudAnchorExperienceManager : Singleton<ARCloudAnchorExperienceManager>
{
    [SerializeField]
    private UnityEvent OnInitialized = null;

    [SerializeField]
    private UnityEvent OnRestarted = null;

    [SerializeField]
    private TextMeshProUGUI ScanText;


    private ARPlaneManager arPlaneManager = null;

    private ARPointCloudManager arPointCloudManager = null;

    private ARRaycastManager arRayCastManager = null;

    private bool Initialized { get; set; }

    private bool AllowCloudAnchorDelay { get; set; }

    private float timePassedAfterPlanesDetected = 0;

    [SerializeField]
    private float maxScanningAreaTime = 5;

    void Awake()
    { 
        arPlaneManager = GetComponent<ARPlaneManager>();
        arPointCloudManager = GetComponent<ARPointCloudManager>();
        arRayCastManager = GetComponent<ARRaycastManager>();

        arPlaneManager.planesChanged += PlanesChanged;

//#if UNITY_EDITOR       
//        OnInitialized?.Invoke();
//        Initialized = true;
//        AllowCloudAnchorDelay = false;
//        arPlaneManager.enabled = false;
//        arPointCloudManager.enabled = false;
//#endif
    }

    public void StartExp()
    {
        arRayCastManager.enabled = true;
        arPlaneManager.planesChanged += PlanesChanged;

        Restart();
    }

    void Update()
    {
        if (AllowCloudAnchorDelay)
        {
            if (timePassedAfterPlanesDetected <= maxScanningAreaTime)
            {
                timePassedAfterPlanesDetected += Time.deltaTime * 1.0f;

                ScanText.text = $"Experience starts in {maxScanningAreaTime - timePassedAfterPlanesDetected} sec(s)";
                print($"Experience starts in {maxScanningAreaTime - timePassedAfterPlanesDetected} sec(s)");
            }
            else
            {
                timePassedAfterPlanesDetected = maxScanningAreaTime;
                Activate();
            }
        }
    }

    void PlanesChanged(ARPlanesChangedEventArgs args)
    {
        if (!Initialized)
        {
            AllowCloudAnchorDelay = true;
        }
    }

    private void Activate()
    {
        print("Activate AR Cloud Anchor Experience");
        OnInitialized?.Invoke();
        Initialized = true;
        AllowCloudAnchorDelay = false;
        arPlaneManager.enabled = false;
        arPointCloudManager.enabled = false;
    }

    public void Restart()
    {
        print("Restart AR Cloud Anchor Experience");
        OnRestarted?.Invoke();
        Initialized = false;
        AllowCloudAnchorDelay = true;
        arPlaneManager.enabled = true;
        arPointCloudManager.enabled = true;
        timePassedAfterPlanesDetected = 0;
    }

  
}
