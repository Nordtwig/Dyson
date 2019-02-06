using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by: Svedlund
/// </summary>

public class PortableShieldScale : MonoBehaviour
{
    private float colliderRadius;
    private float startSizeVFX;

    private StoreWindow storeWindow;

    private void Start()
    {
        SetShieldRadius();
    }

    public void SetShieldRadius()
    {
        storeWindow = FindObjectOfType<StoreWindow>();
        colliderRadius = GetComponent<CapsuleCollider>().radius;
        startSizeVFX = transform.parent.GetComponentInChildren<ParticleSystem>().startSize;

        Debug.Log("Shield scale variables assigned");

        if (storeWindow.currentShieldRadiusLevel > 0)
        {
            if (storeWindow.currentShieldRadiusLevel == 1)
            {
                colliderRadius = 0.7f;
                startSizeVFX = 25.5f;
            }
            else if (storeWindow.currentShieldRadiusLevel == 2)
            {
                colliderRadius = 0.8f;
                startSizeVFX = 28.5f;
            }
            else if (storeWindow.currentShieldRadiusLevel == 3)
            {
                colliderRadius = 0.9f;
                startSizeVFX = 31f;
            }

            GetComponent<CapsuleCollider>().radius = colliderRadius;
            transform.parent.GetComponentInChildren<ParticleSystem>().startSize = startSizeVFX;
            transform.parent.GetComponentInChildren<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            transform.parent.GetComponentInChildren<ParticleSystem>().Play();

            Debug.Log("Setting shield scale");
        }
    }
}
