using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Instantiate : MonoBehaviour
{
    [SerializeField] private GameObject PLayerPrefab = null;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate(PLayerPrefab.name, new Vector3(Random.Range(220.0f, 230.0f), 34.0f, 230.0f), Quaternion.identity);
    }
}
