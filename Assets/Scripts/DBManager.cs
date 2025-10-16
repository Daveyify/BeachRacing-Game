using Firebase.Database;
using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine;

public class DBManager : MonoBehaviour
{
    public DataToSave dataToSave;
    public string scoreID;
    FirebaseFirestore firestore;


    private static int counter = 0;

    private void Awake()
    {
        firestore = FirebaseFirestore.DefaultInstance;
    }

    public void SaveData()
    {
        Debug.Log("Intentando guardar datos en Firebase...");
        
        dataToSave = new DataToSave();

        setScoreID();

        var data = new
        {
            username = dataToSave.username,
            carName = dataToSave.car,
            balls = dataToSave.balls,
            time = dataToSave.time,
            date = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };

        firestore.Collection("scores").AddAsync(data).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Data saved on Firestore!");
            }
            else
            {
                Debug.LogError($"Error saving on Firestore: {task.Exception}");
            }
        });
    }


    public void setScoreID()
    {
        counter++; 
        scoreID = $"SCORE{counter:D3}";
    }
}
