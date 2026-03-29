using Unity.Netcode;
using UnityEngine;

public class NetworkManagerUI : MonoBehaviour
{
    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10,10,200,100));

        if(!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            if(GUILayout.Button("Host"))
                NetworkManager.Singleton.StartHost();

            if(GUILayout.Button("Client"))    
                NetworkManager.Singleton.StartClient();

        }   

        GUILayout.EndArea();

    }
}
