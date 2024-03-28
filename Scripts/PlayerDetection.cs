using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


//this script to detect the triggers and the colliders
public class PlayerDetection : MonoBehaviour
{

    [Header(" Elements ")]
    [SerializeField] private CrowdSystem crowdSystem;
    private int locals=0;
    public TextMeshProUGUI score;
  

    void Start()
    {
      
       
       
        
    }

  
    void Update()
    {
        // to prevent the replication of touching the colliders also for finish line 
        if (GameManager.instance.IsGameState())
        {
            DetectDoors();
        }
        
        DataManager.instance.totalScoreT.text =PlayerPrefs.GetInt("Score").ToString();


    }

    private void DetectDoors()
    {
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, 1);

        /*
          Overlap Sphere this method create a sphere around the player at 
        any position we want and detect things inSide the sphere

        transform.position this is the center of sphere
        the second para is the radius of the sphere

        */

        for (int i = 0; i < detectedColliders.Length; i++)
        {
            /*
             this code detect the doors components attached to the specified collider

             */
            //Doors refer to the Doors class
            //we want to attach this method to the crowd system
            if (detectedColliders[i].TryGetComponent(out Doors doors))
            {
                Debug.Log("We hit some doors");

                int bonusAmount = doors.GetBonusAmount(transform.position.x);
                BonusType bonusType = doors.GetBonusType(transform.position.x);

                /*
                * this method when the player collide with the doors the
                * doors will disable 
                * to disable the increment addition of the player
                * and notmake the runner increased more than the amount
                * */
                doors.Disable();

                crowdSystem.ApplyBonus(bonusType, bonusAmount);
            }
        // this is when we reach to the end of the chuncks

            else if(detectedColliders[i].tag == "Finish")
            {
                Debug.Log("We've hit the finish line");

                PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);

                // when we reach to the finish line change the gameState to level complete and show the UI of level complete
                GameManager.instance.SetGameState(GameManager.GameState.LevelComplete);

                //SceneManager.LoadScene(0);
            }
            else if (detectedColliders[i].tag == "Player")
            {
                Debug.Log("we increase score");
                locals ++;
                score.text = locals.ToString();



                DataManager.instance.increaseScore(1);
                DataManager.instance.totalScoreT.text = DataManager.instance.total.ToString();
                Debug.Log(DataManager.instance.total);
                detectedColliders[i].enabled = false;

            }
        }
    }
}
