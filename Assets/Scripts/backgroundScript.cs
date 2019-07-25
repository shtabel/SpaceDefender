using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundScript : MonoBehaviour
{
   private float speed = 0.1f;
   private float ySpeed = 0.00025f;
    
   // Update is called once per frame
   void Update()
   {
      float border = 1*speed;
      float xx = Input.GetAxis ("Horizontal");
      if (transform.position.x >= border && xx<0 || transform.position.x <= -border && xx>0)
         xx = 0;
      float xSpeed = xx*speed*Time.deltaTime;

      if (transform.position.y > -18.5){
         transform.position = transform.position - new Vector3(xSpeed, ySpeed ,0);
      }
         
         
      

   }
}
