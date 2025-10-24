using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    /// <summary>
    ///             maxHealth - what is the maximum ammount of health the player can have/start with?
    ///             currentHealth - what is the current health the player has?
    ///             canvasGame - what is the canvas for the player's health?
    ///             healthText - what is the health going to be displayed on?
    ///             canvasGameover - what is the game over canvas?
    ///             heldItem - is there an item the player is holding?
    /// </summary>
    public int maxHealth = 4;
    public int currentHealth;
    public Canvas canvasGame;
    public TextMeshProUGUI healthText;
    public Canvas canvasGameover;
    public Item heldItem;

    // calls components, sets up components, and enables ui correctly for player upon startup.
    void Start()
    {
        canvasGame = GameObject.Find("Health").GetComponent<Canvas>();
        canvasGameover = GameObject.Find("GameOver").GetComponent<Canvas>();
        currentHealth = maxHealth;

        canvasGame.enabled = true;
        canvasGameover.enabled = false;

        healthText.SetText("Health: " + currentHealth.ToString());
    }

    // deducts and updates the health using the ammount given when called.
    public void TakeDamage(int amount)
    {
        if (currentHealth <= 0) return;

        currentHealth = Mathf.Max(0, currentHealth - amount);
        healthText.SetText("Health: " + currentHealth.ToString());

        if (heldItem != null && heldItem.pickedUp)
        {
            DropItem();
        }

        if (currentHealth == 0)
        {
            GameOver();
        }
    }

    // spawns item copy, and removes item,in player script.
    private void DropItem()
    {
        heldItem.gameObject.SetActive(true);
        heldItem.transform.position = transform.position;
        heldItem.pickedUp = false;
        heldItem = null;
    }

    // enables/disables ui correctly and destroys player.
    public void GameOver()
    {
        canvasGame.enabled = false;
        canvasGameover.enabled = true;
        Destroy(gameObject);
    }


}
