using System.Runtime.Serialization;
using UnityEngine;

public class Hookg : MonoBehaviour
{
    //���� �ҷ�����
    RopeActive grappling;
    //����Ʈ �ҷ�����
    public DistanceJoint2D joint2D;

    private void Start()
    {
        grappling = GameObject.Find("Player").GetComponent<RopeActive>();
        joint2D = GetComponent<DistanceJoint2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ring"))
        {
            Debug.Log("����");
            joint2D.enabled = true;
            grappling.isAttach = true;
        }
    }
}
