using UnityEngine;

public class HumanBombScript : MonoBehaviour
{

   private float speed = 1.5f;
   private int k = 1;
   public Rigidbody2D rb2d;
   public GameObject impactEffect;

   // Start is called before the first frame update
   void Start()
   {
      if (Random.Range(0,2)==1)
         k = -1;
      float playerX = GameManager.instance.player.transform.position.x;
      float x = (playerX-transform.position.x)/Random.Range(1.8f, 2.2f);
      rb2d.velocity = new Vector2(x, -speed);
   }

   private void FixedUpdate()
   {
      if (transform.position.y <= -10)
         Destroy(gameObject);
      
      transform.Rotate(Vector3.forward*k);
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
