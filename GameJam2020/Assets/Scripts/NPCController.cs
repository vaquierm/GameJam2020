using System;
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
    public GameManager GameManager;

    public ExclamationMark exclamationMark;
    public float suspicionLevel = 0f;
    public VisionCone cone;
    public Animator anim;

    public float LowAlertThreshold = 0f;
    public float MediumAlertThreshold = 30f;
    public float HighAlertThreshold = 60f;
    public float TriggeredThreshold = 80f;
    public float LookChance = 0f;
    public float LookAnimCooldown = 0f;
    public float ScanAngle = 0f;
    //public float ScanAngleMin = 0f;
    //public float ScanAngleMax = 0f;
    //public float MaxRotationSpeed = 0f;
    //public float MinRotationSpeed = 0f;
    public float RotationSpeed = 0f;
    public float SuspicionIncreasePerTick = 0f;
    public float SuspicionDecreasePerTick = 0f;
    public bool IsTriggered = false;
    public bool PlayerInFOV = false;

    private float ExclamationFadeCount;

    private float RotationCount;
    
    
    private Rigidbody rb;
    private AlertLevel alertLevel;
    private Vector3 initialRotation;
    private bool rotating = false;
    private float rotationSpeed = 0f;
    
    private float lastLook;

    private System.Random rand;

    private int Direction;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialRotation = transform.rotation.eulerAngles;
        alertLevel = AlertLevel.LowAlert;
        cone.SetThresholds(LowAlertThreshold, MediumAlertThreshold, HighAlertThreshold);

        rand = new System.Random((int) (RotationSpeed * (SuspicionIncreasePerTick * 100) * ScanAngle * LookAnimCooldown) * (int) (System.DateTime.Now.Ticks % int.MaxValue));
        Direction = rand.Next(2);
        if (Direction == 0)
        {
            Direction = -1;
        }

        RotationCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (ExclamationFadeCount > 0)
        {
            ExclamationFadeCount -= Time.deltaTime;
        }
        if (ExclamationFadeCount <= 0f)
        {
            exclamationMark.Disable();
        }

        if (GameManager.IsGameOver())
        {
            return;
        }
        if (suspicionLevel > TriggeredThreshold && !IsTriggered)
        {
            IsTriggered = true;
            exclamationMark.Enable();
            ExclamationFadeCount = 0.9f;
            GameManager.SetNPCDetection(true);
        }

        
        
        UpdateAlertLevel();
        switch (alertLevel)
        {
            case AlertLevel.LowAlert: 
                LowAlertUpdate();
                break;
            case AlertLevel.MediumAlert:
                MediumAlertUpdate();
                break;
            case AlertLevel.HighAlert:
                HighAlertUpdate();
                break;
        }
        
        // update vision cone
        cone.SetSuspicionLevel(suspicionLevel);
        // make suspicion level go down
        if (PlayerInFOV)
        {
            suspicionLevel += SuspicionIncreasePerTick * Time.deltaTime;
        }
        else if (suspicionLevel > 0f)
        {
            suspicionLevel -= SuspicionDecreasePerTick * Time.deltaTime;
            if (suspicionLevel < 0f) suspicionLevel = 0f;
        }
    }

    void LowAlertUpdate()
    {
        if (Time.time - lastLook > LookAnimCooldown && (float)rand.NextDouble() < LookChance)
        {
            lastLook = Time.time;
            if ((float)rand.NextDouble() > 0.5f)
            {
                anim.SetTrigger("LookDown");
            }
            else
            {
                anim.SetTrigger("LookUp");
            }
        }
        else
        {
            if (cone.coneEnabled)
            {
                var angle = initialRotation.y + (Direction * Mathf.PingPong(RotationCount * RotationSpeed, 2 * ScanAngle)) - ScanAngle;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
                RotationCount += Time.deltaTime;
            }
        }
    }

    void MediumAlertUpdate()
    {

    }

    void HighAlertUpdate()
    {

    }

    void UpdateAlertLevel()
    {
        if (suspicionLevel >= 0f && suspicionLevel <= MediumAlertThreshold)
        {
            alertLevel = AlertLevel.LowAlert;
        }
        else if (suspicionLevel > MediumAlertThreshold && suspicionLevel <= HighAlertThreshold)
        {
            alertLevel = AlertLevel.MediumAlert;
        }
        else if (suspicionLevel > HighAlertThreshold)
        {
            alertLevel = AlertLevel.HighAlert;
        }
        else
        {
            Debug.Log("You're fucked dawg");
        }
    }

    public void PlayerEnteredVision(GameObject player)
    {
        Debug.Log("Player entered vision");
        PlayerInFOV = true;
    }

    public void PlayerInVision(GameObject player)
    {
        switch (alertLevel)
        {
            case AlertLevel.LowAlert: break;
            case AlertLevel.MediumAlert: break;
            case AlertLevel.HighAlert: break;
        }
    }

    public void PlayerLeavedVision(GameObject player)
    {
        Debug.Log("Player Left vision");
        PlayerInFOV = false;
    }


}
