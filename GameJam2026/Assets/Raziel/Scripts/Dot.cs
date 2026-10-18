using UnityEngine;

public class Dot : MonoBehaviour
{
    private LineManager lineManager;
    private bool isActive, isEnd;
    //Color
    SpriteRenderer spriteRenderer;


    private void Awake()
    {
        //Color
        spriteRenderer = GetComponent<SpriteRenderer>();

        lineManager = FindFirstObjectByType<LineManager>();
        Deactivate();

    }

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
        isEnd = false;
        spriteRenderer.color = Color.red;
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
        spriteRenderer.color = Color.green;
    }

    public void SetAsStart()
    {
        isEnd = false;
        spriteRenderer.color = Color.gold;

    }
}
