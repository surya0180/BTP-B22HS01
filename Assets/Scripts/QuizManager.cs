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
    public int currentQuestion = 0;

    public Text QuestionTxt;
    public Text scoreTxt;
    public GameObject Quizpanel;
    public GameObject Gopanel;

    public GameObject cube;
    public GameObject cuboid;
    public GameObject sphere;
    int totalQuestions = 0;
    public int score = 0;

    private void Start()
    {
        totalQuestions = QnA.Count;
        Gopanel.SetActive(false);
        cube.SetActive(false);
        cuboid.SetActive(false);
        sphere.SetActive(false);
        generateQuestion();
    }

    public void retry()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        currentQuestion = 0;
        score = 0;
        Quizpanel.SetActive(true);
        Gopanel.SetActive(false);
        generateQuestion();
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
        currentQuestion++;
        StartCoroutine(WaitForNext());
    }

    public void wrong()
    {
        currentQuestion++;
        StartCoroutine(WaitForNext());
    }
    IEnumerator WaitForNext()
    {
        yield return new WaitForSeconds(1);
        generateQuestion();
    }
    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = QnA[currentQuestion].Answers[i];
            // options[i].GetComponent<Image>().color = options[i].GetComponent<AnswerScript>().startColor;
            options[i].GetComponent<Image>().color = Color.white;
            if (QnA[currentQuestion].CorrectAnswer == i + 1)
            {
                Debug.Log("Changed color");
                options[i].GetComponent<AnswerScript>().isCorrect = true;
                
            }
        }
    }

    void generateQuestion()
    {

        if (currentQuestion < QnA.Count)
        {
            QuestionTxt.text = QnA[currentQuestion].Question;
            SetAnswers();
            if (currentQuestion == 0)
            {
                cube.SetActive(true);
            }
            else if (currentQuestion == 1)
            {
                cube.SetActive(false);
                cuboid.SetActive(true);
            }
            else
            {
                cuboid.SetActive(false);
                sphere.SetActive(true);
            }
        }
        else
        {
            sphere.SetActive(false);
            gameOver();
        }

    }
}
