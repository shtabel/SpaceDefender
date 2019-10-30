using UnityEngine;

public class EnemyBombScript : MonoBehaviour
{

   private float speed = 2.5f;
   private int k=1;
   public Rigidbody2D rb2d;
   public GameObject impactEffect;
   public int damage = 10;

   // Start is called before the first frame update
   void Start()
   {
      rb2d.velocity = transform.up * -speed;
      if (Random.Range(0,2)==1)
         k = -1;
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
         target.TakeDamage(damage);
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
