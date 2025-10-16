using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{

    public GameObject car;
    public GameObject[] checkpoints;
    Vector3 vectorPoint;
    public CarController carController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            Invoke("ResetPosition", 4f);
        }

        if (other.CompareTag("Checkpoint"))
        {
            vectorPoint = car.transform.position;
            Destroy(other.gameObject);
        }
    }


    void ResetPosition()
    {
        car.transform.position = vectorPoint;
    }
}
