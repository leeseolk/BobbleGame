using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Scene관련 기능을 사용하기 위해 필요한 네임스페이스 선언하는 구문입니다.
using UnityEngine.SceneManagement;
//UI시스템에 관련된 클래스를 포함하고 있습니다. UI요소들(버튼,텍스트,이미지 등)을 조작하고 이벤트 처리하는 등의 작업을 수행할 수 있습니다.
using UnityEngine.UI;

/* 게임 마스터는 이 게임 안에서의 유일한 싱글톤 입니다.
 * 이 게임 전체를 컨트롤 하고 마스터 하고, 씬 로드시 데이터가 파괴되지 않고 유지됩니다.
 * 즉 어디서든 싱글톤 클래스와 기능에 접근할 수 있는 경로를 하나로 통합해서 제공할 수 있습니다.
 다만 인스턴스화 시점을 정확하게 통제하기 어렵다는 단점이 있습니다.*/

public class GameMaster : MonoBehaviour
{
    //'GameMaster'클래스의 정적 변수 'instance'를 선언하고 초기값을 'null'로 설정합니다.
    // 다른 스크립트에서 이 변수를 사용하여 게임 마스터에 접근할 수 있습니다.
    public static GameMaster instance = null;
    //'int'형 변수 'Score'를 선언하며 게임의 현재 점수를 저장하는데 사용됩니다.
    public int Score;
    //int'형 변수 'HighScore'를 선언하며 게임의 최고 점수를 저장하는데 사용됩니다.
    public int HighScore;
    //int'형 변수 'Credit'를 선언하고 초기값을 3으로 설정합니다.
    //플레이어의 게임 크레딧(생명)을 나타냅니다.
    public int Credit = 3;
    //'GameObject'형 변수 'PlayerPrefab'을 선언합니다.(사전에 제작한 게임 오브젝트 템플릿)
    public GameObject PlayerPrefab;
    //'GameObject'형 변수 'Credit1,2'를 선언합니다.
    //크레딧 UI표시를 위한 게임 오브젝트를 나타냅니다.
    public GameObject Credit1;
    public GameObject Credit2;
    // 'GameObject'형 변수 InvaderPrefab을 선언해 적의 프리팹을 나타냅니다.
    public GameObject InvaderPrefab;
    public GameObject InvaderPrefab1;
    //'Text'형 변수 'ScoreText'를 선언합니다. 점수를 표시하기 위한 UI텍스트를 나타냅니다.
    public Text ScoreText;
    //'Text'형 변수 'HighScoreText'를 선언합니다. 최고 점수를 표시하기 위한 UI텍스트를 나타냅니다.
    public Text HighScoreText;
    //'Canvas'형 변수 'ScoreCanvas'를 선언합니다. 점수와 관련된 UI요소들을 담고 있는 캔버스를 나타냅니다.
    public Canvas ScoreCanvas;
    //'bool'형 변수 'stage2Unlocked'를 선언하고 초기값을 'false'로 설정합니다.
    //이 변수는 게임의 두 번째 스테이지가 잠금 해제되었는지 여부를 나타냅니다.
    private bool stage2Unlocked = false;
    //'int'형 변수 'stage1Score'를 선언합니다. 첫 번째 스테이지의 점수를 저장하는데 사용됩니다.
    private int stage1Score;
    //'bool'형 변수 'showScoreText'를 선언하고, 초기값을 'false'로 설정합니다.
    //이 변수는 텍스트를 표시할지 여부를 나타냅니다.
    private bool showScoreText = false; 

