using System.Collections.Generic;       //Allows us to use Lists.
using UnityEngine;

public class StageManager : MonoBehaviour
{
   private int level;
   private int waveWidth;
   private int waveHeight;

   private int enemyCount;
   public List<List<GameObject>> wave;

   public void KillEnemy(EnemyScript killed)
   {
      int pts = killed.level * 10;
      GameManager.instance.AddScore(pts);
      RecalcOffsets();
   }

   public void InitGame(int lvl)
   {
      level = lvl;
      CreateWave();
   }

   void CreateWave()
   {
      wave = new List<List<GameObject>>();
      waveWidth = 5+level;
      waveHeight = 3+Mathf.RoundToInt(level/3);

      for (int h=0; h < waveHeight; h++)
      {
         List<GameObject> row = new List<GameObject>();
         for (int w=0; w < waveWidth; w++)
         {
            GameObject toInst = GameManager.instance.enemy;
            GameObject instance = Instantiate(toInst, new Vector3(w-2, h+2.7f, 0f), Quaternion.identity);
            row.Add(instance);
            EnemyScript es = instance.GetComponent<EnemyScript>();
            if (es!=null){
               es.SetOffsets(h, w, waveWidth-1-w);
               es.setIsFront(h==0);
            }
            //instance.SetActive(false);
         }
         wave.Add(row);
      }
   }

   void RecalcOffsets()
   {
      int leftMost = -1;
      int rightMost = -1;

      for (int w=0; w < waveWidth; w++)
      {
         for (int h=0; h < waveHeight; h++)
         {
            GameObject inst = wave[h][w];
            if (inst != null && inst.active)
            {
               if (leftMost == -1){
                  leftMost = w;
                  rightMost = w;
               }else{
                  rightMost = w;
               }

               EnemyScript es = inst.GetComponent<EnemyScript>();
               es.setIsFront(true);
               break;
            }
         }
      }
      
      for (int w=0; w < waveWidth; w++)
      {
         for (int h=0; h < waveHeight; h++)
         {
            GameObject inst = wave[h][w];
            if (inst == null || !inst.active)
               continue;
            EnemyScript es = inst.GetComponent<EnemyScript>();
            es.SetOffsets(h, w-leftMost, rightMost-w);
 
         }
      }


      
   }


}
