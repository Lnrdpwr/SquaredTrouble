using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelResultingPanel : MonoBehaviour
{
    [SerializeField] private GameObject _resultingPanel;
    [SerializeField] private TMP_Text _resultText, _bestResultText;

    public void ShowPanel(int currentResult)
    {
        _resultingPanel.SetActive(true);
        int bestResult = PlayerPrefs.GetInt("BestResult", 0);
        if(currentResult > bestResult)
        {
            PlayerPrefs.SetInt("BestResult", currentResult);
            bestResult = currentResult;
        }
        _resultText.text = "�� �������: " + currentResult.ToString() + " ������";
        _bestResultText.text = "������ ���������: " + bestResult.ToString() + " ������";
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Arena");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
