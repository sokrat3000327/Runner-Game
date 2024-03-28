using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public int total;

    


    public TextMeshProUGUI totalScoreT;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        total = PlayerPrefs.GetInt("Score",0);
    }
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
       totalScoreT.text = PlayerPrefs.GetInt("Score").ToString();

    }
    public void increaseScore(int s)
    {
        total += s;
           
        PlayerPrefs.SetInt("Score",total);
    }

}
