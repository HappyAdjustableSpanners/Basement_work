using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

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


	// Use this for initialization
	void Start () {
        spawnPos = transform.Find("Spawner").transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        //Increment game timer
        gameTimer += Time.deltaTime % 60;
        spawnTimer += Time.deltaTime % 60;

        //Calculate the difficulty based on the game timer
        CalculateDifficulty();

        //After the current spawn delay, spawn an item
        if (spawnTimer > spawnDelay)
        {
            //Choose random 2 snap toy
            GameObject toyToSpawn = twoSnapToys[Random.Range(0, twoSnapToys.Length - 1)];

            //Spawn the object and then for each child remove parent
            for(int i = 0; i < toyToSpawn.transform.childCount; i++)
            {
                GameObject toy = Instantiate(toyToSpawn.transform.GetChild(i), spawnPos, Quaternion.identity).gameObject;
                toy.name = toyToSpawn.transform.GetChild(i).name;
                toy.tag = "Toy";
            }

            //Reset the spawn timer
            spawnTimer = 0f;
        }
        


	}

    public void CompleteItemExitConveyor(GameObject obj)
    {
        score++;
        scoreText.text = score.ToString();
        Destroy(obj);
    }

    public void IncompleteItemExitConveyor(GameObject obj)
    {
        strikes++;
        strikesText.text = strikes.ToString();
        Destroy(obj);
    }

    private void CalculateDifficulty()
    {
        spawnRateText.text = spawnDelay.ToString();
        difficultyText.text = currentDifficulty.ToString();
        if (gameTimer >= 0 && gameTimer < 30 && currentDifficulty != CurrentDifficulty.VeryEasy)
        {
            currentDifficulty = CurrentDifficulty.VeryEasy;
            spawnDelay = 3f;
        }
        else if (gameTimer >= 30 && gameTimer < 60 && currentDifficulty != CurrentDifficulty.Easy)
        {
            currentDifficulty = CurrentDifficulty.Easy;
            spawnDelay = 2f;
        }
        else if (gameTimer >= 60 && gameTimer < 90 && currentDifficulty != CurrentDifficulty.Medium)
        {
            currentDifficulty = CurrentDifficulty.Medium;
            spawnDelay = 1f;
        }
        else if (gameTimer >= 90 && gameTimer < 120 && currentDifficulty != CurrentDifficulty.MediumHard)
        {
            currentDifficulty = CurrentDifficulty.MediumHard;
            spawnDelay = 0.5f;
        }
        else if (gameTimer >= 120 && gameTimer < 150 && currentDifficulty != CurrentDifficulty.Hard)
        {
            currentDifficulty = CurrentDifficulty.Hard;
        }
        else if (gameTimer >= 150 && gameTimer < 180 && currentDifficulty != CurrentDifficulty.VeryHard)
        {
            currentDifficulty = CurrentDifficulty.VeryHard;
        }
        else if (gameTimer >= 180 && gameTimer < 210 && currentDifficulty != CurrentDifficulty.Impossible)
        {
            currentDifficulty = CurrentDifficulty.Impossible;
        }
    }
}
