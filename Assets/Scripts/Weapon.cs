using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
   public Transform firepoint;
   public int damage = 10;
   private float cooldown = 0;
   private int maxCooldown = 15;
   private int fireRate = 85;
   private bool shooting = false;
   public Slider heatSlider;
   private int heat = 100;  // inverted: 0 - hot, 100 - cool
   private float coolingRate = 15f;
   private float cooling = 0f;
   private int shootHeat = 5;

   public GameObject impactEffect;
   public LineRenderer lineRenderer;

   // Update is called once per frame
   void Update()
   {
      if (heat>=shootHeat)
         if (cooldown<=0)
            if (Input.GetButtonDown("Fire1") || Input.GetAxis("Fire1")!=0)
            {
               StartCoroutine(Shoot());
               shooting = true;
            }
   }

   private void FixedUpdate()
   {
      if (cooldown>=0)
         cooldown -= fireRate * Time.fixedDeltaTime;
      else
         shooting = false;

      if (heat<100)
         cooling +=coolingRate*Time.fixedDeltaTime;
         if (cooling >=1)
         {
            heat +=1;
            cooling = 0;
            heatSlider.value = heat;
         }
   
   }

   IEnumerator Shoot()
   {
      cooldown = maxCooldown;
      heat -= shootHeat;
      heatSlider.value = heat;
      //shooting logic
      RaycastHit2D hitInfo = Physics2D.Raycast(firepoint.position, firepoint.up);
      if (hitInfo && hitInfo.distance <= 4.2f)
      {
         EnemyScript enemy = hitInfo.transform.GetComponent<EnemyScript>();
         EnemyBombScript enemyBomb = hitInfo.transform.GetComponent<EnemyBombScript>();
         BossScript boss = hitInfo.transform.GetComponent<BossScript>();
         HumanBombScript humanBomb = hitInfo.transform.GetComponent<HumanBombScript>();

         if (enemy != null)
            enemy.TakeDamage(damage);
         else if (enemyBomb != null)
            enemyBomb.Die();
         else if (humanBomb != null)
            humanBomb.Die();
         else if (boss != null){
            if (hitInfo.collider.name != "Shield")
               boss.TakeDamage(damage);
         }
         Instantiate(impactEffect, hitInfo.point, Quaternion.identity);
         lineRenderer.SetPosition(0, firepoint.position);
         lineRenderer.SetPosition(1, hitInfo.point);
      }
      else
      {
         lineRenderer.SetPosition(0, firepoint.position);
         lineRenderer.SetPosition(1, firepoint.position + firepoint.up*4.2f);
      }
      lineRenderer.enabled = true;
      yield return new WaitForSeconds(0.02f);
      lineRenderer.enabled = false;
   }
}
