using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
   public Transform firepoint;
   public int damage = 10;
   private int cooldown = 0;
   private int maxCooldown = 15;
   private bool shooting = false;

   public GameObject impactEffect;
   public LineRenderer lineRenderer;

   // Update is called once per frame
   void Update()
   {
      if (cooldown<=0)
         if (Input.GetButtonDown("Fire1") || Input.GetAxis("Fire1")!=0)
         {
            StartCoroutine(Shoot());
            shooting = true;
         }
   }

   private void FixedUpdate()
   {
      if (shooting){
         shooting = false;
         cooldown = maxCooldown;
      }
      if (cooldown>=0)
         cooldown -=1;
   
   }

   IEnumerator Shoot()
   {
      //shooting logic
      RaycastHit2D hitInfo = Physics2D.Raycast(firepoint.position, firepoint.up);
      if (hitInfo)
      {
         EnemyScript enemy = hitInfo.transform.GetComponent<EnemyScript>();
         EnemyBombScript enemyBomb = hitInfo.transform.GetComponent<EnemyBombScript>();
         if (enemy != null)
            enemy.TakeDamage(damage);
         else if (enemyBomb != null)
            enemyBomb.Die();
         Instantiate(impactEffect, hitInfo.point, Quaternion.identity);
         lineRenderer.SetPosition(0, firepoint.position);
         lineRenderer.SetPosition(1, hitInfo.point);
      }
      else
      {
         lineRenderer.SetPosition(0, firepoint.position);
         lineRenderer.SetPosition(1, firepoint.position + firepoint.up*100);
      }
      lineRenderer.enabled = true;
      yield return new WaitForSeconds(0.02f);
      lineRenderer.enabled = false;
   }
}
