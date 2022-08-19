using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathTest_ViewAngle : MonoBehaviour
{

    public float angle;
    public Transform targetCube;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log();
    }
    private bool IsVisibleTarget(Vector3 targetDir)
    {
        if (Vector3.Dot(targetDir, (transform.forward)) > Mathf.Cos(angle / 2 * Mathf.Deg2Rad))
        {
            return true;
        }

        return false;
    }
    private Vector3 GetDir(float angle)
    {
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad)); //�ﰢ�Լ��� �̿��Ͽ� �ش� ������ ���� �������⺤�͸� ���Ѵ�.
        // z�� X������ X���� Z������ �ϱ� ���� X�� Sin Z�� Cos �Լ��� ����Ͽ���. 
    }
    private void OnDrawGizmos()
    {
        Vector3 leftDir = GetDir(-angle / 2);
        Vector3 rightDir = GetDir(angle / 2);
        Vector3 targetDir = (targetCube.position - transform.position).normalized;
        float cosAngle = Mathf.Acos(Vector3.Dot(targetDir, (transform.forward))) * Mathf.Rad2Deg;
        Color visibleColor = Color.black;

        float z = targetCube.position.z - transform.position.z;
        float x = targetCube.position.x - transform.position.x;
        if(targetDir.x < 0)
        {
            cosAngle = 360 - cosAngle;
        }
        //Debug.Log(Mathf.Atan2(x, z) * Mathf.Rad2Deg);// arctan2 ���� ���ϱ�
        Debug.Log(cosAngle);//acos���� �������ϱ�

        Debug.DrawRay(transform.position, leftDir * 10, Color.red);
        Debug.DrawRay(transform.position, rightDir * 10, Color.red);

        if (IsVisibleTarget(targetDir))
            visibleColor = Color.cyan;
        Debug.DrawRay(transform.position, targetDir * (targetCube.position - transform.position).magnitude, visibleColor);
    }
}
