using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdSystem : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private Transform runnersParent;
    [SerializeField] private GameObject runnerPrefab;

    [Header(" Settings ")]
    [SerializeField] private float radius;
    [SerializeField] private float angle;

   
    void Start()
    {
        
    }


    void Update()
    {

        //to prevent the replication of the code
        if (!GameManager.instance.IsGameState())
            return;

        PlaceRunners();

        //if there is no runner set the gamestate to gameover
        if (runnersParent.childCount <= 0)
            GameManager.instance.SetGameState(GameManager.GameState.Gameover);
    }

    private void PlaceRunners()
    {
        for (int i = 0; i < runnersParent.childCount; i++)
        {
            // assign the location to each object
            Vector3 childLocalPosition = GetRunnerLocalPosition(i);
            runnersParent.GetChild(i).localPosition = childLocalPosition;
        }
    }

    //the golden angle and golden radius
    private Vector3 GetRunnerLocalPosition(int index)
    {
        // transform from degree to radians
        float x = radius * Mathf.Sqrt(index) * Mathf.Cos(Mathf.Deg2Rad * index * angle);
        float z = radius * Mathf.Sqrt(index) * Mathf.Sin(Mathf.Deg2Rad * index * angle);

        return new Vector3(x, 0, z);
    }

    public float GetCrowdRadius()
    {
        return radius * Mathf.Sqrt(runnersParent.childCount);
    }

    public void ApplyBonus(BonusType bonusType, int bonusAmount)
    {
        switch(bonusType)
        {
            case BonusType.Addition:
                AddRunners(bonusAmount);
                break;

            case BonusType.Product:
                int runnersToAdd = (runnersParent.childCount * bonusAmount) - runnersParent.childCount;
                AddRunners(runnersToAdd);
                break;

            case BonusType.Difference:
                RemoveRunners(bonusAmount);
                break;

            case BonusType.Division:
                int runnersToRemove = runnersParent.childCount - (runnersParent.childCount / bonusAmount);
                RemoveRunners(runnersToRemove);
                break;
        }
    }

    private void AddRunners(int amount)
    {
        //add this runner prefab to this position
        for (int i = 0; i < amount; i++)
            Instantiate(runnerPrefab, runnersParent);

        playerAnimator.Run();
    }

    private void RemoveRunners(int amount)
    {
        if (amount > runnersParent.childCount)
            amount = runnersParent.childCount;

        int runnersAmount = runnersParent.childCount;

        for (int i = runnersAmount - 1; i >= runnersAmount - amount; i--)
        {
            //this get the position of the runner we want to decrease
            Transform runnerToDestroy = runnersParent.GetChild(i);
            runnerToDestroy.SetParent(null);
            //this decrease the number of runners
            Destroy(runnerToDestroy.gameObject);
        }
    }
}
