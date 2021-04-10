using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEventManager : MonoBehaviour
{
   public static UnityEvent onShot;
   public static UnityEvent onFire;
   public static UnityEvent onDefeatedEnemies;
   public int roomNumber;
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
      if (roomNumber == 1)
      {
         StartCoroutine(WaitThenClose());
      }
      else if (roomNumber == 2)
      {
         UIManager.shotsTaken += 1;
         remainingEnemy = GameObject.FindWithTag("Enemy");
         if (remainingEnemy == null)
         {
            StartCoroutine(WaitThenClose());
         }
      }
   }

   private IEnumerator WaitThenClose()
   {
      yield return new WaitForSeconds(2);
      Application.Quit();
   }
}