    //'Awake()'메서드는 스크립트가 활성화될 때 호출되는 함수입니다.
    //게임 마스터의 인스턴스를 생성하고, 다른 씬으로 전환되더라도 게임 오브젝트가 파괴되지 않도록 유지하는 역할을 합니다.
    //다른 씬으로 전환되더라도 게임 마스터는 파괴되지 않고 계속해서 접근할 수 있습니다.
    void Awake()
    {
        //게임 마스터의 인스턴스가 존재하지 않는 경우를 확인합니다. 
        //처음으로 게임 마스터를 생성할 때만 이 조건이 참이 됩니다.
        if (instance == null)
        {
            //게임 마스터의 인스턴스에 자기 자신을 할당합니다.
            //이렇게 함으로써 다른 스크립트에서 'GameMaster.instance'를 통해 게임 마스터에 접근할 수 있습니다.
            instance = this;
            //현재 게임 오브젝트를 씬 전환 시에도 파괴되지 않도록 설정합니다.
            //이를 통해 게임 마스터는 게임의 여러 씬에서 유지되며, 다른 씬에서도 접근할 수 있습니다.
            DontDestroyOnLoad(gameObject);
        }
        //게임 마스터의 인스턴스가 이미 존재하는 경우를 처리합니다.
        else
        {
            //이미 존재하는 게임 마스터의 인스턴스가 현재 인스턴스와 같지 않은 경우를 확인합니다.
            //이 조건이 참이면 이미 다른 게임 마스터의 인스턴스가 씬에 존재하는 것을 의미합니다.
            if (instance != this)
                //현재 게임 오브젝트를 파괴합니다.
                //이미 다른 게임 마스터 인스턴스가 씬에 존재하기 때문에 현재 인스턴스는 파괴되어야 합니다.
                Destroy(gameObject);
        }
    }

    //게임 오브젝트가 활성화된 후에 한 번 호출되는 함수입니다.
    //이 함수는 게임을 초기화하고 게임 상태를 리셋하는 역할을 합니다.
    void Start()
    {
        ResetGame();   
    }

    //필요한 변수들을 초기화하고 점수와 최고 점수를 업데이트 합니다.
    //첫 번째 스테이지로 돌아올 때 최고 점수를 리셋합니다.
    public void ResetGame()
    {
        //'ScoreCanvas'의 'enabled' 속성을 'true'로 설정하여 점수를 표시하는 캔버스를 활성화 합니다.
        ScoreCanvas.enabled = true;
        //"Stage1"이라는 씬을 로드합니다.
        //게임을 처음부터 시작하는 경우, 첫 번재 스테이지인 "Stage1"씬이 로드됩니다.
        SceneManager.LoadScene("Stage1");
        //플레이어의 크레딧(생명)을 초기화 하고 'Credit'변수에 3을 할당합니다.
        Credit = 3;
        //게임의 현재 점수를 초기화 하고 'Score'변수에 0을 할당합니다.
        Score = 0;
        //첫 번째 스테이지 점수를 초기화 하고 'stage1Score' 변수에 0을 할당합니다.
        stage1Score = 0;
        //저장된 최고 점수를 로드합니다.
        LoadHighScore();
        //'StartSpawnInvader()'함수를 호출하여 적을 생성하고, 일정한 간격으로 적이 생성되는 스폰 기능을 활성화 합니다.
        StartSpawnInvader();
        //게임 시작 시 초기 점수가 표시되지 않도록 설정합니다.
        UpdateScoreText(); 
        //화면에 표시되는 최고 점수를 갱신합니다.
        UpdateHighScoreText();
        //"Stage1"씬으로 돌아왔을 때 최고 점수를 초기화 합니다.
        UnlockStage1(); 
    }

    /*'UnlockStage1()'함수는 현재 플레이 중인 씬이 Stage1씬인지를 확인하고, 동작을 수행합니다.
      그에 따라 최고 점수를 리셋하고 저장하며, 점수 텍스트를 표시할지 여부를 결정합니다.
      Stage1씬에서는 최고 점수를 초기화 하고 표시하며, 다른 씬에서는 점수 텍스트를 숨깁니다.*/
    private void UnlockStage1()
    {
        //현재 플레이 중인 씬이 Stage1씬인지를 판단하기 위한 조건문입니다.
        //현재 활성화된 씬의 이름이 "Stage1"인지 확인합니다.
        if (SceneManager.GetActiveScene().name == "Stage1")
        {
            //Stage1씬으로 돌아왔을 대 최고 점수를 0으로 초기화하고, 'HighScore'변수에 0을 할당하고 최고 점수를 리셋합니다.
            HighScore = 0; 
            //최고 점수를 저장합니다.
            //최고 점수를 업데이트 한 후에 저장해야 다음 게임을 시작할 때에도 해당 점수가 유지됩니다.
            SaveHighScore();
            //"HIGH SCORE : "와 'HighScore' 값을 조합하여 최고 점수를 나타냅니다.
            //'HighScoreText'의 'text' 속성을 업데이트하여 화면에 표시되는 최고 점수를 갱신합니다.
            HighScoreText.text = "HIGH SCORE : " + HighScore;
            //Stage1씬에서는 점수 텍스트를 보여주어야 하므로 변수 값을 'true'로 설정합니다.
            showScoreText = true;
        }
        //현재 플레이 중인 씬이 Stage1씬이 아닌 다른 씬인 경우입니다.
        else
        {
            //Stage1씬이 아닌 다른 씬에서는 점수 텍스트를 표시하지 않아야 하므로 변수 값을 'false'로 설정합니다.
            //즉 다른 씬에서는 점수 텍스트를 숨깁니다.
            showScoreText = false; 
        }
    }

