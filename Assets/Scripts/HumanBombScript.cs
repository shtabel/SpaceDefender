using UnityEngine;

public class HumanBombScript : MonoBehaviour
{

   private float speed = 1.5f;
   public Rigidbody2D rb2d;
   public GameObject impactEffect;

   // Start is called before the first frame update
   void Start()
   {
      //rb2d.velocity = transform.up * -speed;
      float playerX = GameManager.instance.player.transform.position.x;
      rb2d.velocity = new Vector2(playerX-transform.position.x, -speed);
   }

   private void FixedUpdate()
   {
      if (transform.position.y <= -10)
         Destroy(gameObject);
      transform.Rotate(Vector3.forward);
   }

   private void OnTriggerEnter2D(Collider2D hitInfo)
   {
      PlayerScript target = hitInfo.GetComponent<PlayerScript>();
      

      if (target != null)
      {
         target.TakeDamage(10);
         Instantiate(impactEffect, transform.position, transform.rotation);
         Destroy(gameObject);
      }
      
   }

   public void Die()
   {
      GameManager gm = GameManager.instance;
      gm.AddScore(1);
      Destroy(gameObject);
   }
}
