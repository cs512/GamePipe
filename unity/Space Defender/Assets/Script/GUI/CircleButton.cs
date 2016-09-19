using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class CircleButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public Image circle;
    public Image icon;
    public string title;
    public CircleMenu myMenu;

    Color defaultColor;
    
    public void OnPointerEnter (PointerEventData eventData)
    {
        myMenu.selected = this;
        defaultColor = circle.color;
        circle.color = Color.blue;
    }
    
    public void OnPointerExit (PointerEventData eventData)
    {
        myMenu.selected = null;
        circle.color = defaultColor;
    }




}
