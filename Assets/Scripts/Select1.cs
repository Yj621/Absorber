using UnityEngine;

public class Select1 : MonoBehaviour
{
    Animator select1;

    [SerializeField] private PlayerController playerController;
    void Start()
    {
        select1 = GetComponent<Animator>();
    }
    void Update()
    {
        if (playerController != null)
        {
            //select���� ���� �ٸ� �ִϸ��̼� Ʈ����
            switch (playerController.GetSelect1())
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
