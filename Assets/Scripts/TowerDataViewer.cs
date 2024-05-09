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
        //타워 정보 판텔 on
        gameObject.SetActive(true);
    }
    public void OffPanel()
    {
        //타워 정보 판넬 off
        gameObject.SetActive(false);
    }
}