    //'StartSpawnInvader()'함수는 게임 시작 후 일정한 간격으로 'SpawnInvader'와 'SpawnInvader1'메서드를 호출하여 적을 생성합니다.
    //적이 주기적으로 게임 화면에 등장하게 됩니다.
    public void StartSpawnInvader()
    {
        /* Invoke함수는 지연 시간 후 한번만 실행되는데,
         * 여러번 반복 실행을 위해서는 InvokeRepeating함수를 사용해서 지정한 주기로 반복할 수 있습니다.
         * InvokeRepeating(함수명, 지연시간, 반복주기); 지연 시간만큼 지연된 후 반복주기 만큼 계속 반복합니다.*/
        //게임이 시작된 후 1초 후 부터 1.5초마다 'SpawnInvader'메서드가 호출됩니다.
        InvokeRepeating("SpawnInvader", 1f, 1.5f);
        //게임이 시작된 후 2초 후 부터 2.5초마다 'SpawnInvader1'메서드가 호출됩니다.
        InvokeRepeating("SpawnInvader1", 2f, 2.5f);
    }

    //적을 생성하는 기능을 구현한 메서드입니다.
    //미리 정의된 프리팹을 'pos'위치에 생성하여 등장 시킵니다.(매번 다른 위치에 생성)
    private void SpawnInvader()
    {
        //'Vector3' 객체를 생성하여 'pos' 변수에 할당합니다.
        //생성된 'Vector3' 객체는 적이 생성될 위치를 나타냅니다.
        //x좌표는 11, y좌표는 -3부터 5 사이의 임의의 값을 가지며, z 좌표는 0 입니다.
        Vector3 pos = new Vector3(11f, Random.Range(-3f, 5), 0);
        //'InvaderPrefab'은 미리 생성된 적의 프리팹입니다.
        //'pos'는 적이 생성될 위치를 나타내는 변수입니다.
        //'Quaternion.identity'는 회전 없이 적을 생성함을 의미합니다.
        Instantiate(InvaderPrefab, pos, Quaternion.identity);
    }

    //'SpawnInvader1()'함수는 "Stage2"장면에서만 사용되며 'pos'위치에 생성하여 등장시킵니다.
    //Stage2에서는 Stage1과 다른 적을 생성합니다.
    private void SpawnInvader1()
    {
        //조건문을 사용하여 현재 장면이 "Stage2"인지 확인하고 적을 생성합니다.
        if (SceneManager.GetActiveScene().name == "Stage2")
        {
            //'Vector3' 객체를 생성하여 'pos' 변수에 할당합니다.
            //생성된 'Vector3' 객체는 적이 생성될 위치를 나타냅니다.
            //x좌표는 11, y좌표는 -3부터 5 사이의 임의의 값을 가지며, z 좌표는 0 입니다.
            Vector3 pos = new Vector3(11f, Random.Range(-3f, 5), 0);
            //'InvaderPrefab1'은 미리 생성된 Stage2용 적의 프리팹입니다.
            Instantiate(InvaderPrefab1, pos, Quaternion.identity);
        }
    }

