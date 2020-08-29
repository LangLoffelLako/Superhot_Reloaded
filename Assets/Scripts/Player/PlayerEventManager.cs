using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEventManager : MonoBehaviour
{
   public static UnityEvent OnDie;
   public static UnityEvent OnFire;

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

   private void FixedUpdate()
   {
      //Fire gun, if attached
      if (Input.GetButton("Fire1"))
      {
         OnFire.Invoke();
      }
   }

   public void Die()
   {
      Debug.Log("You Died");
   }
}
