using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VisionCone : MonoBehaviour
{
    private float suspicionLevel;
    private Renderer coneRenderer;

    public float opacity = 0.75f;
    
    // Start is called before the first frame update
    void Start()
    {
        coneRenderer =  GetComponent<Renderer>();
        suspicionLevel = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (suspicionLevel < 30)
        {
            coneRenderer.material.color = Color.cyan;
        }
        else if (suspicionLevel >= 30 && suspicionLevel < 60)
        {
            coneRenderer.material.color = Color.yellow;
        }
        else
        {
            coneRenderer.material.color = Color.red;
        }
    
        var color = coneRenderer.material.color;
        color.a = opacity;
        coneRenderer.material.color = color;
    }

    public void SetSuspicionLevel(float level) {
        suspicionLevel = level;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.parent.GetComponent<NPCController>().PlayerEnteredVision(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.parent.GetComponent<NPCController>().PlayerInVision(other.gameObject);
        }
    }
}
