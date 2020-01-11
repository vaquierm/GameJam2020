using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvents : MonoBehaviour
{
    public VisionCone cone;
    
    public void ToggleCone()
    {    
        cone.SetEnabled();
    }
    
}
