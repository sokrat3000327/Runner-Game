using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public static ChunkManager instance;

    [Header(" Elements ")]
    [SerializeField] private LevelSO[] levels;
    private GameObject finishLine;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateLevel();

        finishLine = GameObject.FindWithTag("Finish");
    }

    // Update is called once per frame
    void Update()
    {
        DataManager.instance.totalScoreT.text = ConvertArabic.ConvertNumeralsToArabic2(PlayerPrefs.GetInt("Score").ToString());

    }


    // this code generate the levels you want 


    // select the number of the level and create it
    private void GenerateLevel()
    {
        int currentLevel = GetLevel();
        currentLevel = currentLevel % levels.Length;
        if (currentLevel == 0)
        {
            DataManager.instance.total = 0;
        }

        LevelSO level = levels[currentLevel];

        CreateLevel(level.chunks);
    }
    // this code create the chuncks
    private void CreateLevel(Chunk[] levelChunks)
    {
        Vector3 chunkPosition = Vector3.zero;

        for (int i = 0; i < levelChunks.Length; i++)
        {
            Chunk chunkToCreate = levelChunks[i];

            if (i > 0)
                chunkPosition.z += chunkToCreate.GetLength() / 2;

            // quaterion this is the angle of rotation 0
            Chunk chunkInstance = Instantiate(chunkToCreate, chunkPosition, Quaternion.identity, transform);

            chunkPosition.z += chunkInstance.GetLength() / 2;
        }
    }


    public float GetFinishZ()
    {
        return finishLine.transform.position.z;
    }

    public int GetLevel()
    {
        //this class save a data into your device
        //level is akey of the integer value
        return PlayerPrefs.GetInt("level", 0);
    }
}
