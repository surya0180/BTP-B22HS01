using UnityEngine;

[System.Serializable]

public class QuestionsAndAnswers 
{
    // Start is called before the first frame update
    public string Question;
    public string[] Answers;
    public int CorrectAnswer;

    public string title;
    public GameObject firstPrefab;
    public GameObject infoObject;

    public string[] lines;
}
