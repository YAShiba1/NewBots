using TMPro;
using UnityEngine;

public class ScoreViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text _score;

    private float _scoreValue;

    public void SetScore(float value)
    {
        _scoreValue = value;
        _score.text = _scoreValue.ToString("F0");
    }
}
