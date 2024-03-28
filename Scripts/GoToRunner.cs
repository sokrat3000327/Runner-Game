using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToRunner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Move()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void MoveToMultiplayer(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
