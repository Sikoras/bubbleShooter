using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSpawningScript : MonoBehaviour {

    public int SetMat;

    public Renderer rend;

    public Material[] materials;

    [SerializeField]
    private List<GameObject> ColliderObjects = new List<GameObject>();

    public bool moving;

    public float direction;

    [SerializeField]
    private float moveSpeed;

    public bool Spawned;

    private bool hitWall;

    private bool isIn;

    public GameObject GM;


    private GameManagerScript GMScript;


    // 1 is Blue, 2 is Green, 3 is Red

    // Use this for initialization
    void Start()
    {

        GMScript = GM.GetComponent<GameManagerScript>();

        if (SetMat == 4)
        {
            Destroy(this);
        }
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = materials[SetMat];
        moving = true;
        if (Spawned)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
        hitWall = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (moving)
        {
            if (Spawned)
            {
                transform.position += transform.up * Time.deltaTime * moveSpeed;
            }
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Sphere")
        {
            
            moving = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;

            if(ColliderObjects.Count == 0)
            {
                ColliderObjects.Add(collision.gameObject);
            }

            if (ColliderObjects.Count < 6)
            {
                foreach (GameObject i in ColliderObjects)
                {
                    if (i == collision.gameObject)
                    {
                        break;
                    }
                    ColliderObjects.Add(collision.gameObject);
                }
            }

            if (Spawned)
            {
                GMScript.allowedFire = true;
                GMScript.toDestroy.Add(gameObject);
                CheckLinked(SetMat);
            }

            //Would use this collision function to detect the colour of the collided spheres.
            //This would then chain together and give the score.
        }

        if (collision.gameObject.tag == "rightwall")
        {
            if (hitWall)
            {
                gameObject.transform.Rotate(0, 0, 90);
            }
        }
        if (collision.gameObject.tag == "leftwall")
        {
            if (hitWall)
            {
                gameObject.transform.Rotate(0, 0, -90);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!hitWall)
        {
            hitWall = true;
        }
    }

    public void CheckLinked(int MatSet)
    {
        if (ColliderObjects.Count > 20)
        {
            foreach (GameObject i in ColliderObjects)
            {
                if (i == gameObject)
                {
                    break;
                }
                int CheckMat = i.GetComponent<SphereSpawningScript>().SetMat;
                if (CheckMat == MatSet)
                {
                    GMScript.toDestroy.Add(i);
                    i.GetComponent<SphereSpawningScript>().CheckLinked(MatSet);
                }
            }
        }
    }

}
