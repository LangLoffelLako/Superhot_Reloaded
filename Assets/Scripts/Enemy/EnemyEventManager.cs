using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyEventManager : MonoBehaviour
{
    public UnityEvent OnDie;
    public UnityEvent OnFire;

    void OnEnable()
    {
        if (OnDie == null)
        {
            OnDie = new UnityEvent();
            OnDie.AddListener(Die);
        }

        if (OnFire == null)
        {
            OnFire = new UnityEvent();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

}
