using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PanelHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Color normalColor;
    public Color hoverColor;
    private Image panelImage;

    private void Start()
    {
        panelImage = GetComponent<Image>();
        normalColor = panelImage.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        panelImage.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        panelImage.color = normalColor;
    }
}
