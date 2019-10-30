using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
   
   public Transform firepoint;
   public GameObject bombPrefab;

   public void Shoot(int level)
   {
      Instantiate(bombPrefab, firepoint.position, Quaternion.identity);
      
   }
}
