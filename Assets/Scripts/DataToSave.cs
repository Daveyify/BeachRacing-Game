using UnityEngine;
public class DataToSave
{
    public string username;
    public string time;
    public string car;
    public int balls;

    public DataToSave()
    {
        this.username = PlayerPrefs.GetString("username");
        this.time = PlayerPrefs.GetString("time");
        this.car = SetCarName();
        this.balls = PlayerPrefs.GetInt("balls");
    }

    public string SetCarName()
    {
        int carSelection = Player.carSelect;

        if (carSelection == 0)
        {
            car = "X-RAW";
        }
        if (carSelection == 1)
        {
            car = "BLITZ";
        } 
        if (carSelection == 2)
        {
            car = "TRACKET 05";
        }
        return car;
    }

}
