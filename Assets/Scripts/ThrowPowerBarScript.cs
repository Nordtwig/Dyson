using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Made by Ulrik
/// </summary>

public class ThrowPowerBarScript : MonoBehaviour
{
	private Slider throwPowerBar;
    [SerializeField] GameObject throwBarBackground;

    public static ThrowPowerBarScript instance;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            instance.enabled = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        throwPowerBar = GetComponent<Slider>();
		throwPowerBar.value = 0;
	}

	public void ThrowPowerBarUpdate(float eTime, bool holdingItem)
	{
		if (holdingItem)
		{
            SetEnableThrowBackground(true);
            throwPowerBar.value = (eTime - 0.5f) / 0.5f; // powerBarMax
		}
	}

    public void SetEnableThrowBackground(bool set)
    {
        throwBarBackground.SetActive(set);
    }
}
