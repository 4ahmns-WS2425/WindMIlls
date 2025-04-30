using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndingScript : MonoBehaviour
{
    public WindmillManager wma;
    public ColorGoalScript cgsa;
    [SerializeField] GameObject goalSphere;
    [SerializeField] GameObject achievedSphere;
    [SerializeField] TMP_Text procentageText;


    void Start()
    {
        wma = FindObjectOfType<WindmillManager>();
        cgsa = FindObjectOfType<ColorGoalScript>();
        float similarity = GetColorSimilarityPercentage(cgsa._goalColor, wma.windmillColor);
        goalSphere.GetComponent<Renderer>().material.color = cgsa._goalColor;
        achievedSphere.GetComponent<Renderer>().material.color = wma.windmillColor;
        procentageText.text = similarity + "%";
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            Destroy(GameObject.Find("GameManager"));
            Destroy(GameObject.Find("Manager"));
            SceneManager.LoadScene(0);
        }
    }

    float GetColorSimilarityPercentage(Color a, Color b)
    {
        float rDiff = a.r - b.r;
        float gDiff = a.g - b.g;
        float bDiff = a.b - b.b;

        float distance = Mathf.Sqrt(rDiff * rDiff + gDiff * gDiff + bDiff * bDiff);
        float similarity = 1f - (distance / Mathf.Sqrt(3f));

        return Mathf.Clamp((float)System.Math.Round(similarity * 100f, 2), 0f, 100f);

    }
}
