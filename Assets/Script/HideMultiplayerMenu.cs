using UnityEngine;

public class HideMultiplayerMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;

    public void HideMenu()
    {
        if (menuPanel != null)
        {
            menuPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("HideMultiplayerMenu: Menu Panel is not assigned!");
        }
    }
}
