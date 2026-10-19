using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class AVG : MonoBehaviour
{
    GameObject line;
    float sum;
    int num;
   //float m = 0;

    Arrow lineScript;
    PenLogic penLogic;
    private void Awake()
    {
        penLogic = FindAnyObjectByType<PenLogic>();
    }
    void Update()
    {
        

        if (penLogic.IsTouching() == true)
        {
            sum += lineScript.DistanceFromArrow();
            num++;

            Debug.Log(CalculateAverage());
        }
        

        
        //m = Mathf.Max(num,m);
    }
    public void SetLine(GameObject l)
    {
        line = l;
        lineScript = line.GetComponent<Arrow>();
    }
    public float CalculateAverage()
    {
        return sum / num; 
    }
}