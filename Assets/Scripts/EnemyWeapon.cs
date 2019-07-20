using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
   
   public Transform firepoint;
   public GameObject bombPrefab;

   public void Shoot(int level)
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
