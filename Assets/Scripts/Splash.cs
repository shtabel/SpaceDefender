using UnityEngine;

public class Splash : MonoBehaviour
{
   public static Splash instance = null;

   void Awake(){

      if (instance == null)
         instance = this;
      else if (instance != this)
         Destroy(gameObject);
      //DontDestroyOnLoad(gameObject);
   }

}
