using UnityEngine;

public class EnemyScript : MonoBehaviour
{

   private float moveSpeed = 1f;
   private float speed = 0.1f;
   private float maxSpeed = 1f;
   private Rigidbody2D rb2d;
   private bool goingLeft = true;
   private int tickToSpeedup = 300;
   private int ticks = 0;

   private float destY = 0;
   public int level = 0;

   private int health = 20;
   private int maxHealth = 20;
   public GameObject deathEffect;

   void Start()
   {
      rb2d = GetComponent<Rigidbody2D> ();
      destY = Random.Range(0f, 2.3f);
      switch (level)
      {
         case 1:
            health = 20;
            maxHealth = 20;
         break;
         case 2:
            health = 40;
            maxHealth = 40;
         break;
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

      if (transform.position.x <= -4 && goingLeft || transform.position.x >= 4 && !goingLeft)
         goingLeft = !goingLeft;

      if (speed < maxSpeed) // speeding up
      {
         ticks +=1;
         if (ticks>=tickToSpeedup)
         {
            speed += 0.1f;
            ticks = 0;
         }
      }
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
      gameObject.SetActive(false);
      health = maxHealth;
      speed = Random.Range(0.1f, maxSpeed);
      GameManager gm = GameManager.instance;
      gm.stageManager.KillEnemy(this);
   }

   public void Drop()
   {
      destY = Random.Range(0f, 2.3f);
   }
}
