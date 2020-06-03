using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;

namespace Com.MyCompany.MyGame
{
    public class GameManager : MonoBehaviourPunCallbacks
    {

        #region Photon Callbacks

        // Called when the local player left the room. We need the load the launcher scene.
        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }

        public override void OnPlayerEnteredRoom(Player other){
            // Not seen if you are the player connecting
            Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName);
        
            if (PhotonNetwork.IsMasterClient) {
                // called before OnPlayerLeftRoom
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient);
            
                LoadArena();
            }
        }

        public override void OnPlayerLeftRoom(Player other) {
            // Seen when others disconnect
            Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName);
        
            if (PhotonNetwork.IsMasterClient) {
                // called before OnPlayerLeftRoom
                Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient);
            
                LoadArena();
            }
        }

        #endregion

        #region Private Methods

        void LoadArena(){
            if(!PhotonNetwork.IsMasterClient){
                Debug.LogError("PhtotonNetwork : Trying to Load a level but we are not the master Client");
            }
            int playerCount = 2;
            if(PhotonNetwork.CurrentRoom.PlayerCount > 2){
                playerCount = 4;
            }
            Debug.LogFormat("PhotonNetwork : Loading Level : {0}", playerCount);
            PhotonNetwork.LoadLevel("Room for " + playerCount);
        }

        #endregion

        #region Public Methods

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        #endregion
    }
}