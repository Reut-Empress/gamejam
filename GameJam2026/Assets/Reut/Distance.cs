using UnityEngine;
using UnityEngine.InputSystem;

public class Distance : MonoBehaviour
{
    [SerializeField] private GameObject c;
    circle cScript;

    [SerializeField] private SpriteRenderer[] circles;       // drag your circle sprites here
    [SerializeField] private bool findByTag = false;         // alternative: auto-find objects with the tag below
    [SerializeField] private string circleTag = "Circle";
    [SerializeField] private float falloffPixels = 300f;     // pixels beyond the circle's edge where accuracy hits 0%
    [SerializeField] private bool edgeCounts = true;         // true: touching the circle = 100%, false: only the exact center = 100%
    [SerializeField] private int fontSize = 36;

    public float Accuracy { get; private set; }              // 0 to 100, readable from other scripts

    private Camera cam;
    private GUIStyle style;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        if (cam == null) cam = Camera.main;

        if (findByTag)
        {
            GameObject[] found = GameObject.FindGameObjectsWithTag(circleTag);
            circles = new SpriteRenderer[found.Length];
            for (int i = 0; i < found.Length; i++)
                circles[i] = found[i].GetComponent<SpriteRenderer>();
        }

        cScript = c.GetComponent<circle>();
    }

    private void Update()
    {
        Pointer pointer = Pen.current != null ? Pen.current : Pointer.current;
        if (pointer == null || circles == null || circles.Length == 0) return;

        Vector2 penPos = pointer.position.ReadValue();
        float closest = float.MaxValue;

        foreach (SpriteRenderer sr in circles)
        {
            if (sr == null) continue;

            Vector3 center = sr.bounds.center;
            Vector2 centerScreen = cam.WorldToScreenPoint(center);

            float dist = Vector2.Distance(penPos, centerScreen);

            if (edgeCounts)
            {
                // convert the circle's world radius to screen pixels
                Vector2 edgeScreen = cam.WorldToScreenPoint(center + Vector3.right * sr.bounds.extents.x);
                float radiusPx = Vector2.Distance(centerScreen, edgeScreen);
                dist = Mathf.Max(0f, dist - radiusPx);
            }

            closest = Mathf.Min(closest, dist);
        }

        Accuracy = Mathf.Clamp01(1f - closest / falloffPixels) * 100f;
    }

    private void OnGUI()
    {
        if (style == null)
        {
            style = new GUIStyle(GUI.skin.label);
            style.fontSize = fontSize;
            style.fontStyle = FontStyle.Bold;
            style.normal.textColor = Color.white;
        }

        //GUI.Label(new Rect(20, Screen.height - 70, 500, 60),
        //          "Accuracy: " + Mathf.RoundToInt(Accuracy) + "%", style);
        GUI.Label(new Rect(20, Screen.height - 70, 500, 60),
                  "Distance: " + (int)cScript.Distance(), style);
    }
}