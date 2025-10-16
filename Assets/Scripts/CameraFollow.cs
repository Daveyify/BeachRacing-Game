using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject[] Carros;
    public Vector3 offset = new Vector3(0, 5, -10);
    public float suavizado = 5f;

    private Transform objetivo;

    void Start()
    {
        int index = Player.carSelect;
        if (index >= 0 && index < Carros.Length)
        {
            objetivo = Carros[index].transform;
        }
        else
        {
            Debug.LogWarning("Carro seleccionado fuera de rango");
        }
    }

    void LateUpdate()
    {
        if (objetivo == null) return;

        Vector3 posicionDeseada = objetivo.position + objetivo.TransformDirection(offset);
        transform.position = Vector3.Lerp(transform.position, posicionDeseada, suavizado * Time.deltaTime);
        transform.LookAt(objetivo);
    }
}
