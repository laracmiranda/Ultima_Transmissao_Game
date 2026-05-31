using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    private Vector3 originalPosition;

    private void Awake()
    {
        Instance = this;
    
        originalPosition = transform.position;
    }

    public void Shake(float intensity)
    {
        transform.position =
            originalPosition +
            (Vector3)Random.insideUnitCircle * intensity;
    }

    public void ResetPosition()
    {
        transform.position = originalPosition;
    }
}