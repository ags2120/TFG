using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class infoPanel_esconder : MonoBehaviour
{
    public Button info;
    public GameObject infoPanel;
    void Start()
    {
        info.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    private void OnClick()
    {
        if (infoPanel.activeSelf == false)
        
            infoPanel.SetActive(true);
        else
            infoPanel.SetActive(false);
    }
}
