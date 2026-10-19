using UnityEngine;
using UnityEngine.InputSystem;

public class LineManager : MonoBehaviour
{
    private GameObject[] dots, lines;
    private GameObject currentStartDot, currentEndDot, currentLine;

    private AVG avg;
    private PrintInfo printInfo;
    private int d, l;

    private Dot startCode, endCode;
    private void Awake()
    {
        dots = GameObject.FindGameObjectsWithTag("Dot");
        lines = GameObject.FindGameObjectsWithTag("Line");
        avg = FindAnyObjectByType<AVG>();
        printInfo = FindAnyObjectByType<PrintInfo>();

        d = 0;
        l = 0;

        SortDotsAndLines();
    }
    private void Start()
    {
        DeactivateAllDotsAndLines();
        UpdateDotsAndLines();
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
            if (dots[i].name.Length == 3)
            {
                tempDots[0] = dots[i];
            }
            else
            {
                num = int.Parse(dots[i].name.Substring(5, dots[i].name.Length - 5 - 1));
                tempDots[num] = dots[i];
            }
        }

        // For name: "Arrow (x)"
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].name.Length == 5)
            {
                tempLines[0] = lines[i];
            }
            else
            {
                num = int.Parse(lines[i].name.Substring(7, lines[i].name.Length - 7 - 1));
                tempLines[num] = lines[i];
            }
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
    private void DeactivateAllDotsAndLines()
    {
        for (int i = 0; i < dots.Length;i++)
        {
            dots[i].GetComponent<Dot>().Deactivate();
        }
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i].SetActive(false);
        }
    }
    public void UpdateDotsAndLines()
    {
        if (d >= dots.Length || l >= lines.Length)
        {
            currentStartDot.GetComponent<Dot>().Deactivate();
            currentEndDot.GetComponent<Dot>().Deactivate();
            currentLine.SetActive(false);
            return;
        }

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
        startCode = currentStartDot.GetComponent<Dot>();
        startCode.Activate();
        startCode.SetAsStart();

        d++;

        if (d < dots.Length) currentEndDot = dots[d];
        else currentEndDot = dots[0];

        endCode = currentEndDot.GetComponent<Dot>();
        endCode.Activate();
        endCode.SetAsEnd();

        // Sets the current line, and moving up the index.
        currentLine = lines[l];
        //Give current line to AVG and PrintInfo
        if (avg != null) avg.SetLine(currentLine);
        if (printInfo != null) printInfo.SetLine(currentLine.GetComponent<Arrow>());
        l++;
        // Updates after.
        currentLine.SetActive(true);
    }

    public void SetNextDotAsNext()
    {
        endCode.SetIsNextInLine(true);
    }
}
