using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkScript : MonoBehaviour
{
   private float speed = 1f;
   private int k=1;
   public Rigidbody2D rb2d;

   // Start is called before the first frame update
   void Start()
   {
      int xx = Random.Range(-1,2);
      rb2d.velocity = new Vector2(xx*Random.Range(0f, 0.7f), -speed);
      //rb2d.velocity = transform.up * -speed;
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
         //GameManager.instance.AddSpark();
         Destroy(gameObject);
      }
      
   }
}
