using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
    
public class GameManager : MonoBehaviour
{

   public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
   public StageManager stageManager;
   public GameObject enemy;
   public GameObject enemy2;
   public PlayerScript player;
   public TextMeshProUGUI scoreLabel;
   public TextMeshProUGUI levelLabel;

   public int level = 0;
   public int score = 0;

   //Awake is always called before any Start functions
   void Awake(){
      Cursor.visible = false; 
      if (instance == null)
         instance = this;
      else if (instance != this)
         Destroy(gameObject);
      
      DontDestroyOnLoad(gameObject); //Sets this to not be destroyed when reloading scene
      stageManager.InitGame(level);
   }

   private void FixedUpdate()
   {
      level = Mathf.RoundToInt(score/100);
      if (level <= 0)
         level = 1;
      levelLabel.text = level.ToString();
   }

   //Update is called every frame.
   void Update()
   {
      if (Input.GetButtonDown("Cancel"))
         Application.Quit();
      if (player != null && !player.isActiveAndEnabled)
         SceneManager.LoadScene("SampleScene");
         //Debug.Log("LOH!");
   }
   
   public void AddScore(int x)
   {
      if (Mathf.RoundToInt(score/25) != Mathf.RoundToInt((score+x)/25))
         player.GainHp(1);
      score +=x;
      scoreLabel.text = score.ToString();
   }

}