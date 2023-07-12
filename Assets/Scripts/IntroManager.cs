using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//Scene���� ����� ����ϱ� ���� �ʿ��� ���ӽ����̽� �����ϴ� �����Դϴ�.
using UnityEngine.SceneManagement;

//�̹����� �ϳ��� ��Ÿ���� ���� ��ũ��Ʈ �Դϴ�.
public class IntroManager : MonoBehaviour
{   //SerializeField�� private �Ӽ��� Inspector���� ������ ����ȭ �Ͽ� ���� �����ϰ� ���ݴϴ�.
    //��ũ��Ʈ�� �������� �ʰ� ����Ƽ ������ �� Inspectorâ���� �ǽð����� ���� ���� ������ �� �ֽ��ϴ�.
    //4���� �̹����� �����ϱ� ���� [SerializeField] Ŭ������ ����Ͽ����ϴ�.
    //������ ImageŸ������ ����Ǿ���, �ʱⰪ���� null�� �Ҵ�Ǿ����ϴ�. �� ������ Inspectorâ���� ���� ���� ������ �� �ֽ��ϴ�. 
    [SerializeField] Image image1 = null;
    [SerializeField] Image image2 = null;
    [SerializeField] Image image3 = null;
    [SerializeField] Image image4 = null;

    //����ǰ� �ִ� ���� ó�� �� ���� ȣ��
    void Start()
    {
        //�ڷ�ƾ ���� ����� �Ϲ� �Լ��� �޸�, StartCoroutine()�޼ҵ带 �̿��Ͽ� ��ȣ�ȿ� �ڷ�ƾ �̸��� �־��־�� �մϴ�.
        //�̹����� ���������� ǥ���Ѵٴ� ShowImagesSequentially���� �ڷ�ƾ �̸��� �����߽��ϴ�.
        //�ڷ�ƾ ������ '0.3f'��� ���� �ð�(��)���Ŀ� ����˴ϴ�.
        //image1, image2, image3, image4�� �̹����� �����ϴ� ����Ƽ�� 'Image' ������Ʈ �������Դϴ�.
        StartCoroutine(ShowImagesSequentially(0.3f, image1, image2, image3, image4));
    }

    //�ڷ�ƾ ������ ��ȯ���� IEnumerator�� ���� �մϴ�.
    //�Ϲ����� �Լ��� �޸� ��ȯ�� return�� �ƴ�,  yield return ���� �����־�� �մϴ�.
    /* yield return �ڿ��� ��ȯ �ð��� ����� �ִµ�, ����� ��ȯ �ð� ��ŭ �ڵ� ������ ���� ��Ű��,
     * �ð��� ������ ���� �� ���� �ٽ� �ڷ�ƾ�� �����մϴ�.*/
    //�̹����� ���������� ǥ���ϴ� ����� �����ϱ� ���� �Ű����� 'duration'�� �̿��� �� �̹����� ǥ�õǴ� �ð�(��)�� ��Ÿ���ϴ�.
    //�������� �Ű�����'params'����ϸ� ������ ���� ���� �Ű������� �ѱ� �� �ֽ��ϴ�.
    //params�� ���� ���� ���� 'Image' ������ ���� �� �� �ֽ��ϴ�.
    public IEnumerator ShowImagesSequentially(float duration, params Image[] images)
    {
        //'foreach'����Ͽ� 'images'�迭�� �ִ� �� ��Ҹ� 'Image'���� 'imgae'�� �����Ͽ� ��ȸ�ϴ� �ݺ����Դϴ�.
        foreach (Image image in images)
        {
            //�ݺ��� ���� : 'image'������ 'color'�Ӽ��� �����ϰ� �ֽ��ϴ�.
            //���� ������ RGB������� ('r' , 'g' , 'b')�� �״�� ����ϵ�, ���� ��('a')�� 0���� �����Ͽ� 'new Color'���ο� 'Color'��ü�� �����մϴ�.
            //�̹����� ���İ��� 0�� ������ ������ �������� �����˴ϴ�.
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        }

        //�迭 'images'�� �ִ� ��� �̹����� ���̵� �� ȿ���� �����ϴ� �ݺ����Դϴ�.
        //���� 'for'�ݺ������� 'i'������ ����Ͽ� �迭'images'�� �� ��ҿ� �����մϴ�.
        for (int i = 0; i < images.Length; i++)
        {
            //���� ��ҿ� �ش��ϴ� 'Image' ������Ʈ�� 'image'������ �Ҵ��մϴ�.
            Image image = images[i];
            //'t'������ 0���� �ʱ�ȭ �մϴ�. 
            //�� �κ��� ���̵� �� ���� �ð��� ���� ���� ���¸� ������Ʈ �ϴ� ���� �ǹ��մϴ�.
            float t = 0;

            //while�ݺ����� 't'�� 1�ʺ��� ���� �� ���� ����˴ϴ�.
            while (t < 1.0f)
            {
                //'t' ���� 'Time.deltaTime / duration'��ŭ �����մϴ�.
                //Time.deltaTime�� �̿��ϸ�, PC�� ���ɰ��� �����ϰ� ������ ������ �ǰ� �ǹǷ� �Ȱ��� ��� ���� �������� ����Ͽ����ϴ�.
                //Time.deltaTime�� �����Ӱ��� �ð� ��������, ������ �ӵ��� ������� ������ �������� ������Ʈ �˴ϴ�.
                //duration�� ���̵� ���� ��ü�ð��� ��Ÿ���� �����Դϴ�. �� ���̵� ���� �Ϸ�ɶ� ���� �ɸ��� �ð��Դϴ�.
                //'t'�� ������Ʈ�����ν� ���̵� ���� ���� ���¸� �����ϰ�, ���� ä�� ���� �����Ͽ� �̹��� ������ �����մϴ�.
                //'t'�� 1�� �����ϸ� ���̵� ���� �Ϸ�Ǵ� ���� �ǹ��մϴ�.
                t += Time.deltaTime / duration;
                //'image.color'�� ����Ͽ� �̹����� ���� ä�� ���� 't'�� ������Ʈ �մϴ�.
                //�̹����� ������ ���� �����Ͽ� ���̵��� ȿ���� ��Ÿ���ϴ�.
                image.color = new Color(image.color.r, image.color.g, image.color.b, t);
                //null : 1frame ��ŭ �ڵ� ������ ���� ��ŵ�ϴ�.
                //�� �κ��� ���� ���̵� �� ȿ���� �ε巴�� ����ǵ��� �մϴ�.
                yield return null;
            }
            //new WaitForSeconds(1f) : 1�� ��ŭ ������ ���� ��ŵ�ϴ�.
            //�� �κ��� ���� �� �̹����� ���̵� ���� �Ϸ�� �� 1�ʵ��� ��� �ϴ� ������ �մϴ�.
            yield return new WaitForSeconds(1f);

            //���� ���̵� ���� �غ��ϴ� �κ��Դϴ�.
            t = 1.0f;
        }
    }
}
