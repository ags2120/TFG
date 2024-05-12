using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class infoPanel_esconder : MonoBehaviour
{
    public Button info;
    public GameObject infoPanel;
    public Texture2D imageInfo, imageCerrar;
    public RawImage img;
    void Start()
    {
        info.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    private void OnClick()
    {
        if (infoPanel.activeSelf == false)
        {
            infoPanel.SetActive(true);
            img.texture = imageCerrar;
        }
        else
        {
            infoPanel.SetActive(false);
            img.texture = imageInfo;
        }
            
    }
}
