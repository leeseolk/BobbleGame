using UnityEngine;
//�� ���ӽ����̽��� Unity�� UI ��ҵ��� �����ϰ� �̺�Ʈ ó���ϴ� ���� �۾��� ������ �� �ְ� �մϴ�.
using UnityEngine.UI;

public class WinManager : MonoBehaviour
{
    //'Text' Ÿ���� ��� ���� 'HighScoreText'�� �����Ͽ� UI�ؽ�Ʈ ��Ҹ� �����ϴµ� ���˴ϴ�.
    public Text HighScoreText;
    //�� ������ �ְ� ������ �����ϴµ� ���˴ϴ�.
    private int highScore = 100;

    void Start()
    {
        // ���Ӹ����� ��ũ��Ʈ �ν��Ͻ��� �˻��Ͽ� ����
        // FindObjectOfType �Լ��� ����'GameMaster'Ÿ���� �ν���Ʈ�� �˻��ϴ� �޼����Դϴ�.
        //'GameMaster'��ũ��Ʈ�� ������ �ִ� ���� ������Ʈ�� ã�� �� �ν��Ͻ��� 'gameMaster'������ �Ҵ��մϴ�.
        GameMaster gameMaster = FindObjectOfType<GameMaster>();
        //'gameMaster'������ 'null'�� �ƴ� ���, �ش� �ν��Ͻ��� ���� ������Ʈ�� �����մϴ�.
        //�̸� ���� 'GameMaster'��ũ��Ʈ���ν��Ͻ��� �ߺ� �Ǿ� �����Ǵ� ���� �����մϴ�.
        if (gameMaster != null)
        {
            //���� ������Ʈ ����
            Destroy(gameMaster.gameObject);
        }
        // 3.5�� �Ŀ� ShowHighScore �Լ��� 3.5���Ŀ� ȣ���մϴ�.
        //�� �Լ��� �ְ� ������ UI�ؽ�Ʈ ��ҿ� ǥ���ϴ� ������ �մϴ�.
        Invoke("ShowHighScore", 3.5f); 
    }
    
    //�ְ� ������ UI�ؽ�Ʈ ��ҿ� ǥ���ϴ� ������ �մϴ�.
    //private���� ����Ǿ� �����Ƿ� Ŭ���� ���ο����� �����մϴ�.
    private void ShowHighScore()
    {
        // "HIGH SCORE: " ���ڿ��� highScore ������ ���� ���ڿ��� ��ȯ�� �� ��ģ ����� �Ҵ��մϴ�.
        HighScoreText.text = "HIGH SCORE: " + highScore.ToString();
    }
}
