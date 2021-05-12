using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    Vector3 startingPosition;

    public float moveSpeed = 0.5f;

    public bool tagged = false;

    MeshRenderer mr;

    public Vector3 startPos
    {
        set
        {
            startingPosition = value;
        }
    }

    public Material untaggedMat;
    public Material taggedMat;

    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        transform.position = new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), Random.Range(-20f, 20f));
    }

    private void OnEnable()
    {
        transform.position = new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), Random.Range(-20f, 20f));
    }

    // Update is called once per frame
    void Update()
    {
        mr.material = (tagged) ? taggedMat : untaggedMat;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (tagged)
            {
                GameManager.AddScore();
            }
            else
            {
                GameManager.LowerLives();
            }
            tagged = false;
            gameObject.SetActive(false);
        }
    }
}
