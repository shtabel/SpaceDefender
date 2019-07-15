using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class bgLightScript : MonoBehaviour
{

   float duration = 30f;
   public Color startColor;
   public Color endColor;


   private Light2D l2d;

   private void Start()
   {
      l2d = GetComponent<Light2D> ();
   }
 
   void Update()
   {
      l2d.color = Color.Lerp(startColor, endColor,  Mathf.PingPong(Time.time/duration, 1));
   }
 
 
}
