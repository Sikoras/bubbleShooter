using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    public bool allowedFire;

    public List<GameObject> toDestroy = new List<GameObject>();

	// Use this for initialization
	void Start () {
        allowedFire = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ClearList()
    {
        toDestroy.Clear();
    }
}
