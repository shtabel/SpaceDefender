using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

   private float moveSpeed = 1f;
   private float maxSpeed = 2.5f;
   private float minSpeed = 1f;
   private Rigidbody2D rb2d;

   public GameObject deathEffect;
   public int health = 100;
   public Slider hpSlider;

   void Start()
   {
      rb2d = GetComponent<Rigidbody2D> ();
      GameManager gm = GameManager.instance;
      gm.player = this;
   }
   
   void Update()
   {
      float xx = Input.GetAxis ("Horizontal");
      if (transform.position.x <= -3.5 && xx<0 || transform.position.x >= 3.5 && xx>0)
         xx = 0;

      float yy = Input.GetAxis ("Vertical");
      if (transform.position.y <= -2.2 && yy<0 || transform.position.y >= -1 && yy>0)
         yy = 0;

      if (moveSpeed < maxSpeed && xx !=0)
         moveSpeed += Time.deltaTime;
      else if (moveSpeed>minSpeed && xx == 0)
         moveSpeed -= Time.deltaTime;

      rb2d.velocity = new Vector2(xx*moveSpeed, yy*maxSpeed);
        
   }

   public void TakeDamage(int damage)
   {
      health -= damage;
      hpSlider.value = health;
      if (health<=0)
         Die();
   }

   public void GainHp(int hp)
   {
      health += hp;
      if (health>100)
         health=100;
      hpSlider.value = health;
   }

   void Die()
   {
      Instantiate(deathEffect, transform.position, Quaternion.identity);
      gameObject.SetActive(false);
   }
}
