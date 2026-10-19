using UnityEngine;

public class Dot : MonoBehaviour
{
    private LineManager lineManager;
    private bool isActive, isEnd;
    GameObject deactivatedChild, startChild, endChild;


    private void Awake()
    {
        lineManager = FindFirstObjectByType<LineManager>();

        deactivatedChild = transform.Find("Deactivated").gameObject;
        startChild = transform.Find("Start").gameObject;
        endChild = transform.Find("End").gameObject;

        deactivatedChild.gameObject.SetActive(false);
        startChild.gameObject.SetActive(false);
        endChild.gameObject.SetActive(false);
    }

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
        isEnd = false;
        ChangeActiveChild(deactivatedChild);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive)
        {
            if (collision.CompareTag("Pen Dot") && isEnd)
            {
                lineManager.UpdateDotsAndLines();
            }
        }
    }

    public void SetAsEnd()
    {
        isEnd = true;
        ChangeActiveChild(endChild);
    }

    public void SetAsStart()
    {
        isEnd = false;
        ChangeActiveChild(startChild);
    }

    private void ChangeActiveChild(GameObject activeChild)
    {
        deactivatedChild.gameObject.SetActive(false);
        startChild.gameObject.SetActive(false);
        endChild.gameObject.SetActive(false);

        activeChild.gameObject.SetActive(true);
    }
}
