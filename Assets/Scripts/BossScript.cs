using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{

   protected float moveSpeed = 1f;
   protected float speed = 0.1f;
   private float speedOffset = 0.05f;
   protected float maxSpeed = 1.75f;
   private Rigidbody2D rb2d;
   private bool goingLeft = true;
   protected int tickToSpeedup = 100;
   protected int ticks = 0;
   protected Slider hpSlider = null;

   protected float destY = 0;

   protected int health = 1000;
   protected int maxHealth = 1000;
   public GameObject deathEffect;

   /* shooting */
   protected int ticksToShoot;
   protected int maxTicksToShoot = 100;
   protected int minTicksToShoot = 40;
   /* -shooting */

   void Start()
   {
      rb2d = GetComponent<Rigidbody2D> ();
      destY = 1.5f;
      if (hpSlider == null)
      {
         hpSlider = StageManager.instance.bossHpSlider;
      }
      hpSlider.maxValue = maxHealth;
      hpSlider.value = health;
   }
   
   protected void Update()
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
   }

   public virtual void Shoot()
   {
      // overridden in childs
   }

   public void TakeDamage(int damage)
   {
      if (StageManager.instance.EnemiesLeft())
         return;
      health -= damage;
      hpSlider.value = health;
      if (health<=0)
         Die();
   }

   void Die()
   {
      Instantiate(deathEffect, transform.position, Quaternion.identity);
      Instantiate(deathEffect, transform.position + new Vector3(0.14f,0,0), Quaternion.identity);
      Instantiate(deathEffect, transform.position - new Vector3(0.14f,0,0), Quaternion.identity);
      Instantiate(deathEffect, transform.position + new Vector3(0.1f,0.1f,0), Quaternion.identity);
      Instantiate(deathEffect, transform.position - new Vector3(0.1f,0.2f,0), Quaternion.identity);
      gameObject.SetActive(false);
      StageManager.instance.KillBoss();
      Destroy(this);
   }

   public void SpeedUp()
   {
      if (moveSpeed < maxSpeed)
      {
         moveSpeed += speedOffset;
      }
      else  if (moveSpeed > maxSpeed){
         moveSpeed = maxSpeed;
      }
      if (minTicksToShoot >= 30){
         maxTicksToShoot -= 10;
         minTicksToShoot -= 10;
      }
   }


}
