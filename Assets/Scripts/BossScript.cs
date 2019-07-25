using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{

   private float moveSpeed = 1f;
   public float speed = 0.1f;
   private float speedOffset = 0.05f;
   private float maxSpeed = 1.5f;
   private Rigidbody2D rb2d;
   private bool goingLeft = true;
   private int tickToSpeedup = 100;
   private int ticks = 0;
   public Slider hpSlider = null;

   private float destY = 0;

   private int health = 1000;
   private int maxHealth = 1000;
   public GameObject deathEffect;

   /* shooting */
   private int ticksToShoot;
   private int maxTicksToShoot = 100;
   private int minTicksToShoot = 30;
   /* -shooting */

   void Start()
   {
      rb2d = GetComponent<Rigidbody2D> ();
      destY = 1.5f;
      if (hpSlider == null)
      {
         hpSlider = StageManager.instance.bossHpSlider;
      }
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

      if (transform.position.x <= -3.2f && goingLeft || transform.position.x >= 3.2f && !goingLeft)
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

      /* shooting */
      if (ticksToShoot >0)
         ticksToShoot -=1;
      else if (ticksToShoot <=0 && Random.Range(0, 100) < 20)
      {
         if (maxTicksToShoot>minTicksToShoot)
            maxTicksToShoot -=1;
         ticksToShoot = maxTicksToShoot;
         if (transform.position.y<=2.5f)
            Shoot();
      }
      /* -shooting */      
   }

   public virtual void Shoot()
   {
      // overridden in childs
   }

   public void TakeDamage(int damage)
   {
      Debug.Log("Takad Damage");
      health -= damage;
      hpSlider.value = health;
      if (health<=0)
         Die();
   }

   void Die()
   {
      Instantiate(deathEffect, transform.position, Quaternion.identity);
      gameObject.SetActive(false);
      StageManager.instance.KillBoss();
      Destroy(this);
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


}
