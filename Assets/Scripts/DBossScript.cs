using UnityEngine;

public class DBossScript : BossScript
{

   public Transform firepoint;
   public GameObject bombPrefab;
   public GameObject obeyPrefab;
   public GameObject signPrefab;
   private int phase = 0;
   private int fired = 0;

   private void Awake()
   {
      maxTicksToShoot = 200;
      minTicksToShoot = 160;
      tickToSpeedup = 300;
      speed = 0.3f;
      health = 1500;
      maxHealth = 1500;
      fired = -5;
      if (StageManager.instance.EnemiesLeft())
         phase = 0;
      else
         phase = 2;
   }

   public override void Shoot()
   {
      Instantiate(bombPrefab, firepoint.position, Quaternion.identity);
   }

   private void DoubleShoot()
   {
      GameObject toInst;
      if (Random.Range(0,100) > 50)
         toInst = obeyPrefab;
      else
         toInst = signPrefab;
      Instantiate(toInst, firepoint.position - new Vector3(-0.3f, 0, 0), Quaternion.identity);
      Instantiate(toInst, firepoint.position - new Vector3(0.3f, 0, 0), Quaternion.identity);
   }

   private void Update()
   {
      base.Update();
      if (phase > 0 && !hpSlider.IsActive())
         hpSlider.gameObject.SetActive(true);
         

      if (phase != 0 && phase !=1)
      {
         /* speedup by time */
         if (moveSpeed < maxSpeed) // speeding up
         {
            ticks +=1;
            if (ticks>=tickToSpeedup)
            {
               SpeedUp();
               ticks = 0;
            }
         }
         /* */
      }

      if (phase==0 && !StageManager.instance.EnemiesLeft()) {
         phase = 1;
         fired = 0;
         moveSpeed = 0f;
         maxSpeed = 0f;
      }
      else if (fired >= 42 && phase == 1){
         SpeedUp();
         phase = 2;
         fired = -5;
         moveSpeed = 1.3f;
         maxSpeed = 2f;
         if (destY >=0)
            destY -= 0.3f;
      }
      else if (fired >= Random.Range(20, 30) && phase == 2)
      {
         fired = -5;
         phase = 1;
         fired = 0;
         moveSpeed = 0f;
         maxSpeed = 0f;
         SpeedUp();
      }
      
      /* shooting */
      if (ticksToShoot >0)
         ticksToShoot -=1;
      else if (ticksToShoot <=0 && Random.Range(0, 100) < 20)
      {
         if (phase == 1)
            ticksToShoot = 10;
         else if (phase == 0)
            ticksToShoot = Random.Range(minTicksToShoot*4, maxTicksToShoot*4);
         else
            ticksToShoot = Random.Range(minTicksToShoot, maxTicksToShoot);
         fired +=1;
         if (transform.position.y<=2.5f && fired >=0)
            if (phase == 2)
               DoubleShoot();
            else
               Shoot();
      }
      /* -shooting */  
   }


}
