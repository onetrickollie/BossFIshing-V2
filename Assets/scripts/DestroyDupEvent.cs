using UnityEngine;

public class DestroyIfDupEvent : MonoBehaviour
{
    private void Awake()
    {
        if (FindObjectsOfType<UnityEngine.EventSystems.EventSystem>().Length > 1)
        {
            Destroy(gameObject); // Destroy this instance if another EventSystem exists
        }
    }
}
