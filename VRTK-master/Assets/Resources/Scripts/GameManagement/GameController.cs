using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    //Arrays of toy game objects based on how many snaps they require to complete
    public GameObject[] twoSnapToys, threeSnapToys, fourSnapToys, fiveSnapToys;

    //Game difficulty
    public enum CurrentDifficulty { None, VeryEasy, Easy, Medium, MediumHard, Hard, VeryHard, Impossible }
    public CurrentDifficulty currentDifficulty;

    //Game timer
    private float gameTimer;

    //Spawn timer and delay
    private float spawnDelay;
    private float spawnTimer;

    //Spawn position
    private Vector3 spawnPos;

    //Score
    private float score = 0f;
    public TextMesh scoreText;
    public TextMesh strikesText;
    private int strikes = 0;

    //Debug Text Mesh
    public TextMesh difficultyText;
    public TextMesh spawnRateText;

    public bool enabled = true;
    

    // Use this for initialization
    void Start()
    {
        spawnPos = transform.Find("Spawner").transform.position;

        EventManager.CompleteItemExitedConveyorMethods += OnCompleteItemExitConveyor;
        EventManager.IncompleteItemExitedConveyorMethods += OnIncompleteItemExitConveyor;
        EventManager.DifficultyChangedMethods += OnDifficultyChanged;
         
    }

    // Update is called once per frame
    void Update()
    {
        if (enabled)
        {
            //Increment game timer
            gameTimer += Time.deltaTime % 60;
            spawnTimer += Time.deltaTime % 60;

            //Calculate the difficulty based on the game timer
            CalculateDifficulty();

            //After the current spawn delay, spawn an item
            if (spawnTimer > spawnDelay)
            {          
                SpawnToy(currentDifficulty);
            }
        }
    }

    public void OnCompleteItemExitConveyor(GameObject obj)
    {
        score++;
        scoreText.text = score.ToString();
        Destroy(obj.transform.root.gameObject);
    }

    public void OnIncompleteItemExitConveyor(GameObject obj)
    {
        strikes++;
        strikesText.text = strikes.ToString();
        Destroy(obj);
    }

    public void OnDifficultyChanged()
    {
        //Update text elements
        spawnRateText.text = spawnDelay.ToString();
        difficultyText.text = currentDifficulty.ToString();
    }

    private void CalculateDifficulty()
    {        
        if (gameTimer >= 0 && gameTimer < 30 && currentDifficulty != CurrentDifficulty.VeryEasy)
        {
            currentDifficulty = CurrentDifficulty.VeryEasy;
            spawnDelay = 3f;
            OnDifficultyChanged();
        }
        else if (gameTimer >= 30 && gameTimer < 60 && currentDifficulty != CurrentDifficulty.Easy)
        {
            currentDifficulty = CurrentDifficulty.Easy;
            spawnDelay = 2f;
            OnDifficultyChanged();
        }
        else if (gameTimer >= 60 && gameTimer < 90 && currentDifficulty != CurrentDifficulty.Medium)
        {
            currentDifficulty = CurrentDifficulty.Medium;
            spawnDelay = 1f;
            OnDifficultyChanged();
        }
        else if (gameTimer >= 90 && gameTimer < 120 && currentDifficulty != CurrentDifficulty.MediumHard)
        {
            currentDifficulty = CurrentDifficulty.MediumHard;
            spawnDelay = 0.5f;
            OnDifficultyChanged();
        }
        else if (gameTimer >= 120 && gameTimer < 150 && currentDifficulty != CurrentDifficulty.Hard)
        {
            currentDifficulty = CurrentDifficulty.Hard;
            OnDifficultyChanged();
        }
        else if (gameTimer >= 150 && gameTimer < 180 && currentDifficulty != CurrentDifficulty.VeryHard)
        {
            currentDifficulty = CurrentDifficulty.VeryHard;
            OnDifficultyChanged();
        }
        else if (gameTimer >= 180 && gameTimer < 210 && currentDifficulty != CurrentDifficulty.Impossible)
        {
            currentDifficulty = CurrentDifficulty.Impossible;
            OnDifficultyChanged();
        }
    }

    private void SpawnToy(CurrentDifficulty difficulty)
    {
        GameObject toyToSpawn = null;

        //If easy difficulty, spawn only 2-snap toys
        if (currentDifficulty == CurrentDifficulty.VeryEasy)
        {
            //Choose random 2 snap toy
            toyToSpawn = twoSnapToys[Random.Range(0, twoSnapToys.Length)];
        }
        else if (currentDifficulty == CurrentDifficulty.Easy)
        {
            //Choose either 2-snap or 3-snap
            int random = Random.Range(0, 2);

            if (random == 0)
            {
                //spawn 2-snap toy
                toyToSpawn = twoSnapToys[Random.Range(0, twoSnapToys.Length)];
            }
            else if (random == 1)
            {
                //spawn 3-snap toy
                toyToSpawn = threeSnapToys[Random.Range(0, threeSnapToys.Length)];
            }
        }

        if (toyToSpawn != null)
        {
            //Spawn the object and then for each child remove parent
            for (int i = 0; i < toyToSpawn.transform.childCount; i++)
            {
                GameObject toy = Instantiate(toyToSpawn.transform.GetChild(i), spawnPos, Quaternion.identity).gameObject;
                toy.name = toyToSpawn.transform.GetChild(i).name;
            }
        }

        //Reset the spawn timer
        spawnTimer = 0f;
    }
}
