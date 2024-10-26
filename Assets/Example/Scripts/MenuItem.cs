using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum Direction
{
    Left,
    Right
}

public class MenuItem : MonoBehaviour
{
    private TextMeshProUGUI[] text;   
    private Image[] images; 
    private GameObject indicator;

    public bool useArrowToInteract;
    public bool useButtonDown;
    public UnityEvent<Direction> OnPressMenuItemEventHandler;  

     
    private void Init()
    {
        if (indicator == null)
        {
            indicator = transform.GetChild(0).gameObject;
            text = GetComponentsInChildren<TextMeshProUGUI>();
            images = GetComponentsInChildren<Image>(); 
        } 
    }

    public void Deselect()
    {
        indicator.SetActive(false);

        foreach (Image image in images)
        {
            image.color = MenuManager.Instance.normalColor;
        }

        foreach (var item in text)
        {
            item.color = MenuManager.Instance.normalColor; 
        }
    }

    public void Select()
    {
        Init();

        indicator.SetActive(true);

        foreach (var item in text)
        {
            item.color = MenuManager.Instance.selectedColor;
        }

        foreach (Image image in images)
        {
            image.color = MenuManager.Instance.selectedColor;
        }

    }
}
