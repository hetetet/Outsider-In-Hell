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
    public static string[] cusslist = { "��", "����", "����", "����", "����", "����", "fuck", "shit", "asshole" };
    private short filterType=0;

    [SerializeField] Toggle reverse;
    [SerializeField] Toggle normal;
    [SerializeField] Toggle none;

    public static string cussFilter(int type, string line)
    {
        if (type == 2) //2. type�� 2, �� �˿����� �ʴ´ٸ� ��縦 �״�� ��ȯ�Ѵ�.
            return line;

        //3.������ ���ڿ��� line�� ����.
        string ret = line;
        List<char> linechars = new List<char>();
        linechars.AddRange(line);

        //4. ���� �弳�� ���κ��� �ε���, �ֱٿ� ã�� �弳�� ����, ������ �弳�� �����ϴ°�
        int previndex = 0;
        int cusslength = 0;
        bool startwithcuss = false;

        bool foundbr = false;

        while (true) //���� ��ü �� �ȱ�
        {
            //5. ���� �����̿� �ִ� �弳 ã��
            int index_i = ret.Length;
            for (int j = 0; j < cusslist.Length; j++) //�弳��� �ϳ��� ����, ���� ������ �ִ� �弳 ã��, type 0�� ��� <br>�� �Ÿ���.
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

            //9. �弳�� �Բ� ������ ��� �弳 �޺κ�
            if (index_i == ret.Length)
            {
                if (type == 0 && previndex < ret.Length && startwithcuss)
                {
                    if (cusslength <= 2 && index_i - previndex > 1) //�弳�� ���̰� 2�� �����̰ų�, ret�� ���̿� previndex�� ���̰� 2 �̸��� ��
                    {
                        int randomindex = Random.Range(previndex, index_i - 1); //ret.length�� ���� ���ڿ� ���̺��� 1 ũ��. null���� ����.
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
                //8. �����˿��� ��
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
                //7. ����˿��� ��
                if (cusslength == 1)
                    linechars[index_i] = cussHider;
                else
                    linechars[index_i + 1] = cussHider;

                if (cusslength > 2)
                    linechars[index_i + 2] = cussHider;
            }

            //6. ���� �ε����� ã�� �弳�� ������ ��ġ
            previndex = index_i + cusslength;
        }
        //10. ���� ����Ʈ�� ���ڿ��� ����� ret�� �־� ��ȯ
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
