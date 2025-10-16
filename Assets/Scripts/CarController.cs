using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    // Balls
    public int balls = 0;
    public TMP_Text scoreText;

    // Colliders
    public WheelCollider wheelFR;
    public WheelCollider wheelFL;
    public WheelCollider wheelRR;
    public WheelCollider wheelRL;

    // Mesh
    public GameObject wheelMeshFR;
    public GameObject wheelMeshFL;
    public GameObject wheelMeshRR;
    public GameObject wheelMeshRL;

    // Audio
    public AudioSource audioData;
    public AudioSource audioPoint;
    public AudioSource audioCheckpoint;
    public AudioMixer audioMixer;

    public ParticleSystem humo;

    public float motorTorque = 1;
    public float maxAngle = 30;

    public float movX;
    public float movZ;

    public bool isStopped = false;
    public float stopBrakeForce = 500f;

    public Timer timerSC;
    public DBManager dbManager;

    public Animator ballsAnimator;

    public Image fadeImage;

    float fadeTime = 0f;
    public AudioSource fadeAudio;
    bool isDead = false;
    bool fadingOut = false;
    

    void Start()
    {
       timerSC = FindFirstObjectByType<Timer>();
       dbManager = FindFirstObjectByType<DBManager>();
    }
    void Update()
    {
        CarMovement();
        AcelerationSound();
        Smoke();

        if (isDead)
        {
            FadeOnDeath();
        }

        if (fadingOut)
        {
            FadeOut();
        }
    }

    public void CarMovement()
    {
        if (isStopped)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) ||
                Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                ReleaseBrakes(); 
            }
            else
            {
                StopCar();
            }
        }

        movX = 0;
        movZ = 0;

        if (Input.GetKey(KeyCode.A)) movX = -1f;
        if (Input.GetKey(KeyCode.D)) movX = 1f;
        if (Input.GetKey(KeyCode.W)) movZ = 5f;
        if (Input.GetKey(KeyCode.S)) movZ = -5f;

        wheelFR.motorTorque = movZ * motorTorque;
        wheelFL.motorTorque = movZ * motorTorque;

        wheelFR.steerAngle = movX * maxAngle;
        wheelFL.steerAngle = movX * maxAngle;

        wheelFR.brakeTorque = 0f;
        wheelFL.brakeTorque = 0f;
        wheelRR.brakeTorque = 0f;
        wheelRL.brakeTorque = 0f;

        UpdateWheels(wheelFL, wheelMeshFL);
        UpdateWheels(wheelFR, wheelMeshFR);
        UpdateWheels(wheelRL, wheelMeshRL);
        UpdateWheels(wheelRR, wheelMeshRR);
    }

    public void UpdateWheels(WheelCollider wheelcol, GameObject wheelM)
    {
        Vector3 pos = new Vector3();
        Quaternion rot = Quaternion.identity;
        wheelcol.GetWorldPose(out pos, out rot);
        wheelM.transform.position = pos;
        wheelM.transform.rotation = rot;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Balls"))
        {
            balls += 1;
            UpdateScoreText();
            Destroy(other.gameObject);
            audioPoint.Play();
            ballsAnimator.SetTrigger("Balls");
        }

        if (other.CompareTag("End"))
        {
            PlayerPrefs.SetString("time", timerSC.textTimer.text);
            PlayerPrefs.SetInt("balls", balls);
            PlayerPrefs.Save();

            dbManager.SaveData();

            SceneManager.LoadScene("SceneGameOver");
        }

        if (other.CompareTag("Checkpoint"))
        {
            audioCheckpoint.Play();
        }

        if (other.CompareTag("Water"))
        {
            StopCar();
            isDead = true;
            fadeTime = 0f;
        }

        else
        {
            ballsAnimator.SetTrigger("NotBalls");
        }
    }

    public void UpdateScoreText()
    {
        scoreText.text = "BALLS: " + balls.ToString();
    }

    public void AcelerationSound()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            audioMixer.SetFloat("PitchFX", 2);
        }
        else
        {
            audioMixer.SetFloat("PitchFX", 1);
        }
    }

    public void Smoke()
    {
        if (Input.GetKey(KeyCode.W))
        {
            humo.Play();
        }
        else
        {
            if (humo.isPlaying)
                humo.Stop();
        }
    }

    public void setPlayerScore()
    {
        PlayerPrefs.SetInt("Score", balls);
    }

    void StopCar()
    {
        isStopped = true;

        wheelFR.motorTorque = 0f;
        wheelFL.motorTorque = 0f;
        wheelRR.motorTorque = 0f;
        wheelRL.motorTorque = 0f;

        wheelFR.brakeTorque = stopBrakeForce;
        wheelFL.brakeTorque = stopBrakeForce;
        wheelRR.brakeTorque = stopBrakeForce;
        wheelRL.brakeTorque = stopBrakeForce;

        movX = 0;
        movZ = 0;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    void ReleaseBrakes()
    {
        Debug.Log("ReleaseBrakes called");
        isStopped = false;

        wheelFR.brakeTorque = 0f;
        wheelFL.brakeTorque = 0f;
        wheelRR.brakeTorque = 0f;
        wheelRL.brakeTorque = 0f;
    }

    public void FadeOnDeath()
    {
        fadeTime += Time.deltaTime;
        float alpha = Mathf.Lerp(0f, 1f, fadeTime / 2f);
        fadeAudio.Play();

        Color color = fadeImage.color;
        color.a = alpha;
        fadeImage.color = color;

        if (alpha >= 1f)
        {
            isDead = false;
            fadeTime = 0f;
            Invoke("StartFadeOut", 2f);
        }
    }

    void StartFadeOut()
    {
        fadingOut = true;
        fadeTime = 0f;
    }

    public void FadeOut()
    {
        fadeTime += Time.deltaTime;
        float alpha = Mathf.Lerp(1f, 0f, fadeTime / 2f);

        Color color = fadeImage.color;
        color.a = alpha;
        fadeImage.color = color;

        if (alpha <= 0f)
        {
            fadingOut = false;
        }
    }

}
