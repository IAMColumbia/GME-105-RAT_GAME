using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 4;
    public int currentHealth;
    public Canvas canvasGame;
    public TextMeshProUGUI healthText;
    public Canvas canvasGameover;

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

        if (currentHealth == 0)
        {
            GameOver();
        }
    }

    // enables/disables ui correctly and destroys player.
    public void GameOver()
    {
        canvasGame.enabled = false;
        canvasGameover.enabled = true;
        Destroy(gameObject);
    }
}
