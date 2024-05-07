using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextTMPViewer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textPlayerHP;
    [SerializeField]
    private PlayerHp playerHP;
    private void Update()
    {
        textPlayerHP.text = playerHP.CurrentHp + "/" + playerHP.MaxHP;
    }
}