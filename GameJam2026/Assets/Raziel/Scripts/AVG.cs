using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class AVG : MonoBehaviour
{
    GameObject line;
    float sum;
    int num;
    Arrow lineScript;
    void Update()
    {
        num++;
        sum += lineScript.DistanceFromArrow();
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
