using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAdmin : MonoBehaviour {

    public CotohaAccessToken cotohaAccessToken { get; private set; }

    private void Awake() {
        cotohaAccessToken = GetComponent<CotohaAccessToken>();
    }

}
