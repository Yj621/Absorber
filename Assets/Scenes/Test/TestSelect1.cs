using UnityEngine;

public class TestSelect1 : MonoBehaviour
{
    Animator select1;

    [SerializeField] private Test1 test;
    void Start()
    {
        select1 = GetComponent<Animator>();
    }
    void Update()
    {
        if (test != null)
        {
            //select���� ���� �ٸ� �ִϸ��̼� Ʈ����
            switch (test.GetSelect1())
            {
                case 1:
                    select1.SetTrigger("Water");
                    break;
                case 2:
                    select1.SetTrigger("Glass");
                    break;
                case 3:
                    select1.SetTrigger("Rock");
                    break;
            }
        }
    }
}
