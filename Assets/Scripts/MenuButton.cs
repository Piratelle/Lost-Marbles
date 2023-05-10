using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

/**
 * A class handling mouse-over effects for menu buttons.
 * 
 * @author Erin Ratelle
 * @author Ryan Smith
 */
public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private static Color BASE_COLOR = new Color32(206, 169, 85, 255);
    private static Color HOV_COLOR = new Color32(139, 133, 180, 255);

    public AudioSource audioToggle;

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.GetComponentInChildren<TMP_Text>().color = HOV_COLOR;
        audioToggle.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.GetComponentInChildren<TMP_Text>().color = BASE_COLOR;
    }
}
