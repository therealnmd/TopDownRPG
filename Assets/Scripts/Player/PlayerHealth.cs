using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public TMP_Text healthText;
    public Animator anim;

    private void Start()
    {
        healthText.text = "HP: " + StatsManager.Instance.currentHealth + "/" + StatsManager.Instance.maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        StatsManager.Instance.currentHealth += amount;
        anim.Play("TextUpdate");

        healthText.text = "HP: " + StatsManager.Instance.currentHealth + "/" + StatsManager.Instance.maxHealth; 
        if (StatsManager.Instance.currentHealth <= 0)
        {
            gameObject.SetActive(false); //gameobject o day la Player, khi het mau, Player se bien mat == chet
        }
    }
}
