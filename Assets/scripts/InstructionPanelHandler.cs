using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InstructionPanelHandler : MonoBehaviour
{
    private Button instructionButton;

    private void Start()
    {
        instructionButton = GetComponent<Button>();

        if (instructionButton != null)
        {
            instructionButton.onClick.AddListener(OnInstructionPanelClicked);
        }
    }

    public void OnInstructionPanelClicked()
    {
        GameManager.Instance.StartGame();
    }
}
