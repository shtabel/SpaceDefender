using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class PointLightScript : MonoBehaviour
{
   
   public float minInternsity = 0.5f;
   public float maxInternsity = 1f;


   private Light2D l2d;
   private bool lighting;
   private float deltaIntensity = 0.004f;

   private void Start()
   {
      l2d = GetComponent<Light2D> ();
      l2d.intensity = minInternsity;
      lighting = true;
   }

   // Update is called once per frame
   void Update()
   {
      if (lighting)
      {
         l2d.intensity += deltaIntensity;
         if (l2d.intensity>=maxInternsity)
            lighting = false;
      }
      else
      {
         l2d.intensity -= deltaIntensity;
         if (l2d.intensity<=minInternsity)
            lighting = true;
      }
        
   }
}
