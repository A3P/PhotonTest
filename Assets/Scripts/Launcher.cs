using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Com.MyCompany.MyGame
{
    public class Launcher : MonoBehaviour
    {
        #region Private Serializable Fields

        #endregion

        #region Private Fields

        /// <summary>
        /// This client's version number. Users are seperated from each other by gameVersion (which allows you to make breaking changes).
        /// </summary>
        string gameVersion = "1";

        [Tooltip("The Ui Panel to let the user enter name, connect and play")]
        [SerializeField]
        private GameObject controlPanel;
        [Tooltip("The UI Label to inform the user that the connection is in progress")]
        [SerializeField]
        private GameObject progressLabel;

        #endregion

        #region MonoBehaviour CallBacks

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
        /// </summary>
        void Awake(){
            // #Critical
            // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room synch their level automatically
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        // <summary>
        // MonoBehaviour method called on GameObject by Unity during initialization phase.
        // </summary>
        void Start()
        {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
        }

        #endregion

        #region Public Methods

        // <summary>
        // Start the connection Process.
        // - If already connected, we attempt joining a random room
        // - if not yet connected, Connect this application instance to Photon Cloud Network
        // </summary>
        public void Connect()
        {
            progressLabel.SetActive(true);
            controlPanel.SetActive(false);
            // we check if we are connected or not, we join if we are, else we initiate the connection to the server
            if (PhotonNetwork.IsConnected)
            {
                // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
                Debug.Log("Connected!");
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                // #Critical, we must first and foremost connect to Photon Online Server.
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;
            }
            
            if (PhotonNetwork.IsConnected)
            {
                Debug.Log("Connected!");
            }
        }

    #endregion

    }
}