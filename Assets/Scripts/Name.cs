using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Name : MonoBehaviourPun
{
    public Text nametext;
    // Start is called before the first frame update
    void Start()
    {
        if (!photonView.IsMine)
        {
            if(photonView.Owner.NickName != "New Text")
            {
                nametext.text = photonView.Owner.NickName;
            }
        }
    }

}