    //'OnPlayerDestroy()'함수는 플레이어 파괴 시 크레딧 감소, UI 업데이트, 적 제거, 플레이어 재생성, 게임 오버 전환 등의 로직을 처리합니다.
    public void OnPlayerDestroy()
    {
        //'Credit'변수의 값을 1 감소시킵니다. 이는 플레이어의 크레딧(생명)을 나타냅니다.
        Credit--;
        //"Credit1"이름을 가진 게임 오브젝트를 찾아서 'Credit1' 변수에 할당합니다.
        Credit1 = GameObject.Find("Credit1");
        //"Credit2"이름을 가진 게임 오브젝트를 찾아서 'Credit2' 변수에 할당합니다.
        Credit2 = GameObject.Find("Credit2");
        //'Credit'변수가 3인 경우를 확인합니다. 즉 플레이어의 크레딧이 3인 경우입니다.
        if (Credit == 3)
        {
            //이 경우, 'Credit1'과'Credit2' 게임 오브젝트를 활성화 합니다.
            Credit1.SetActive(true);
            Credit2.SetActive(true);
        }
        //'Credit'변수가 2인 경우를 확인합니다. 즉 플레이어의 크레딧이 2인 경우입니다.
        else if (Credit == 2)
        {
            //이 경우, 'Credit1'게임 오브젝트를 비활성화하고, 'Credit2' 게임 오브젝트를 활성화 합니다.
            Credit1.SetActive(false);
            Credit2.SetActive(true);
        }
        //'Credit'변수가 1인 경우를 확인합니다. 즉 플레이어의 크레딧이 1인 경우입니다.
        else if (Credit == 1)
        {
            //이 경우, 'Credit2'게임 오브젝트를 비활성화 합니다.
            Credit2.SetActive(false);
        }
        /* FindGameObjectsWithTag는 게임 상의 같은 태그를 가진 오브젝트를 배열로 가져오고 싶을 때 사용합니다.
         * 태그는 사용전에 tag manager에 선언되어 져야 합니다.*/
        //"Invader"태그를 가진 모든 게임 오브젝트를 찾아서 'invaders'배열에 할당합니다.
        GameObject[] invaders = GameObject.FindGameObjectsWithTag("Invader");
        //'invaders'배열의 각 요소를 'i'변수에 할당하여 반복합니다.
        foreach (GameObject i in invaders)
        {
            //현재 'i'에 할당된 게임 오브젝트를 제거합니다.
            //적 오브젝트를 제거하여 화면에서 사라지게 됩니다.
            Destroy(i);
        }
        //'Credit'변수가 0보다 큰 경우를 확인합니다. 즉 플레이어의 크레딧이 0보다 큰 경우입니다.
        if (Credit > 0)
        {
            //플레이어 프리팹을(-3f,0,0)위치에 회전을(0,90,0)으로 설정하여 생성합니다.
            Instantiate(PlayerPrefab, new Vector3(-3f, 0, 0), Quaternion.Euler(0, 90, 0));
        }
        //'Credit'변수가 0이하인 경우를 확인합니다. 즉 플레이어의 크레딧이 0이하인 경우입니다.
        else
        {
            //현재 획득한 최고 점수를 저장합니다. 이는 게임 오버시 현재까지의 최고점수를 유지하기 위한 작업입니다.
            SaveHighScore();
            //1.5초후에 'GotoGameOver'함수를 호출하여 게임 오버 상태로 전환합니다.
            Invoke("GotoGameOver", 1.5f);
            //'SpawnInvader'메서드의 반복 호출을 취소하여 적이 생성되지 않도록 합니다.
            //'CancelInvoke'함수는 특정한 메서드 호출을 취소하는 기능을 제공합니다.
            CancelInvoke("SpawnInvader");
        }
    }

    //'AddScore'메서드는 점수를 증가시키고 스테이지 1의 점수를 갱신합니다.
    //잠금 해제를 확인하여 스테이지2를 잠금 해제하고 전환하는 역할을 수행합니다.
    public void AddScore()
    {
        //플레이어가 적을 제거할때마다 점수가 10점씩 증가합니다.
        Score += 10;
        //스테이지1 점수에 10을 더합니다.
        //스테이지1 에서의 플레이어의 총 점수를 나타내는 변수입니다.
        stage1Score += 10;
        //점수를 텍스트로 업데이트하여 UI에 표시되는 점수를 갱신하는 역할을 합니다.
        UpdateScoreText();
        //현재 점수를 확인하고, 특정 점수에 도달하면 성공 화면으로 전환합니다.
        //플레이어가 스테이지1 목표점수 80점에 도달하였을 때 게임 상태를 변경하는 역할을 합니다.
        UnlockSuccess();
        //스테이지 2가 아직 잠금 해제되지 않은 상태이고, 스테이지 1의 점수가 80이상인지 확인합니다.
        if (!stage2Unlocked && stage1Score >= 30)
        {
            //스테이지2의 잠금 해제 상태를 'true'로 설정합니다.
            //스테이지1의 목표점수 80점을 충족하여 스테이지2를 잠금 해제하는 역할을 합니다.
            stage2Unlocked = true;
            //스테이지2를 잠금 해제하고 해당 스테이지로 전환합니다.
            //스테이지2의 초기화 및 전환작업을 수행하는 메서드입니다.
            UnlockStage2();
        }
    }

