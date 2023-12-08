using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScorePanel : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI nicknameText;

    public void SetPanel(string _nickname, int _score)
    {
        scoreText.text = _score.ToString();
        nicknameText.text = _nickname;
    }
}
