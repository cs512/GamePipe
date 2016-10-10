using UnityEngine;
using System.Collections;

public class music : MonoBehaviour
{


    private static music instance = null;
    public static music Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
    }
    // any other methods you need

}