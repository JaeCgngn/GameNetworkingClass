using UnityEngine;
using Unity.Netcode;

[RequireComponent(typeof(NetworkObject))]
public class MultiplayerMenu : NetworkBehaviour
{
    [SerializeField] private GameObject menuPanel;

    public override void OnNetworkSpawn()
    {
        // Subscribe to disconnection events once spawned on the network
        if (IsClient)
        {
            NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
        }
    }

    public override void OnNetworkDespawn()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnected;
        }
    }

    public void StartHost()
    {
        // Execute network start locally
        if (NetworkManager.Singleton.StartHost())
        {
            HideMenuLocal();
        }
    }

    public void StartClient()
    {
        // Execute network start locally
        if (NetworkManager.Singleton.StartClient())
        {
            HideMenuLocal();
        }
    }

    public void StartServer()
    {
        if (NetworkManager.Singleton.StartServer())
        {
            HideMenuLocal();
        }
    }

    /// <summary>
    /// Hides the menu ONLY on the local machine clicking the button.
    /// Do NOT call this via a ServerRPC or ClientRPC.
    /// </summary>
    public void HideMenuLocal()
    {
        if (menuPanel != null)
        {
            menuPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("MultiplayerMenu: Menu Panel is not assigned in the Inspector!");
        }
    }

    private void OnClientDisconnected(ulong clientId)
    {
        // If the local client disconnected, restore their local menu UI
        if (clientId == NetworkManager.Singleton.LocalClientId)
        {
            if (menuPanel != null)
            {
                menuPanel.SetActive(true);
            }
        }
    }
}