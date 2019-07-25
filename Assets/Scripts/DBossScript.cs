using UnityEngine;

public class DBossScript : BossScript
{

   public Transform firepoint;
   public GameObject bombPrefab;

   private void Awake()
   {
      speed = 0.3f;
   
   }

   public override void Shoot()
   {
      Instantiate(bombPrefab, firepoint.position, Quaternion.identity);
     
   }


}
