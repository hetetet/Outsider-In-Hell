using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CussFilter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI inputArea;
    [SerializeField] TextMeshProUGUI result;
    
    private static char cussHider = 'X';
    public static string[] cusslist = { "좆", "씨발", "새끼", "병신", "존나", "지랄", "fuck", "shit", "asshole" };
    private short filterType=0;

    [SerializeField] Toggle reverse;
    [SerializeField] Toggle normal;
    [SerializeField] Toggle none;

    public static string cussFilter(int type, string line)
    {
        if (type == 2) //2. type이 2, 즉 검열하지 않는다면 대사를 그대로 반환한다.
            return line;

        //3.리턴할 문자열에 line을 복사.
        string ret = line;
        List<char> linechars = new List<char>();
        linechars.AddRange(line);

        //4. 이전 욕설의 끝부분의 인덱스, 최근에 찾은 욕설의 길이, 문장이 욕설로 시작하는가
        int previndex = 0;
        int cusslength = 0;
        bool startwithcuss = false;

        bool foundbr = false;

        while (true) //문장 전체 다 훑기
        {
            //5. 가장 가까이에 있는 욕설 찾기
            int index_i = ret.Length;
            for (int j = 0; j < cusslist.Length; j++) //욕설목록 하나씩 점검, 가장 가까이 있는 욕설 찾기, type 0일 경우 <br>도 거른다.
            {
                int tempindex = ret.IndexOf(cusslist[j], previndex);
                if (tempindex != -1 && tempindex < index_i)
                {
                    index_i = tempindex;
                    cusslength = cusslist[j].Length;
                    foundbr = false;
                }
            }

            int brIndex = ret.IndexOf("<br>", previndex);
            if (brIndex != -1 && brIndex < index_i)
            {
                index_i = brIndex;
                cusslength = 4;
                foundbr = true;
            }

            //9. 욕설과 함께 시작한 경우 욕설 뒷부분
            if (index_i == ret.Length)
            {
                if (type == 0 && previndex < ret.Length && startwithcuss)
                {
                    if (cusslength <= 2 && index_i - previndex > 1) //욕설의 길이가 2자 이하이거나, ret의 길이와 previndex의 차이가 2 미만일 때
                    {
                        int randomindex = Random.Range(previndex, index_i - 1); //ret.length는 실제 문자열 길이보다 1 크다. null문자 때문.
                        linechars[randomindex] = cussHider;
                    }
                    else if (cusslength > 2 && index_i - previndex > 2)
                    {
                        int randomindex = Random.Range(previndex, index_i - 2);
                        linechars[randomindex] = linechars[randomindex + 1] = cussHider;
                    }
                }
                break;
            }

            if (type == 0 && !foundbr)
            {
                //8. 병맛검열일 때
                if (index_i == 0)
                {
                    startwithcuss = true;
                }
                else
                {
                    if (cusslength <= 2 && index_i - previndex > 0)
                    {
                        int randomindex = Random.Range(previndex, index_i);
                        linechars[randomindex] = cussHider;
                    }
                    else if (cusslength > 2 && index_i - previndex > 1)
                    {
                        int randomindex = Random.Range(previndex, index_i - 1);
                        linechars[randomindex] = linechars[randomindex + 1] = cussHider;
                    }
                }
            }
            else if(type == 1 && !foundbr)
            {
                //7. 정상검열일 때
                if (cusslength == 1)
                    linechars[index_i] = cussHider;
                else
                    linechars[index_i + 1] = cussHider;

                if (cusslength > 2)
                    linechars[index_i + 2] = cussHider;
            }

            //6. 이전 인덱스는 찾은 욕설의 마지막 위치
            previndex = index_i + cusslength;
        }
        //10. 문자 리스트를 문자열로 만들고 ret에 넣어 반환
        ret = new string(linechars.ToArray());
        return ret;
    }

    public void Filtering()
    {
        
        result.text = cussFilter(filterType, inputArea.text);
    }

    public void changeFilterMode()
    {
        if (reverse.isOn)
            filterType = 0;
        else if(normal.isOn)
            filterType= 1;
        else if(none.isOn)
            filterType= 2;
    }
}
