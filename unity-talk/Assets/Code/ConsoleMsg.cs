using UnityEngine;

public class ConsoleMsg : MonoBehaviour
{
    void Start ()
    {
        Debug.Log("I am a Log.");
        Debug.LogWarning("I am a Warning.");
        Debug.LogError("I am an Error.");

    }
}