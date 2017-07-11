using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public Slider SlideController;
    private float Rot;

    [SerializeField]
    private GameObject SphereSpawning;

    [SerializeField]
    private GameObject SpawnPoint;

    [SerializeField]
    private GameObject SpawningPoints;

    [SerializeField]
    private GameObject GM;

    private GameManagerScript GMScript;

    // Use this for initialization
    void Start () {
        GMScript = GM.GetComponent<GameManagerScript>();
        int ChildCount = SpawningPoints.transform.GetChildCount();
        for(int i = 0; i < ChildCount; i++)
        {
            GameObject ChildGO;
            ChildGO = SpawningPoints.transform.GetChild(i).gameObject;
            GameObject SpawnedObject;
            SpawnedObject = Instantiate(SphereSpawning, ChildGO.transform.position, ChildGO.transform.rotation);
            int randomMat = Random.Range(0, 3);
            SpawnedObject.GetComponent<SphereSpawningScript>().SetMat = randomMat;
            SpawnedObject.GetComponent<SphereSpawningScript>().GM = GM;


        }
	}
	
	// Update is called once per frame
	void Update () {

        Rot = SlideController.value;
        this.transform.localEulerAngles = new Vector3(0, 0, Rot);
        	
	}

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void Fire()
    {
        if (GMScript.allowedFire)
        {
            GMScript.ClearList();
            GameObject SpawnedObject = Instantiate(SphereSpawning, SpawnPoint.transform.position, this.transform.rotation);
            SpawnedObject.GetComponent<SphereSpawningScript>().direction = this.transform.rotation.z;
            SpawnedObject.GetComponent<SphereSpawningScript>().moving = true;
            SpawnedObject.GetComponent<SphereSpawningScript>().Spawned = true;
            int randomMat = Random.Range(0, 3);
            SpawnedObject.GetComponent<SphereSpawningScript>().SetMat = randomMat;
            SpawnedObject.GetComponent<SphereSpawningScript>().GM = GM;
            GMScript.allowedFire = false;
        }
    }
}
