using System.Collections.Generic;       //Allows us to use Lists.
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
   public static StageManager instance = null; 
   private int level;
   private int waveWidth;
   private int waveHeight;
   private float waveDensity;
   public bool spawnBoss = false;

   public Slider bossHpSlider = null;

   private int enemyCount = 0;
   private List<List<GameObject>> wave;

  //Awake is always called before any Start functions
   void Awake(){
      if (instance == null)
         instance = this;
      else if (instance != this)
         Destroy(gameObject);
      
      DontDestroyOnLoad(gameObject); //Sets this to not be destroyed when reloading scene
   }

   public bool EnemiesLeft()
   {
      return enemyCount>0;
   }

   public void KillEnemy(EnemyScript killed)
   {
      int pts = killed.level * 10;
      enemyCount -=1;
      GameManager.instance.AddScore(pts);
      if (enemyCount > 0)
         RecalcOffsets();
      else
         GameManager.instance.LevelComplete();
      if (level == 3 & enemyCount < waveHeight*waveWidth/2 && spawnBoss)
         GameManager.instance.StartBoss();
   }

   public void KillBoss()
   {
      bossHpSlider.gameObject.SetActive(false);
      GameManager.instance.AddScore(1000);
      GameManager.instance.LevelComplete();
   }

   public void InitGame(int lvl)
   {
      level = lvl;
      enemyCount = 0;
      CreateWave();
   }

   public void InitBoss(GameObject boss)
   {
      //waveWidth = 0;
      //waveHeight = 0;
      spawnBoss = false;
      bossHpSlider.gameObject.SetActive(true);
      Instantiate(boss, new Vector3(0, 2.9f, 0f), Quaternion.identity);
   }

   void CreateWave()
   {
      wave = new List<List<GameObject>>();
      waveWidth = 5+level-1;
      waveHeight = 3+level-1;
      waveDensity = 6.4f/waveWidth;
      float waveOffset = 3f * (Random.Range(0,3)-1);
      for (int h=0; h < waveHeight; h++)
      {
         List<GameObject> row = new List<GameObject>();
         for (int w=0; w < waveWidth; w++)
         {
            GameObject toInst;
            if(waveHeight-level <= h)
               toInst = GameManager.instance.enemy2;
            else
               toInst = GameManager.instance.enemy;

            GameObject instance = Instantiate(toInst, new Vector3((w+waveOffset)*waveDensity, h+2.9f, 0f), Quaternion.identity);
            row.Add(instance);
            enemyCount +=1;
            EnemyScript es = instance.GetComponent<EnemyScript>();
            if (es!=null){
               es.SetOffsets(h, w*waveDensity, (waveWidth-1-w)*waveDensity);
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
            es.SetOffsets(h, (w-leftMost)*waveDensity, (rightMost-w)*waveDensity);
            es.SpeedUp();
 
         }
      }      
   }

   public void ClearLevel()
   {
      for (int w=0; w < waveWidth; w++)
      {
         for (int h=0; h < waveHeight; h++)
         {
            GameObject inst = wave[h][w];
            if (inst != null)
               Destroy(inst);
         }
      } 
   }


}
