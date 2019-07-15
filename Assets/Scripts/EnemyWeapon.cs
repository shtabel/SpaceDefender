using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
   
   public Transform firepoint;
   public GameObject bombPrefab;

   public int level = 0;
   public int damage = 10;
   private int ticksToShoot;
   private int maxTicksToShoot = 300;
   private int minTicksToShoot = 100;

   private void Start()
   {
      ticksToShoot = Random.Range(0, maxTicksToShoot);
   }

   // Update is called once per frame
   void FixedUpdate()
   {
      if (ticksToShoot >0)
         ticksToShoot -=1;
      else if (ticksToShoot <=0 && Random.Range(0, 100) < 20)
      {
         if (maxTicksToShoot>minTicksToShoot)
            maxTicksToShoot -=1;
         ticksToShoot = maxTicksToShoot;
         Shoot();
      } 
   }

   void Shoot()
   {
      switch (level)
      {
         case 1:
            Instantiate(bombPrefab, firepoint.position, Quaternion.identity);
         break;
         case 2:
            Instantiate(bombPrefab, firepoint.position+new Vector3(0.1f,0f,0f), Quaternion.identity);
            Instantiate(bombPrefab, firepoint.position+new Vector3(-0.1f,0f,0f), Quaternion.identity);
         break;
      }
      
   }
}
