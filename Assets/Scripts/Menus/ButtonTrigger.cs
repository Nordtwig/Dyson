using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

/// <summary>
/// Created by: Svedlund
/// </summary>

public class ButtonTrigger : EventTrigger
{
    private Vector3 largeScale = new Vector3(1.1f, 1.1f, 1.1f);
    //private Vector3 smallScale = new Vector3(0.8f, 0.8f, 0.8f);
    private Vector3 originalScale = new Vector3(1.0f, 1.0f, 1.0f);
    private GameObject thisButton;
    private Button[] allButtons;
    public bool keepScale;

    private void Start()
    {
        thisButton = this.gameObject;
        allButtons = FindObjectsOfType<Button>();
        if (thisButton == GameObject.Find("NextButton") || thisButton == GameObject.Find("PreviousButton")) keepScale = true;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (thisButton.GetComponent<Button>().interactable)
        {
            if (thisButton.GetComponentInParent<VerticalLayoutGroup>() == null) thisButton.transform.SetAsLastSibling();
            iTween.ScaleTo(thisButton.gameObject, largeScale, 1f);
        }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (thisButton.GetComponent<Button>().interactable)
        {
            iTween.ScaleTo(thisButton.gameObject, originalScale, 1f);
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (thisButton.GetComponent<Button>().interactable)
        {
            if (!keepScale) iTween.ScaleTo(thisButton.gameObject, originalScale, 1f);
            AudioManager.instance.Play("Button Press");
        }
        if (keepScale) StartCoroutine(RescaleHowToPlayButtons());
    }

    private IEnumerator RescaleHowToPlayButtons()
    {
        yield return new WaitForSeconds(0.2f);
        if (thisButton.GetComponent<Button>().interactable == false)
        {
            iTween.ScaleTo(thisButton.gameObject, originalScale, 1f);
        }
        yield return null;
    }

    public void ResetScale()
    {
        iTween.ScaleTo(thisButton.gameObject, originalScale, 0.01f);
    }
}
