using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTrigger : MonoBehaviour
{
    public Vector3 punchStrength;
    private Button thisButton;

    private void Start()
    {
        thisButton = GetComponent<Button>();
    }

    public void OnMouseEnter()
    {
        Debug.Log("Punch scale!");
        iTween.PunchScale(thisButton.gameObject, Random.insideUnitCircle * 1.5f, 02f);
    }
}
