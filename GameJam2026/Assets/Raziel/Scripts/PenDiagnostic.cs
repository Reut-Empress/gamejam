using UnityEngine;
using UnityEngine.InputSystem;

public class PenDiagnostic : MonoBehaviour
{
    void Update()
    {
        if (Pen.current == null)
        {
            Debug.Log("NO PEN DETECTED");
            return;
        }

        Pen pen = Pen.current;

        Debug.Log(
            $"Pen detected: {pen.displayName} | " +
            $"In range: {pen.inRange.ReadValue()} | " +
            $"Position: {pen.position.ReadValue()} | " +
            $"Pressure: {pen.pressure.ReadValue()} | " +
            $"Tip pressed: {pen.tip.isPressed}"
        );
    }
}
