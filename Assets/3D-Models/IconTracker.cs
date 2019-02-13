using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Created by: Noah, 31/1, 2019
/// </summary>

public class IconTracker : MonoBehaviour
{

    Image icon;
    Image offScreenIcon;
    //public List<GameObject> targets;
    GameObject target;

    void Start() {
        Vector3 screenCenter = new Vector3(Screen.width, Screen.height)*.5f;
        icon = gameObject.transform.GetChild(0).GetComponent<Image>();
        offScreenIcon = gameObject.transform.GetChild(1).GetComponent<Image>();
        target = GameObject.Find("SLS");
    }

    void Update() {
        PlaceTrackers();    
    }

    void PlaceTrackers() {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(target.transform.position);

        if (screenPos.z > 0 && screenPos.x < Screen.width &&  screenPos.x > 0 && screenPos.y < Screen.height && screenPos.y > 0) {
            if (screenPos.z > 2) {
                icon.enabled = true;
                offScreenIcon.enabled = false;
                icon.rectTransform.position = screenPos;
            }
            //icon.enabled = false;
            //offScreenIcon.enabled = false;
        }
        else {
            icon.enabled = false;
            offScreenIcon.enabled = true;
            PlaceOffScreen(screenPos);
        }
    }

    void PlaceOffScreen(Vector3 screenPos) {
        float x = screenPos.x;
        float y = screenPos.y;
        float offset = 10;

        if (screenPos.z < 0)
            screenPos = -screenPos;

        if (screenPos.x > Screen.width)
            x = Screen.width - offset;

        if (screenPos.x < 0)
            x = offset;

        if (screenPos.y > Screen.height)
            y = Screen.height - offset;

        if (screenPos.y < 0)
            y = offset;

        offScreenIcon.rectTransform.position = new Vector3(x, y, 0);
    }
}
