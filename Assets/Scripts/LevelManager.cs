using UnityEngine;
using UnityEngine.UIElements;
using Unity.Cinemachine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] Cars;
    public CinemachineCamera cinemachineCamera;


    void Start()
    {
        Cars[0].SetActive(false);
        Cars[1].SetActive(false);
        Cars[2].SetActive(false);
        Cars[Player.carSelect].SetActive(true);

        cinemachineCamera.Follow = Cars[Player.carSelect].transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Pause();
        }
    }
}

