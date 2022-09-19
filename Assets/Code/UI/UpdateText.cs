using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateText : MonoBehaviour
{
    [SerializeField]
    private GameDataManager Data;

    [SerializeField]
    private TMP_Text t_Record;



    public void UpdateTextUI()
    {
        t_Record.text = Data.Record.ToString();
    }

    private void Start()
    {
        UpdateTextUI();
    }
}
