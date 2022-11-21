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

    public TMP_Text title;
    public TMP_Text line1;
    public TMP_Text line2;
    public TMP_Text line3;
    public TMP_Text line4;
    public TMP_Text line5;

    public GameObject Quizpanel;
    public GameObject Gopanel;

    public GameObject homePanel;
    public GameObject InfoPanel;

    public GameObject cube;
    public GameObject cuboid;
    public GameObject sphere;
    int totalQuestions = 0;
    public int score = 0;

    public List<ObjectItem> ObjectItems = new List<ObjectItem>() {
                new ObjectItem(){  name="Cube" ,line1="Number of sides = 6", line2 = "Area of each side of the cube = a x a", line3 = "Total area of the Cube = 6 x a x a", line4 = "volume of the cube = a x a x a", line5 ="a : lenth of the side of a cube"},
                new ObjectItem(){  name="Cuboid" ,line1="Number of sides = 6", line2 = "Area of each side of the cuboid = l x b / l x h / h x b ", line3 = "Total area of the Cuboid = 2(l x b + l x h + h x b)", line4 = "volume of the cuboid = l x b x h", line5 ="l : length, b : bredth, h : height"},
                new ObjectItem(){  name="Sphere" ,line1="Number of sides = 0", line2 = "Area of sphere in 2D plane = Π x r x r", line3 = "Area of sphere in 3D plane = 4 x Π x r x r", line4 = "volume of the cube = 4/3 x Π x r x r x r", line5 ="r : Radius of the sphere"},
        };
    private void Start()
    {
        homePanel.SetActive(true);
        totalQuestions = QnA.Count;
        InfoPanel.SetActive(false);
        Gopanel.SetActive(false);
        cube.SetActive(false);
        cuboid.SetActive(false);
        sphere.SetActive(false);
        Quizpanel.SetActive(false);

    }

    public void startGame()
    {
        homePanel.SetActive(false);
        Quizpanel.SetActive(true);
        title.text = ObjectItems[currentQuestion].name;
        line1.text = ObjectItems[currentQuestion].line1;
        line2.text = ObjectItems[currentQuestion].line2;
        line3.text = ObjectItems[currentQuestion].line3;
        line4.text = ObjectItems[currentQuestion].line4;
        line5.text = ObjectItems[currentQuestion].line5;
        generateQuestion();
    }
    public void retry()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        currentQuestion = 0;
        score = 0;
        // Quizpanel.SetActive(true);
        Gopanel.SetActive(false);
        startGame();
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
        StartCoroutine(WaitForNext());
    }

    public void wrong()
    {
        StartCoroutine(WaitForNext());
    }
    IEnumerator WaitForNext()
    {
        yield return new WaitForSeconds(1);
        InfoPanel.SetActive(true);
    }
    public void next()
    {
        currentQuestion++;
        if (currentQuestion < QnA.Count)
        {
            Debug.Log(currentQuestion);
            title.text = ObjectItems[currentQuestion].name;
            line1.text = ObjectItems[currentQuestion].line1;
            line2.text = ObjectItems[currentQuestion].line2;
            line3.text = ObjectItems[currentQuestion].line3;
            line4.text = ObjectItems[currentQuestion].line4;
            line5.text = ObjectItems[currentQuestion].line5;
            InfoPanel.SetActive(false);
            generateQuestion();
        }
        else
        {
            InfoPanel.SetActive(false);
            sphere.SetActive(false);
            gameOver();
        }

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
                options[i].GetComponent<AnswerScript>().isCorrect = true;

            }
        }
    }

    void generateQuestion()
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
}
