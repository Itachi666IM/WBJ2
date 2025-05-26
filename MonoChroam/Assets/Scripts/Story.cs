using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Story : MonoBehaviour
{
    public TMP_Text storyText;
    public string[] storyTextsToWrite;
    int index = 0;
    string currentStoryText;
    public float typingSpeed;

    public GameObject continueButton;
    public GameObject startGameButton;

    public Animator anim;

    private void Start()
    {
        currentStoryText = storyTextsToWrite[index];
        StartCoroutine(WriteSentence());
    }
    private void Update()
    {
        if (storyText.text == currentStoryText)
        {
            continueButton.SetActive(true);
        }
    }
    IEnumerator WriteSentence()
    {
        foreach (char c in currentStoryText.ToCharArray())
        {
            storyText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);
        anim.SetTrigger("change");
        if (index < storyTextsToWrite.Length - 1)
        {
            index++;
            storyText.text = "";
            currentStoryText = storyTextsToWrite[index];
            StartCoroutine(WriteSentence());
        }
        else
        {
            storyText.text = "Solve all puzzles to win!";
            continueButton.SetActive(false);
            startGameButton.SetActive(true);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Level 1");
    }
}
