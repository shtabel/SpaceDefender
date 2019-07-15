using System.Collections.Generic;       //Allows us to use Lists.
using UnityEngine;

public class StageManager : MonoBehaviour
{
   private int level;

   private int maxSpawnRate = 400;
   private int minSpawnRate = 50;
   private int spawnRate = 100;

   private int enemyCount;
   public List<List<EnemyScript>> wave;

   public void KillEnemy(EnemyScript killed)
   {
      int pts = killed.level * 10;
      GameManager.instance.AddScore(pts);
   }

   public void InitGame(int lvl)
   {
      level = lvl;
      CreateWave();
   }

   void CreateWave()
   {
      wave = new List<List<EnemyScript>>();
      int waveWidth = 5+level;
      int waveHeight = 3+level;

      for (int h=0; h < waveHeight; h++)
      {
         for (int w=0; w< waveWidth; w++)
         {
            GameObject toInst = GameManager.instance.enemy;
            GameObject instance = Instantiate(toInst, new Vector3(w-2, h, 0f), Quaternion.identity);
            EnemyScript es = instance.GetComponent<EnemyScript>();
            if (es!=null)
               es.SetOffsets(h, w, waveWidth-1-w);
            //instance.SetActive(false);
         }
      }
   }


}
