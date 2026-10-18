using UnityEngine;
using UnityEngine.InputSystem;

public class PenRangeDetector : MonoBehaviour
{
    Camera cam;
    GUIStyle style;
    float pressurePercent;

    void Start()
    {
        cam = GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = Color.red;
    }

    void Update()
    {
        Pen pen = Pen.current;

        bool inRange = pen != null && pen.inRange.ReadValue() > 0.5f;
        float pressure = pen != null ? pen.pressure.ReadValue() : 0f;
        bool touching = pressure > 0f || (pen != null && pen.tip.isPressed);

        pressurePercent = Mathf.Clamp01(pressure) * 100f;

        if (touching)
            cam.backgroundColor = Color.blue;
        else if (inRange)
            cam.backgroundColor = Color.green;
        else
            cam.backgroundColor = Color.red;

    }

    void OnGUI()
    {
        if (style == null)
        {
            style = new GUIStyle(GUI.skin.label);
            style.fontSize = 28;
            style.fontStyle = FontStyle.Bold;
            style.alignment = TextAnchor.UpperLeft;
            style.normal.textColor = Color.white;
        }

        GUI.Label(new Rect(20, 20, 400, 50),
                  "Pressure: " + Mathf.RoundToInt(pressurePercent) + "%", style);
    }
}
