using UnityEngine;

public class Player : MonoBehaviour
{
    public static int carSelect = 0;

    public void SetCar(int myCar)
    {
        carSelect = myCar;
    }
}
