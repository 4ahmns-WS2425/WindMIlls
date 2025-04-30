using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorGoalScript : MonoBehaviour
{
    public Color _goalColor;
    [SerializeField] Color[] _colorsArray;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void SelectColorGoal(int a)
    {
        _goalColor = _colorsArray[a];
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    
}
