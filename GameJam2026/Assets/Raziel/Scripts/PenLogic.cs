using UnityEngine;
using UnityEngine.InputSystem;

public class PenLogic : MonoBehaviour
{
    [SerializeField] private GameObject penDot;
    private Rigidbody2D penDotRigidbody2D;
    private Vector3 targetPosition;

    private Camera cam;

    private Pen pen;
    private Vector2 screenPosition;
    private Vector3 worldPosition;

    private bool inRange;
    private bool touching;
    private float pressure;
    private void Awake()
    {
        penDotRigidbody2D = penDot.GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    private void Update()
    {
        UpdatePen();
    }

    private void FixedUpdate()
    {
        TeleportPenDot();
    }

    private void UpdatePen()
    {
        pen = Pen.current;

        inRange = pen != null && pen.inRange.ReadValue() > 0.5f;
        pressure = pen != null ? pen.pressure.ReadValue() : 0f;
        touching = pressure > 0f || (pen != null && pen.tip.isPressed);

        screenPosition = pen.position.ReadValue();
        worldPosition = cam.ScreenToWorldPoint
            (
            new Vector3(
            screenPosition.x,
            screenPosition.y,
            Mathf.Abs(cam.transform.position.z)
            )
        );

        targetPosition = worldPosition;
    }

    private void TeleportPenDot()
    {
        if (pen == null || !inRange) return;

        penDotRigidbody2D.MovePosition(targetPosition);
    }

    public bool IsTouching()
    {
        return touching;
    }

    public Vector3 PenPosition()
    {
        return targetPosition;
    }
}
