using TMPro;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private int score = 0; // Variabel för att hålla reda på poäng
    [SerializeField] private TextMeshProUGUI scoreText; // Referens till UI-texten som visar poängen

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kolla om objektet som kolliderade har taggen "Collectible"
        if (collision.CompareTag("Fruit"))
        {
            score++; // Öka poängen med 1
            scoreText.text = "Score: " + score; // Uppdatera UI-texten med den nya poängen
            Destroy(collision.gameObject); // Förstör det insamlade objektet
        }
    }
}
