using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleMobSpawner : MonoBehaviour
{
    //SingleMobSpawner�� �ϳ��� ���͸� ����. ���Ͱ� ������ 5�� ������ �� ������ ��ġ�� �ٽ� ��Ȱ��Ŵ
    /*���߿� MultiMobSpawner ����� ��� ���� ���� ���Ͱ� ������ ���� ���� �ð� �������� ��� �����?
    ��⿡�� �ð� ������ ���Ͱ� �ʹ� �������� �� ������ �ɸ����� ������ ���� ������� ����� ����ִ����� �� ������...
    �׸��� static ������ �ξ� ���� �� ����� ���Ͱ� �ִ����� ��ü������ �����ְ� �ϴ� ���� ������*/
    [SerializeField] Enemy emeny;
    private bool IsMobDead = false;
    void Start()
    {
        spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
        //���Ͱ� �׾��ٴ� ������ ������ 5�� ��ٸ��� ���͸� ��Ȱ��Ų��.
    }
    public IEnumerator revive()
    {
        yield return new WaitForSeconds(5);
        spawn();
    }

    public void spawn()
    {
        Instantiate(emeny, gameObject.transform);
        IsMobDead = false;
    }
}
