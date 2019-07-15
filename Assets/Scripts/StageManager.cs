using System.Collections.Generic;       //Allows us to use Lists.
using UnityEngine;

public class StageManager : MonoBehaviour
{
   private int level;

   private int maxSpawnRate = 400;
   private int minSpawnRate = 50;
   private int spawnRate = 100;

   private int enemyCount;
   private List<EnemyScript> activeEnemies = new List<EnemyScript>();
   private List<EnemyScript> poolEnemies = new List<EnemyScript>();

   private void FixedUpdate()
   {
      level = GameManager.instance.level;
      spawnRate -= level;
      if (spawnRate<=0)
      {
         SpawnEnemy();
         spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
      }

      if (enemyCount < level*2+10)
      {
         int lvl =1;
         if (level>=100)
            lvl = 2;
         else
            lvl = Random.Range(level,100) > 50/level ? 2 : 1;
         AddInactiveEnemy(lvl);
         enemyCount +=1;
      }
   }

   private void SpawnEnemy()
   {
      if (activeEnemies.Count == 0 && poolEnemies.Count>0){
         DropWave();
      }
      else if (poolEnemies.Count>0)
      {
         EnemyScript instance = poolEnemies[poolEnemies.Count-1];
         int k = Random.Range(0,100)>50 ? 1 : -1;
         instance.transform.position = new Vector3(4.7f*k, Random.Range(0f, 2.3f) ,0);
         instance.gameObject.SetActive(true);
         activeEnemies.Add(instance);
         poolEnemies.RemoveAt(poolEnemies.Count-1);
      }
   }
   
   private void DropWave()
   {
      int count = Mathf.RoundToInt(poolEnemies.Count/2);
      int until = poolEnemies.Count-count;
      for (int i=poolEnemies.Count-1; i>=until; i--){
         EnemyScript en = (EnemyScript) poolEnemies[i];
         poolEnemies.RemoveAt(i);
         activeEnemies.Add(en);
         en.transform.position = new Vector3(Random.Range(-4f,4f), Random.Range(0f, 2.3f)+2.5f ,0);
         en.Drop();
         en.gameObject.SetActive(true);
      }
      
   }

   public void KillEnemy(EnemyScript killed)
   {
      int pts = killed.level * 10;
      for (int i =activeEnemies.Count-1; i>=0; i--) {
         if (activeEnemies[i] == killed) {
            activeEnemies.RemoveAt(i);
            poolEnemies.Add(killed);
            break;
         }
      }
      GameManager.instance.AddScore(pts);
   }


   void AddInactiveEnemy(int lvl)
   {
      GameObject toInst = GameManager.instance.enemy;
      switch (lvl)
      {
         case 1:
            toInst = GameManager.instance.enemy;
         break;
         case 2:
            toInst = GameManager.instance.enemy2;
         break;
      }
      GameObject instance = Instantiate(toInst, new Vector3(0f, 0f, 0f), Quaternion.identity);
      instance.SetActive(false);
      poolEnemies.Add(instance.GetComponent<EnemyScript>());
   }


   public void InitGame(int lvl)
   {
      level = lvl;
      enemyCount = level*2+10;

      for (int i=0; i<enemyCount; i++)
      {
         AddInactiveEnemy(1);
      }
   }


}
