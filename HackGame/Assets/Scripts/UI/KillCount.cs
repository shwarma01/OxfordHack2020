using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KillCount : MonoBehaviour
{
    private TextMeshProUGUI tmpro;

    private void Start()
    {
        tmpro = GetComponent<TextMeshProUGUI>();
        tmpro.text = 0.ToString();
    }

    public void SetKills(int kills)
    {
        tmpro.text = kills.ToString();
    }
}
