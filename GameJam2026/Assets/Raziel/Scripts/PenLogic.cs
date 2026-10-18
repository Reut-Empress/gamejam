using UnityEngine;
using UnityEngine.InputSystem;

public class PenLogic : MonoBehaviour
{
    [SerializeField] private GameObject penDot;

    private Pen pen;

    private bool inRange, touching;
    private float pressure;

    private void Update()
    {
        UpdatePen();
        TeleportPenDot();
    }

    private void UpdatePen()
    {
        pen = Pen.current;

        inRange = pen != null && pen.inRange.ReadValue() > 0.5f;
        pressure = pen != null ? pen.pressure.ReadValue() : 0f;
        touching = pressure > 0f || (pen != null && pen.tip.isPressed);
    }

    private void TeleportPenDot()
    {
        Debug.Log($"if {pen == null || !inRange}, inRange {inRange}, pressure {pressure}, touching {touching}");
        if (pen == null || !inRange) return;

        Vector2 screenPosition = pen.position.ReadValue();

        Debug.Log(screenPosition);

        penDot.transform.position = Camera.main.ScreenToWorldPoint(
            new Vector3(screenPosition.x, screenPosition.y,
            Mathf.Abs(Camera.main.transform.position.z))
        );
    }
}
