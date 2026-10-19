using UnityEngine;

public class Dot : MonoBehaviour
{
    private LineManager lineManager;
    private PenLogic penLogic;
    private bool isActive, isEnd, isNextInLine, wasInLine;
    GameObject redChild, goldChild, greenChild, activeChild;
    [SerializeField] private float minimumPressure, maximumPressure;


    private void Awake()
    {
        lineManager = FindFirstObjectByType<LineManager>();
        penLogic = FindFirstObjectByType<PenLogic>();

        redChild = transform.Find("Red").gameObject;
        goldChild = transform.Find("Gold").gameObject;
        greenChild = transform.Find("Green").gameObject;

        redChild.gameObject.SetActive(false);
        goldChild.gameObject.SetActive(false);
        greenChild.gameObject.SetActive(false);

        isNextInLine = false;
        wasInLine = false;
        activeChild = null;
    }

    public void Activate()
    {
        isActive = true;
        if (!wasInLine)
        {
            ChangeActiveChild(goldChild);
        }
    }

    public void Deactivate()
    {
        isActive = false;
        isEnd = false;

        if (!wasInLine)
        {
            ChangeActiveChild(redChild);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive)
        {
            if (collision.CompareTag("Pen Dot") && PressureInRange())
            {
                if (isEnd && isNextInLine)
                {
                    lineManager.UpdateDotsAndLines();
                }
                if (isNextInLine)
                {
                    MoveUpTheLine();
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isActive)
        {
            if (collision.CompareTag("Pen Dot") && PressureInRange())
            {
                if (isEnd && isNextInLine)
                {
                    lineManager.UpdateDotsAndLines();
                }
                if (isNextInLine)
                {
                    MoveUpTheLine();
                }
            }
        }
    }

    public void SetAsEnd()
    {
        isEnd = true;
    }

    public void SetAsStart()
    {
        isEnd = false;

        if (!wasInLine)
        {
            SetIsNextInLine(true);
        }
        else
        {
            MoveUpTheLine();
        }
    }

    private void ChangeActiveChild(GameObject child)
    {
        redChild.gameObject.SetActive(false);
        goldChild.gameObject.SetActive(false);
        greenChild.gameObject.SetActive(false);

        activeChild = child;
        activeChild.gameObject.SetActive(true);
    }

    public void SetIsNextInLine(bool value)
    {
        isNextInLine = value;
    }

    private void MoveUpTheLine()
    {
        SetIsNextInLine(false);
        wasInLine = true;
        ChangeActiveChild(greenChild);
        lineManager.SetNextDotAsNext();
    }

    private bool PressureInRange()
    {
        if (penLogic.Pressure() >= minimumPressure && penLogic.Pressure() <= maximumPressure)
        {
            return true;
        }
        return false;
    }
}
