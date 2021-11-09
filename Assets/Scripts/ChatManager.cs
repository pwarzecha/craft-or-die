using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Chat;
using Photon.Pun;
using TMPro;
//using UnityEngine.UI;
//using ExitGames.Client.Photon;
using System.Text.RegularExpressions;
public class ChatManager : MonoBehaviour
{
    public TMP_InputField ChatInput;
    public TextMeshProUGUI ChatContent;
    private PhotonView _photon;
    private List<string> _messages = new List<string>();
    private float _buildDelay = 0f;
    private int _maximumMessages = 5;
    void Start() {
        _photon = GetComponent<PhotonView>();
    }
    [PunRPC]
    void RPC_AddNewMessage(string msg){
        _messages.Add(msg);
    }

    public void SendChat(string msg){
        string NewMessage = PhotonNetwork.NickName + ": " + msg;
        _photon.RPC("RPC_AddNewMessage", RpcTarget.All, NewMessage);
    }

    public void SubmitChat(){
        string blankCheck = ChatInput.text;
        blankCheck = Regex.Replace(blankCheck, @"\s", "");
        if (blankCheck == ""){
            ChatInput.ActivateInputField();
            ChatInput.text = "";
            return;
        }
        SendChat(ChatInput.text);
        ChatInput.ActivateInputField();
        ChatInput.text = "";
    }

    void BuildChatContents()
    {
        string NewContents = "";
        foreach (string s in _messages)
        {
            NewContents += s + "\n";
        }
        ChatContent.text = NewContents;
    }
    void Update() {
        if (PhotonNetwork.InRoom)
        {
            ChatContent.maxVisibleLines = _maximumMessages;
            if(_messages.Count > _maximumMessages){
                _messages.RemoveAt(0);
            }
            if(_buildDelay < Time.time){
                BuildChatContents();
                _buildDelay = Time.time + 0.25f;
            }
        }
        else if(_messages.Count > 0){
            _messages.Clear();
            ChatContent.text = "";
        }
    }






    /*--------------------------------------------------------chat system that didnt worked.
    private ChatClient chatClient;
    public TextMeshProUGUI connectionState;
    public TMP_InputField msgInput;
    public TextMeshProUGUI msgArea;

    public GameObject msgPanel;

    private string worldchat;  
    [SerializeField] private string userID;
    // Start is called before the first frame update

    private void Awake() {
        userID = PlayerPrefs.GetString("USERNAME");
    }
    void Start()
    {
        Application.runInBackground = true;
        if(string.IsNullOrEmpty(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat)){
            Debug.LogError("No AppID Provided");
            return;
        }

        worldchat = "world";
    }

    // Update is called once per frame
    void Update()
    {
            chatClient.Service();
    }

    public void GetConnected(){
        Debug.Log("Connecting");
        chatClient = new ChatClient(this);
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new Photon.Chat.AuthenticationValues(userID));
    }
    public void SendMsg()
    {
        chatClient.PublishMessage(worldchat, msgInput.text);
    }
    public void DebugReturn(ExitGames.Client.Photon.DebugLevel level, string message){
    
    }
    //public void OnNetworkInstantiate(NetworkMessageInfo info) 
    //{
    //} 
    public void OnDisconnected() 
    {
       
    }
    public void OnConnected()
    {
        chatClient.Subscribe(new string[] {worldchat});
        chatClient.SetOnlineStatus(ChatUserStatus.Online);
    }
    public void OnChatStateChange(ChatState state)
    {
        
    }
    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        for (int i = 0; i< senders.Length; i++)
        {
            msgArea.text += senders[i] + ": " + messages[i] + ", ";
        }
    }
    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        
    }
    public void OnSubscribed(string[] channels, bool[] results)
    {
        connectionState.text = "Connected";
        foreach(var channel in channels)
        {
            this.chatClient.PublishMessage(channel, "joined");
        }

        
    }
    public void OnUnsubscribed(string[] channels)
    {
        
    }
    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        
    }
    public void OnUserSubscribed(string channel, string user)
    {
        
    }
    public void OnUserUnsubscribed(string channel, string user)
    {
       
    }*/

}
