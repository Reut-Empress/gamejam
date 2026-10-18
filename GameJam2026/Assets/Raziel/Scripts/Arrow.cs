using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Arrow : MonoBehaviour
{
    [SerializeField] private GameObject startPoint;
    [SerializeField] private GameObject endPoint;

    [SerializeField] private GameObject penDot;

    private Vector3 startPointPosition;
    private Vector3 endPointPosition;

    private void Awake()
    {
        startPointPosition = startPoint.transform.position;
        endPointPosition = endPoint.transform.position;
    }

    private void Start()
    {
        Resize();
        DistaneFromArrow();
    }

    private void Resize()
    {
        // Position the object halfway between the points
        transform.position = (startPointPosition + endPointPosition) / 2f;

        // Calculate direction and distance
        Vector2 direction = endPointPosition - startPointPosition;
        float distance = direction.magnitude;

        // Rotate the object toward the end point
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // Stretch along the X-axis
        transform.localScale = new Vector3(
            distance,
            transform.localScale.y,
            transform.localScale.z
        );
    }

    public float DistaneFromArrow()
    {
        float distance = Vector3.Cross(penDot.transform.position - transform.position, transform.right).magnitude;
        Debug.Log(distance);
        return distance;
    }
}
