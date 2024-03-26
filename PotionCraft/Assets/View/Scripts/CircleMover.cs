using UnityEngine;

public class CircleMover : MonoBehaviour
{
    public void MoveTo(Vector3 newPosition)
    {
        transform.position = newPosition;
    }
}