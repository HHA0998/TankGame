using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileIO : MonoBehaviour
{
    // Start is called before the first frame update

    public int[] scores = new int[10];

    string currentDirectory;

    public string scoreFileName = "highscores.txt";

    void Start()
    {
        currentDirectory = Application.dataPath;
        Debug.Log("Directoryl: " + currentDirectory);

        LoadScoreFromFile();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScoreFromFile()
    {
        bool fileExists = File.Exists(currentDirectory + "\\" + scoreFileName);
        if (fileExists == true)
        {
            Debug.Log("Found high score file " + scoreFileName);
        }
        else
        {
            Debug.Log("The file " + scoreFileName +
            " does not exist. No scores will be loaded.", this);
            return;
        }

        scores = new int[scores.Length];

        StreamReader fileReader = new StreamReader(currentDirectory +
        "\\" + scoreFileName);
        int scoreCount = 0;

        while (fileReader.Peek() != 0 && scoreCount < scores.Length)
        {
            string fileLine = fileReader.ReadLine();
            int readScore = -1;
            // Try to parse it
            bool didParse = int.TryParse(fileLine, out readScore);
            if (didParse)
            {
                scores[scoreCount] = readScore;
            }
            else
            {

                Debug.Log("Invalid line in scores file at " + scoreCount +
                ", using default value.", this);
                scores[scoreCount] = 0;
            }
            scoreCount++;
        }
        fileReader.Close();
        Debug.Log("High scores read from " + scoreFileName);
    }


    public void SaveScoreToFile()
    {
        StreamWriter fileWriter = new StreamWriter(currentDirectory + "\\" + scoreFileName);

        for (int i = 0; i < scores.Length; i++)
        {
            fileWriter.WriteLine(scores[i]);
        }
        fileWriter.Close();

        Debug.Log("High Scored written to " + scoreFileName);
    }

    public void AddScore(int newScore)
    {
        int desitedIndex = -1;
        for (int i = 0; i < scores.Length; i++)
        {
            if (scores[i] > newScore || scores[i] == 0)
            {
                desitedIndex = i;
                break;
            }
        }

        if(desitedIndex < 0)
        {
            Debug.Log("Score of " + newScore + " Not high enough for high scores list.", this);
            return;
        }
        for (int i = scores.Length; i < desitedIndex; i--)
        {
            scores[i] = scores[i - 1];
        }
        scores[desitedIndex] = newScore;
        Debug.Log("score of " + newScore + "enterd into new high score at position " + desitedIndex, this);
    }
}
