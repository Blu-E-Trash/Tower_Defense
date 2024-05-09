using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDataViewer : MonoBehaviour
{
    private void Awake()
    {
        OffPanel();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            OffPanel();
        }
    }
    public void OnPanel()
    {
        //Ÿ�� ���� ���� on
        gameObject.SetActive(true);
    }
    public void OffPanel()
    {
        //Ÿ�� ���� �ǳ� off
        gameObject.SetActive(false);
    }
}
