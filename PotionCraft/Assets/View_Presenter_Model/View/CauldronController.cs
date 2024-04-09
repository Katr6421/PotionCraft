using UnityEngine;

public class CauldronController : MonoBehaviour
{
    // Make sure to attach this script to CauldronBase

    public float tiltAngle = 8.0f; // Assign in the inspector
    public float tiltSpeed = 1.5f; // Assign in the inspector
    private Quaternion targetRotation;

    // Assuming CauldronBase is at world origin; adjust if it's not.
    private Vector3 rotationPivot = Vector3.zero;

    private void Start()
    {
        targetRotation = Quaternion.identity;
    }

    public void TiltLeft()
    {
        targetRotation = Quaternion.Euler(0, 0, tiltAngle);
    }

    public void TiltRight()
    {
        targetRotation = Quaternion.Euler(0, 0, -tiltAngle);
    }

    public void StandUpright()
    {
        targetRotation = Quaternion.identity;
    }

    private void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * tiltSpeed);
    }
}
