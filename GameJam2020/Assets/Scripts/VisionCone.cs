using UnityEngine;

public class VisionCone : MonoBehaviour
{
    private float suspicionLevel;
    private Renderer coneRenderer;
    private float lowAlertThreshold = 0;
    private float mediumAlertThreshold = 30;
    private float highAlertThreshold = 60;
    public bool coneEnabled = true;
    
    public float opacity = 0.75f;
    
    // Start is called before the first frame update
    void Start()
    {
        coneRenderer = GetComponent<Renderer>();
        suspicionLevel = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (coneEnabled)
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
        else
        {
            var color = coneRenderer.material.color;
            color.a = 0f;
            coneRenderer.material.color = color;
        }
    }

    public void SetThresholds(float low, float med, float high)
    {
        lowAlertThreshold = low;
        mediumAlertThreshold = med;
        highAlertThreshold = high;
    }

    public void SetSuspicionLevel(float level) {
        suspicionLevel = level;
    }

    public void SetEnabled()
    {
        coneEnabled = !coneEnabled;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && coneEnabled)
        {
            transform.parent.GetComponent<NPCController>().PlayerEnteredVision(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (coneEnabled)
            {
                transform.parent.GetComponent<NPCController>().PlayerInVision(other.gameObject);
            }
            else
            {
                transform.parent.GetComponent<NPCController>().PlayerLeavedVision(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.parent.GetComponent<NPCController>().PlayerLeavedVision(other.gameObject);
        }
    }
}
