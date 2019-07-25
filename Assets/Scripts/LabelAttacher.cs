using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LabelAttacher : MonoBehaviour
{
   // Start is called before the first frame update
   void Start()
   {
      
      TextMeshProUGUI label = this.GetComponent<TextMeshProUGUI>();
      if (this.name=="LevelLabel")
         GameManager.instance.levelLabel = label;
      else if (this.name=="ScoreLabel")
         GameManager.instance.scoreLabel = label;
      else if (this.name=="RestartSplash")
         GameManager.instance.restartSplash = gameObject;
      else if (this.name=="FinalScore")
         GameManager.instance.finalScoreLabel = label;
      else if (this.name=="BigLabel")
         GameManager.instance.bigLabel = label;
      else if (this.name=="BossSlider"){
         StageManager.instance.bossHpSlider = this.gameObject.GetComponent<Slider>();
         this.gameObject.SetActive(false);
      }

   }

}
