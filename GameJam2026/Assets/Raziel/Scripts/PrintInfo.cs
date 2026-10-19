using System;
using UnityEngine;
using TMPro;

public class PrintInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text pressureText;
    private PenLogic penlogic;

    private void Awake()
    {
        penlogic = FindAnyObjectByType<PenLogic>();
    }

    private void Update()
    {
        pressureText.text = $"Pressure: {(int)(penlogic.Pressure() * 100)}%";
    }
}
