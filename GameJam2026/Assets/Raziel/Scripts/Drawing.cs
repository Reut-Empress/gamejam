using UnityEngine;

public class Drawing : MonoBehaviour
{
    [SerializeField] private GameObject sketch;
    private PenLogic penLogic;
    private LineRenderer lineRenderer;
    private Vector3 previousPosition;
    private float minimumDistance;

    private bool isDrawing;
    private bool wasActivated;
    private void Awake()
    {
        penLogic = FindAnyObjectByType<PenLogic>();

        lineRenderer = GetComponent<LineRenderer>();
        minimumDistance = 0.1f;

        wasActivated = false;
    }

    private void Update()
    {
        Draw();
    }

    private void Draw()
    {
        if (penLogic.IsTouching())
        {
            Vector3 currentPosition = penLogic.PenPosition();
            wasActivated = true;

            if (!isDrawing)
            {
                // Start a new line
                isDrawing = true;

                lineRenderer.positionCount = 1;
                lineRenderer.SetPosition(0, currentPosition);

                previousPosition = currentPosition;
            }
            else if (Vector3.Distance(currentPosition, previousPosition) > minimumDistance)
            {
                // Continue drawing
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(
                    lineRenderer.positionCount - 1,
                    currentPosition
                );

                previousPosition = currentPosition;
            }
        }
        else if (isDrawing && wasActivated)
        {
            // Stop drawing when contact is released
            isDrawing = false;
            Instantiate(sketch);
            enabled = false;
        }

        /*
        if (penLogic.IsTouching())
        {
            if (penLogic.IsTouching())
            {
                if (Vector3.Distance(penLogic.PenPosition(), previousPosition) > minimumDistance)
                {
                    lineRenderer.positionCount++;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, penLogic.PenPosition());
                    previousPosition = penLogic.PenPosition();
                }
            }
        }
        */
    }

    
}
