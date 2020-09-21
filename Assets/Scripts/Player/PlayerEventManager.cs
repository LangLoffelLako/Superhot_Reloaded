using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEventManager : MonoBehaviour
{
   public static UnityEvent onShot;
   public static UnityEvent onFire;
   public static UnityEvent onDefeatedEnemies;

   public static bool isGameOver = false;
   private GameObject remainingEnemy;

   private void OnEnable()
   {
      if (onShot == null)
      {
         onShot = new UnityEvent();
      }
      
      if (onFire == null)
      {
         onFire = new UnityEvent();
      }

      if (onDefeatedEnemies == null)
      {
         onDefeatedEnemies = new UnityEvent();
      }
   }

   private void Start()
   {
      onShot.AddListener(GameOver);
      onDefeatedEnemies.AddListener(GameOver);
   }

   private void Update()
   {
      remainingEnemy = GameObject.FindWithTag("Enemy");
      if (remainingEnemy == null)
      {
         Debug.Log("You won!");
         onDefeatedEnemies.Invoke();
      }
   }

   private void FixedUpdate()
   {
      //Fire gun, if attached
      if (Input.GetButton("Fire1"))
      {
         if (onFire != null)
            onFire.Invoke();
      }
   }

   private void GameOver()
   {
      if (!isGameOver)
      {
         isGameOver = true;
      }
   }

   private void Continue()
   {
      isGameOver = false;
   }
}