using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//Scene관련 기능을 사용하기 위해 필요한 네임스페이스 선언하는 구문입니다.
using UnityEngine.SceneManagement;

//이미지가 하나씩 나타나기 위한 스크립트 입니다.
public class IntroManager : MonoBehaviour
{   //SerializeField는 private 속성을 Inspector에서 변수를 직렬화 하여 접근 가능하게 해줍니다.
    //스크립트를 변경하지 않고도 유니티 에디터 내 Inspector창에서 실시간으로 변수 값을 조정할 수 있습니다.
    //4장의 이미지를 변경하기 위해 [SerializeField] 클래스를 사용하였습니다.
    //변수는 Image타입으로 선언되었고, 초기값으로 null이 할당되었습니다. 이 변수는 Inspector창에서 변수 값을 설정할 수 있습니다. 
    [SerializeField] Image image1 = null;
    [SerializeField] Image image2 = null;
    [SerializeField] Image image3 = null;
    [SerializeField] Image image4 = null;

    //실행되고 있는 동안 처음 한 번만 호출
    void Start()
    {
        //코루틴 실행 방법은 일반 함수와 달리, StartCoroutine()메소드를 이용하여 괄호안에 코루틴 이름을 넣어주어야 합니다.
        //이미지를 순차적으로 표시한다는 ShowImagesSequentially으로 코루틴 이름을 설정했습니다.
        //코루틴 실행은 '0.3f'라는 지연 시간(초)이후에 실행됩니다.
        //image1, image2, image3, image4는 이미지를 참조하는 유니티의 'Image' 컴포넌트 변수들입니다.
        StartCoroutine(ShowImagesSequentially(0.3f, image1, image2, image3, image4));
    }

    //코루틴 선언은 반환형을 IEnumerator로 설정 합니다.
    //일반적인 함수와 달리 반환시 return이 아닌,  yield return 으로 돌려주어야 합니다.
    /* yield return 뒤에는 반환 시간을 명시해 주는데, 명시한 반환 시간 만큼 코드 동작을 중지 시키고,
     * 시간이 지나면 다음 줄 부터 다시 코루틴이 동작합니다.*/
    //이미지를 순차적으로 표시하는 기능을 구현하기 위해 매개변수 'duration'을 이용해 각 이미지가 표시되는 시간(초)를 나타냅니다.
    //가변인자 매개변수'params'사용하면 개수의 제한 없이 매개변수를 넘길 수 있습니다.
    //params를 통해 여러 개의 'Image' 변수를 전달 할 수 있습니다.
    public IEnumerator ShowImagesSequentially(float duration, params Image[] images)
    {
        //'foreach'사용하여 'images'배열에 있는 각 요소를 'Image'변수 'imgae'에 대입하여 순회하는 반복문입니다.
        foreach (Image image in images)
        {
            //반복문 내부 : 'image'변수의 'color'속성을 변경하고 있습니다.
            //현재 색상의 RGB구성요소 ('r' , 'g' , 'b')를 그대로 사용하되, 알파 값('a')을 0으로 설정하여 'new Color'새로운 'Color'객체를 생성합니다.
            //이미지의 알파값이 0인 완전히 투명한 색상으로 설정됩니다.
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        }

        //배열 'images'에 있는 모든 이미지의 페이드 인 효과를 구현하는 반복문입니다.
        //먼저 'for'반복문에서 'i'변수를 사용하여 배열'images'의 각 요소에 접근합니다.
        for (int i = 0; i < images.Length; i++)
        {
            //현재 요소에 해당하는 'Image' 컴포넌트를 'image'변수에 할당합니다.
            Image image = images[i];
            //'t'변수를 0으로 초기화 합니다. 
            //이 부분은 페이드 인 안의 시간에 따라 진행 상태를 업데이트 하는 것을 의미합니다.
            float t = 0;

            //while반복문은 't'가 1초보다 작을 때 까지 실행됩니다.
            while (t < 1.0f)
            {
                //'t' 값은 'Time.deltaTime / duration'만큼 증가합니다.
                //Time.deltaTime을 이용하면, PC의 성능과는 무관하게 동등한 조건이 되게 되므로 똑같은 결과 값을 내기위해 사용하였습니다.
                //Time.deltaTime은 프레임간의 시간 간격으로, 프레임 속도와 상관없이 일정한 간격으로 업데이트 됩니다.
                //duration은 페이드 인의 전체시간을 나타내는 변수입니다. 즉 페이드 인이 완료될때 까지 걸리는 시간입니다.
                //'t'를 업데이트함으로써 페이드 인의 진행 상태를 제어하고, 알파 채널 값을 조절하여 이미지 투명도를 조절합니다.
                //'t'가 1에 도달하면 페이드 인이 완료되는 것을 의미합니다.
                t += Time.deltaTime / duration;
                //'image.color'를 사용하여 이미지의 알파 채널 값을 't'로 업데이트 합니다.
                //이미지의 투명도가 점차 증가하여 페이드인 효과를 나타냅니다.
                image.color = new Color(image.color.r, image.color.g, image.color.b, t);
                //null : 1frame 만큼 코드 동작을 중지 시킵니다.
                //이 부분을 통해 페이드 인 효과가 부드럽게 진행되도록 합니다.
                yield return null;
            }
            //new WaitForSeconds(1f) : 1초 만큼 동작을 중지 시킵니다.
            //이 부분을 통해 각 이미지의 페이드 인이 완료된 후 1초동안 대기 하는 역할을 합니다.
            yield return new WaitForSeconds(1f);

            //다음 페이드 인을 준비하는 부분입니다.
            t = 1.0f;
        }
    }
}
