using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyEventManager : MonoBehaviour
{
    public UnityEvent onBeingShot;
    public UnityEvent onFire;

    void OnEnable()
    {
        if (onBeingShot == null)
        {
            onBeingShot = new UnityEvent();
        }
        
        if (onFire == null)
        {
            onFire = new UnityEvent();
        }
    }

    private void Start()
    {
        if (onBeingShot != null)
        {
            onBeingShot.AddListener(Die);
        }
    }

    private void Die()
    {
        UIManager.enemiesKilled += 1;
        Destroy(gameObject);
    }
}