﻿using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

   private float moveSpeed = 1f;
   private float maxSpeed = 2f;
   private float minSpeed = 1f;
   private Rigidbody2D rb2d;

   public GameObject deathEffect;
   public int health = 100;
   public Slider hpSlider;

   void Start()
   {
      rb2d = GetComponent<Rigidbody2D> ();
      GameManager gm = GameManager.instance;
      gm.player = this;
   }
   
   void FixedUpdate()
   {
      float xx = Input.GetAxis ("Horizontal");
      if (transform.position.x <= -3.2f && xx<0 || transform.position.x >= 3.2f && xx>0)
         xx = 0;

      if (moveSpeed < maxSpeed && xx !=0)
         moveSpeed += Time.fixedDeltaTime;
      else if (moveSpeed>minSpeed && xx == 0)
         moveSpeed -= Time.fixedDeltaTime;

      rb2d.velocity = new Vector2(xx*moveSpeed, 0);
        
   }

   public void TakeDamage(int damage)
   {
      health -= damage;
      hpSlider.value = health;
      if (health<=0)
         Die();
   }

   public void GainHp(int hp)
   {
      health += hp;
      if (health>100)
         health=100;
      hpSlider.value = health;
   }

   void Die()
   {
      Instantiate(deathEffect, transform.position, Quaternion.identity);
      gameObject.SetActive(false);
   }
}
