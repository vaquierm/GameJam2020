using UnityEngine;

public class BouncerController : MonoBehaviour
{
    public GameManager GameManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.SetWinColliderHit(true);
        }
    }
}
