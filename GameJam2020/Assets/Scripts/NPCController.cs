using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AlertLevel
{
    LowAlert,
    MediumAlert,
    HighAlert
}

public class NPCController : MonoBehaviour
{
    public ExclamationMark exclamationMark;
    public float suspicionLevel = 0f;
    public VisionCone cone;

    public float lookDownChance = 0f;
    public float lookUpChance = 0f;

    public float ScanAngleMin = 0f;
    public float ScanAngleMax = 0f;
    public float MaxRotationSpeed = 0f;
    public float SuspicionIncreasePerTick = 0f;
    public bool IsTriggered = false;
    
    private Rigidbody rb;
    private AlertLevel alertLevel;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        alertLevel = AlertLevel.LowAlert;
    }

    // Update is called once per frame
    void Update()
    {
        if (suspicionLevel > 70 && !IsTriggered)
        {
            IsTriggered = true;
            exclamationMark.Enable();
        }   
        
        // update vision cone
        cone.SetSuspicionLevel(suspicionLevel);
        // make suspicion level go down
        if (suspicionLevel > 0f)
            suspicionLevel -= 0.001f;
    }

    void LowAlertUpdate()
    {
        
    }

    void MediumAlertUpdate()
    {
        
    }

    void HighAlertUpdate()
    {
    
    }

    public void PlayerEnteredVision(GameObject player)
    {
        Debug.Log("I C U");
    }

    public void PlayerInVision(GameObject player)
    {
        suspicionLevel += SuspicionIncreasePerTick;
        switch (alertLevel)
        {
            case AlertLevel.LowAlert: break;
            case AlertLevel.MediumAlert: break;
            case AlertLevel.HighAlert: break;
        }
    }
}
