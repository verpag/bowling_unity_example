using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBall : MonoBehaviour
{
    public float force;
    // Start is called before the first frame update

    private List<Vector3> pinPositions;
    private List<Quaternion> pinRotations;
    private Vector3 ballPosition;

    private void Start()
    {
        var pins = GameObject.FindGameObjectsWithTag("Pin");
        pinPositions = new List<Vector3>();
        pinRotations = new List<Quaternion>();
        foreach (var pin in pins)
        {
            pinPositions.Add(pin.transform.position);
            pinRotations.Add(pin.transform.rotation);
        }

        ballPosition = GameObject.FindGameObjectWithTag("Ball").transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, force));
        if (Input.GetKeyUp(KeyCode.LeftArrow))
            GetComponent<Rigidbody>().AddForce(new Vector3(1, 0, 0),
           ForceMode.Impulse);
        if (Input.GetKeyUp(KeyCode.RightArrow))
            GetComponent<Rigidbody>().AddForce(new Vector3(-1, 0, 0),
           ForceMode.Impulse);
        if (Input.GetKeyUp(KeyCode.R))
        {
            var pins = GameObject.FindGameObjectsWithTag("Pin");
            for (int i = 0; i < pins.Length; i++)
            {
                //collision.gameObject.transform.parent.gameObject.tag
                var pinPhysics = pins[i].GetComponent<Rigidbody>();
                pinPhysics.velocity = Vector3.zero;
                pinPhysics.position = pinPositions[i];
                pinPhysics.rotation = pinRotations[i];
                pinPhysics.velocity = Vector3.zero;
                pinPhysics.angularVelocity = Vector3.zero;
                var ball = GameObject.FindGameObjectWithTag("Ball");
                ball.transform.position = ballPosition;
                ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
                ball.GetComponent<Rigidbody>().angularVelocity =
               Vector3.zero;
            }
        }
        if (Input.GetKeyUp(KeyCode.B))
        {
            var ball = GameObject.FindGameObjectWithTag("Ball");
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            ball.transform.position = ballPosition;
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pin")
            GetComponent<AudioSource>().Play();
    }
}