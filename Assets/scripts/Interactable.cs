using UnityEngine;

public class InteractableTrigger : MonoBehaviour
{
    public string interactionType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player interacted with: " + interactionType);
            // Add your specific interaction logic here, e.g., open vending machine UI
        }
    }
}
