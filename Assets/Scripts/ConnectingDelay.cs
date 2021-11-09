using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectingDelay : MonoBehaviour
{
    public Button playButton;
    public Text connectingText;

    // Start is called before the first frame update
    void Start()
    {
        playButton.interactable = false;
        Invoke("EnablePlayButton", 4);
        Invoke("DisableConnectingText", 4);
    }

    public void EnablePlayButton(){
        playButton.interactable = true;
    }
    public void DisableConnectingText(){
        connectingText.enabled = false;}
}
