using UnityEngine;

public class EnemyScript : MonoBehaviour
{

   private float moveSpeed = 1f;
   private float speed = 0.1f;
   private float speedOffset = 0.05f;
   private float maxSpeed = 1.8f;
   private Rigidbody2D rb2d;
   private bool goingLeft = true;
   private int tickToSpeedup = 300;
   private int ticks = 0;
   private bool canShoot = false;

   private float destY = 0;
   private float leftOffset = 0;
   private float rightOffset = 0;

   public int level = 0;

   private int health = 20;
   private int maxHealth = 20;
   public GameObject deathEffect;

   // shooting
   public EnemyWeapon weapon;
   private int ticksToShoot;
   private int maxTicksToShoot = 300;
   private int minTicksToShoot = 100;
   // -shooting

   void Start()
   {
      rb2d = GetComponent<Rigidbody2D> ();
      //destY = Random.Range(0f, 2.3f);
      switch (level)
      {
         case 1:
            health = 20;
            maxHealth = 20;
         break;
         case 2:
            health = 30;
            maxHealth = 30;
         break;
      }
      ticksToShoot = Random.Range(0, maxTicksToShoot);

   }
   
   void Update()
   {
      float xx = speed;
      float yy = 0;
      if (goingLeft)
         xx *= -1;
      if (transform.position.y>destY)
         yy = -speed;
      rb2d.velocity = new Vector2(xx*moveSpeed, yy*moveSpeed);

      if (transform.position.x <= -3.2f+leftOffset && goingLeft || transform.position.x >= 3.2f-rightOffset && !goingLeft)
         goingLeft = !goingLeft;

      /* speedup by time */
      if (speed < maxSpeed) // speeding up
      {
         ticks +=1;
         if (ticks>=tickToSpeedup)
         {
            SpeedUp();
            ticks = 0;
         }
      }
      /* */

      // shooting
      if (canShoot) {
         if (ticksToShoot >0)
            ticksToShoot -=1;
         else if (ticksToShoot <=0 && Random.Range(0, 100) < 20)
         {
            if (maxTicksToShoot>minTicksToShoot)
               maxTicksToShoot -=1;
            ticksToShoot = maxTicksToShoot;
            if (transform.position.y<=2.5f)
               weapon.Shoot(level);
         }
      } 
      // -shooting
   }

   public void TakeDamage(int damage)
   {
      health -= damage;
      if (health<=0)
         Die();
   }

   void Die()
   {
      Instantiate(deathEffect, transform.position, Quaternion.identity);
      Instantiate(GameManager.instance.spark, transform.position, Quaternion.identity);
      gameObject.SetActive(false);
      health = maxHealth;
      speed = Random.Range(0.1f, maxSpeed);
      StageManager.instance.KillEnemy(this);
   }

   public void SetOffsets(float h, float left, float right)
   {
      destY = h*0.5f-1;
      leftOffset = left;
      rightOffset = right;
   }

   public void SpeedUp()
   {
      if (speed < maxSpeed)
      {
         speed += speedOffset;
      }
      if (minTicksToShoot>=20){
         maxTicksToShoot -= 20;
         minTicksToShoot -= 20;
      }
   }

   public void setIsFront(bool isFront)
   {
      canShoot = isFront;
   }

}
