using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class PointLightScript : MonoBehaviour
{

   private Light2D l2d;
   private bool lighting;
   private float deltaIntensity = 0.004f;

   private void Start()
   {
      l2d = GetComponent<Light2D> ();
      l2d.intensity = 1;
      lighting = true;
   }

   // Update is called once per frame
   void Update()
   {
      if (lighting)
      {
         l2d.intensity += deltaIntensity;
         if (l2d.intensity>=1f)
            lighting = false;
      }
      else
      {
         l2d.intensity -= deltaIntensity;
         if (l2d.intensity<=0.5f)
            lighting = true;
      }
        
   }
}
