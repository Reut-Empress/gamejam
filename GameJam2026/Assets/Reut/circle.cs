using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class circle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float Distance()
    { 
        Pen pen = Pen.current;

        Vector2 screenPosition = pen.position.ReadValue();

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y,
            Mathf.Abs(Camera.main.transform.position.z)));

        float d = Vector3.Distance(transform.position, worldPosition);

        return d;
    }
}
