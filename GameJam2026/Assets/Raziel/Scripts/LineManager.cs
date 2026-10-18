using UnityEngine;
using UnityEngine.InputSystem;

public class LineManager : MonoBehaviour
{
    private GameObject[] dots, lines;
    private GameObject currentStartDot, currentEndDot, currentLine;

    private int d, l;
    private void Awake()
    {
        dots = GameObject.FindGameObjectsWithTag("Dot");
        lines = GameObject.FindGameObjectsWithTag("Line");

        d = 0;
        l = 0;

        SortDotsAndLines();
        DeactivateAllLines();
        UpdateDotsAndLines();
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            UpdateDotsAndLines();
        }
    }

    // Names must be with parentheses, eg "Dot (0)" and not "Dot".
    private void SortDotsAndLines()
    {
        GameObject[] tempDots = new GameObject[dots.Length];
        GameObject[] tempLines = new GameObject[lines.Length];

        int num;

        // For name: "Dot (x)"
        for (int i = 0; i < dots.Length; i++)
        {
            num = int.Parse(dots[i].name.Substring(5, 1));
            tempDots[num - 1] = dots[i];
        }

        // For name: "Arrow (x)"
        for (int i = 0; i < lines.Length; i++)
        {
            num = int.Parse(lines[i].name.Substring(7, 1));
            tempLines[num - 1] = lines[i];
        }

        for (int i = 0; i < dots.Length; i++)
        {
            dots[i] = tempDots[i];
        }

        for (int i = 0; i < lines.Length; i++)
        {
            lines[i] = tempLines[i];
        }
    }
    // Deactivates dots too eventually.
    private void DeactivateAllLines()
    {
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i].SetActive(false);
        }
    }
    // In the future, change the visuals of the dots and lines through here.
    public void UpdateDotsAndLines()
    {
        if (d >= dots.Length || l >= lines.Length) return;

        // Updates before moving up.
        if (currentLine != null)
        {
            currentStartDot.GetComponent<Dot>().Deactivate();
            currentEndDot.GetComponent<Dot>().Deactivate();
            currentLine.SetActive(false);
        }

        // Sets the new current starting dot, moving to the next index, and updating the current end dot with the new index.
        // The index is not updated again, insuring that the new starting dot is the current end dot.
        currentStartDot = dots[d];
        Dot startCode = currentStartDot.GetComponent<Dot>();
        startCode.Activate();
        startCode.SetAsStart();

        d++;

        if (d < dots.Length) currentEndDot = dots[d];
        else currentEndDot = dots[0];

        Dot endCode = currentEndDot.GetComponent<Dot>();
        endCode.Activate();
        endCode.SetAsEnd();

        // Sets the current line, and moving up the index.
        currentLine = lines[l];
        l++;

        // Updates after.
        currentLine.SetActive(true);
    }

}
