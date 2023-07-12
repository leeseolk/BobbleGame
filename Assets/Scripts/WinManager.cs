using UnityEngine;
//이 네임스페이스는 Unity의 UI 요소들을 조작하고 이벤트 처리하는 등의 작업을 수행할 수 있게 합니다.
using UnityEngine.UI;

public class WinManager : MonoBehaviour
{
    //'Text' 타입의 멤버 변수 'HighScoreText'를 선언하여 UI텍스트 요소를 참조하는데 사용됩니다.
    public Text HighScoreText;
    //이 변수는 최고 점수를 저장하는데 사용됩니다.
    private int highScore = 100;

    void Start()
    {
        // 게임마스터 스크립트 인스턴스를 검색하여 제거
        // FindObjectOfType 함수를 통해'GameMaster'타입의 인스턴트를 검색하는 메서드입니다.
        //'GameMaster'스크립트를 가지고 있는 게임 오브젝트를 찾아 그 인스턴스를 'gameMaster'변수에 할당합니다.
        GameMaster gameMaster = FindObjectOfType<GameMaster>();
        //'gameMaster'변수가 'null'이 아닌 경우, 해당 인스턴스의 게임 오브젝트를 제거합니다.
        //이를 통해 'GameMaster'스크립트의인스턴스가 중복 되어 생성되는 것을 방지합니다.
        if (gameMaster != null)
        {
            //게임 오브젝트 제거
            Destroy(gameMaster.gameObject);
        }
        // 3.5초 후에 ShowHighScore 함수를 3.5초후에 호출합니다.
        //이 함수는 최고 점수를 UI텍스트 요소에 표시하는 역할을 합니다.
        Invoke("ShowHighScore", 3.5f); 
    }
    
    //최고 점수를 UI텍스트 요소에 표시하는 역할을 합니다.
    //private으로 선언되어 있으므로 클래스 내부에서만 접근합니다.
    private void ShowHighScore()
    {
        // "HIGH SCORE: " 문자열과 highScore 변수의 값을 문자열로 변환한 후 합친 결과를 할당합니다.
        HighScoreText.text = "HIGH SCORE: " + highScore.ToString();
    }
}
