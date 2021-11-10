using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
namespace UnityStandardAssets.Characters.FirstPerson
{ 
    public class Disable : MonoBehaviour
    {
        private PhotonView view;
        private RigidbodyFirstPersonController user;
        //private FirstPersonUserControl user;
        private Rigidbody character;

        // Start is called before the first frame update
        void Start()
        {
            view = GetComponent<PhotonView>();
            user = GetComponent<RigidbodyFirstPersonController>();
            character = GetComponent<Rigidbody>();
            if (view.IsMine)
            {
                gameObject.tag = "Player";
            }
            else {
                Destroy(user);
                Destroy(character);
                Destroy(this);
            }
        }
    }
}