    //'UpdateScoreText()'메서드는 현재 점수를 텍스트로 업데이트하는 역할을 합니다.
    public void UpdateScoreText()
    {
        // Stage1 씬에서만 점수 텍스트를 표시합니다.
        //'showScoreText'변수가 'true'인 경우에만 실행됩니다.
        if (showScoreText)
            //'ScoreText'객체의 'text'속성을 현재 점수 값과 함께 설정하여 텍스트를 갱신합니다.
            ScoreText.text = "SCORE : " + Score;
    }

    //'UpdateHighScoreText()'메서드는 최고 점수를 텍스트로 업데이트 해야하는 역할을 합니다.
    public void UpdateHighScoreText()
    {
        //'HighScoreText'객체의 'text'속성을 최고 점수 값과 함께 설정하여 텍스트를 갱신합니다.
        HighScoreText.text = "HIGH SCORE : " + HighScore;
    }

    //'GotoGameOver()'메서드는 게임 오버 상태로 전환하는 역할을 합니다.
    private void GotoGameOver()
    {
        //'ScoreCanvas'의 'enabled'속성을 'false'로 설정하여 점수를 숨깁니다.
        ScoreCanvas.enabled = false;
        //"GameOver"씬을 로드하여 게임 오버 화면을 표시합니다.
        SceneManager.LoadScene("GameOver");
    }

    //'SaveHighScore()'메서드는 현재 점수를 최고 점수를 저장하는 역할을 합니다.
    private void SaveHighScore()
    {
        //현재 점수가 최고 점수보다 높은 경우에만 실행됩니다.
        if (Score > HighScore)
        {
            //최고점수를 현재 점수로 업데이트 합니다.
            HighScore = Score;
            //PlayerPrefs.SetInt()메서드를 사용하여 "HighScore"라는 키로 최고 점수를 저장합니다.
            PlayerPrefs.SetInt("HighScore", HighScore);
            //'PlayerPrefs.Save()'메서드를 호출하여 변경된 설정을 저장합니다.
            //이를 통해 최고점수가 영구적으로 저장됩니다.
            PlayerPrefs.Save();
        }
    }

    //'LoadHighScore()'이 메서드는 저장된 최고 점수를 불러오는 역할을 합니다.
    private void LoadHighScore()
    {
        //"HighScore"라는 키로 저장된 값을 'PlayerPrefs'에서 불러와 'HighScore'변수에 할당합니다.
        //기본값으로 0을 설정하여 저장된 값이 없는 경우에는 0으로 초기화 합니다.
        HighScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    //'UnlockStage2()'메서드는 스테이지2로 이동할 때 호출되는데,최고점수 80을 설정하고 저장합니다.
    //최고 점수를 화면에 표시한 후에 Stage2씬으로 전환됩니다.
    private void UnlockStage2()
    {
        //스테이지 2로 이동할 때 최고 점수를 80으로 설정합니다.
        HighScore = 30;
        //최고 점수를 저장합니다.
        SaveHighScore();
        //'HighScoreText'라는 텍스트 UI요소의 'text'속성을 업데이트 하여 최고 점수를 화면에 표시합니다.
        HighScoreText.text = "HIGH SCORE : " + HighScore;
        //Stage2 씬으로 전환합니다.
        SceneManager.LoadScene("Stage2");
    }

    //'UnlockSuccess()'메서드는 플레이어가 스테이지1 또는 스테이지2씬에서 특정 점수인 200점 이상을 달성했을 때 호출되며, 성공 화면으로 전환하는 역할을 합니다.
    private void UnlockSuccess()
    {
        //현재 활성화된 씬이 "Stage1"또는 "Stage2"인지 확인하는 조건문입니다.
        if (SceneManager.GetActiveScene().name == "Stage2")
        {
            //점수가 200이상인지 확인하는 조건문입니다.
            if (Score >= 100)
            {
                //'HighScore'변수를 200으로 설정합니다.
                HighScore = 100;
                //현재 최고 점수를 저장하는 'SaveHighScore()'메서드를 호출합니다.
                SaveHighScore();
                //"Success"씬으로 전환합니다.
                //이는 플레이어가 200점 이상을 획득하여 성공화면으로 이동하는 역할을 합니다.
                SceneManager.LoadScene("Success");
            }
        }
    }
}