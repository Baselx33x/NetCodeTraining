using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{
    
    NetworkVariable<int> m_Scroe = new NetworkVariable<int>(0,NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public GameObject m_SpawnBullet = null;


    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();


        m_Scroe.OnValueChanged += (oldValue, newValue) =>
        {
            Debug.Log( "Player:  " + OwnerClientId + "   |  Score: " + newValue);
        };

    }
    void Update()
    {



        if (!IsOwner) { return; }




        //TestServerRPC();
        TestClientRPC();




        Vector3 Movment = Vector3.zero; 
        
        if (Input.GetKey(KeyCode.W)) Movment.z = 1f;
        if (Input.GetKey(KeyCode.S)) Movment.z = -1f;
        if (Input.GetKey(KeyCode.A)) Movment.x = -1f;
        if (Input.GetKey(KeyCode.D)) Movment.x = 1f;
        if (Input.GetKey(KeyCode.Space)) Movment += Vector3.up;

        
        transform.position += Movment * Time.deltaTime * 5f;
        
    }

    [ServerRpc]
    void TestServerRPC ()
    {
        //if (Input.GetKey(KeyCode.Q)) m_Scroe.Value++;
        //if (Input.GetKey(KeyCode.E)) m_Scroe.Value--;

        if (Input.GetKey(KeyCode.Q))
        {

            var bullet = Instantiate(m_SpawnBullet, transform.position, Quaternion.identity);

            bullet.GetComponent<NetworkObject>().Spawn(true);

        }

    }


    [ClientRpc]
    void TestClientRPC()
    {
        //if (Input.GetKey(KeyCode.Q)) m_Scroe.Value++;
        //if (Input.GetKey(KeyCode.E)) m_Scroe.Value--;


        if (Input.GetKey(KeyCode.Q))
        {

            var bullet = Instantiate(m_SpawnBullet, transform.position, Quaternion.identity);

            bullet.GetComponent<NetworkObject>().Spawn(true);

        }
    }
}
