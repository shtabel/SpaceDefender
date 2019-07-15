using UnityEngine;

public class EffectScript : MonoBehaviour
{
   private Animator anim;
   // Start is called before the first frame update
   void Start()
   {
      anim = GetComponent<Animator>();              
   }

   private void Update()
   {
      AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex("Base Layer"));
      
      if (state.tagHash == 951154001) //End
         Destroy(gameObject);
      
   }

}
