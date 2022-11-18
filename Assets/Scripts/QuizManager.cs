using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class QuizManager : MonoBehaviour
{
    public List<QuestionsAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion;

    public Text QuestionTxt;
    public Text scoreTxt;
    public GameObject Quizpanel;
    public GameObject Gopanel;
    int totalQuestions = 0;
    public int score = 0;

    private void Start()
    {
        totalQuestions = QnA.Count;
        Gopanel.SetActive(false);
        generateQuestion();
    }

    public void retry(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void gameOver()
    {
        Quizpanel.SetActive(false);
        Gopanel.SetActive(true);
        scoreTxt.text = score + " / " + totalQuestions;
    }
    public void correct()
    {
        score++;
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }

    public void wrong(){
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }
    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = QnA[currentQuestion].Answers[i];

            if (QnA[currentQuestion].CorrectAnswer == i + 1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    void generateQuestion()
    {
        if (QnA.Count > 0)
        {
            currentQuestion = Random.Range(0, QnA.Count);
            QuestionTxt.text = QnA[currentQuestion].Question;
            SetAnswers();
        }
        else
        {
            gameOver();
        }

    }
}
