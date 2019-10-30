using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
    
public class GameManager : MonoBehaviour
{

   public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
   public GameObject enemy;
   public GameObject enemy2;
   public GameObject[] bosses;
   public GameObject spark;
   public PlayerScript player;
   public TextMeshProUGUI scoreLabel;
   public TextMeshProUGUI levelLabel;
   public TextMeshProUGUI finalScoreLabel;
   public TextMeshProUGUI bigLabel;
   public GameObject restartSplash;
   private bool reloading = false;
   private bool started = false;

   private int level = 1;
   private int score = 0;

   //Awake is always called before any Start functions
   void Awake(){
      Cursor.visible = false; 
      if (instance == null)
         instance = this;
      else if (instance != this)
         Destroy(gameObject);
      
      DontDestroyOnLoad(gameObject); //Sets this to not be destroyed when reloading scene
   }

   private void FixedUpdate()
   {
      levelLabel.text = level.ToString();
   }

   //Update is called every frame.
   void Update()
   {
      if (Input.GetButtonDown("Cancel"))
         Application.Quit();
      if (!started && Input.GetButtonDown("Fire1"))
         StartGame();
      if (started && player != null && !player.isActiveAndEnabled && !reloading)
         StartCoroutine(Restart());         
   }
   
   public void AddScore(int x)
   {
      //if (Mathf.RoundToInt(score/25) != Mathf.RoundToInt((score+x)/25))
      //   player.GainHp(1);
      score +=x;
      scoreLabel.text = score.ToString();
   }

   public void LevelComplete()
   {
      StageManager.instance.ClearLevel();
      AddScore(level*100);
      StartCoroutine(NextLevel());
   }

   void StartGame()
   {
      started = true;
      restartSplash.SetActive(false);
      StageManager.instance.InitGame(level);
   }

   public void StartBoss()
   {
      StageManager.instance.InitBoss(bosses[0]);
   }

   IEnumerator NextLevel(){
      level +=1;
      yield return new WaitForSeconds(2f);
      
      if (level % 2 == 0)
         StartBoss();
      else
         StageManager.instance.InitGame(level);
      
   }

   IEnumerator Restart()
   {
      reloading = true;
      finalScoreLabel.text = "";

      switch (Random.Range(0,4))
      {
         case 0:
            bigLabel.text = "Wasted";
         break;
         case 1:
            bigLabel.text = "YOU DIED!";
         break;
         case 2:
            bigLabel.text = "GAME OVER";
         break;
         case 3:
            bigLabel.text = "Press F to Pay Respects";
         break;
      }      

      restartSplash.SetActive(true);
      yield return new WaitForSeconds(2f);
      finalScoreLabel.text = string.Format("Score: {0}", score);
      level = 1;
      score = 0;
      StageManager.instance.ClearLevel();
      yield return new WaitForSeconds(2f);
      SceneManager.LoadScene("SampleScene");
      started = false;
      reloading = false;
   }

}