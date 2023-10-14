using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleMobSpawner : MonoBehaviour
{
    //SingleMobSpawner�� �ϳ��� ���͸� ����. ���Ͱ� ������ 5�� ������ �� ������ ��ġ�� �ٽ� ��Ȱ��Ŵ
    /*���߿� MultiMobSpawner ����� ��� ���� ���� ���Ͱ� ������ ���� ���� �ð� �������� ��� �����?
    ��⿡�� �ð� ������ ���Ͱ� �ʹ� �������� �� ������ �ɸ����� ������ ���� ������� ����� ����ִ����� �� ������...
    �׸��� static ������ �ξ� ���� �� ����� ���Ͱ� �ִ����� ��ü������ �����ְ� �ϴ� ���� ������*/
    [SerializeField] Enemy enemy;
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

    public void CoRevive()
    {
        StartCoroutine("revive");
    }
    public IEnumerator revive()
    {
        IsMobDead = true;
        Debug.Log("enemy is dead");
        yield return new WaitForSeconds(60f);
        spawn();
        yield return new WaitForSeconds(0.1f);
    }

    public void spawn()
    {
        Debug.Log("spawn enemy");
        Instantiate(enemy, gameObject.transform);
        IsMobDead = false;
    }
}
