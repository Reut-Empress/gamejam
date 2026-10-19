using System;
using UnityEngine;
using TMPro;

public class PrintInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text pressureText;
    [SerializeField] private TMP_Text distanceText;
    [SerializeField] private TMP_Text averageDistanceText;

    private AVG avgScript;

    private Arrow currentLineScript;


    private PenLogic penlogic;

    private void Awake()
    {
        penlogic = FindAnyObjectByType<PenLogic>();
        avgScript = FindAnyObjectByType<AVG>();
    }

    private void Update()
    {
        pressureText.text = $"Pressure: {(int)(penlogic.Pressure() * 100)}%";
        if(currentLineScript != null)
        distanceText.text = $"Distance: {Math.Round(currentLineScript.DistanceFromArrow(), 2)}";
        averageDistanceText.text = $"Average Distance:  {Math.Round(avgScript.CalculateAverage(), 2)}";
    }

    public void SetLine(Arrow a)
    {
        currentLineScript = a;
    }
}
