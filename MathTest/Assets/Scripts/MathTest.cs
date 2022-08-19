using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathTest : MonoBehaviour
{
    public Vector3 centerPoint;
    public Vector3 scale;
    public Vector3 dir;
    private float speed = 3.0f;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator RotateAxis()
    {
        TransformRot(1);
        yield return new WaitForSeconds(10.3f);

    }
    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.W))
        {
            ShearTest();
        }
      
    }

    void ShearTest()
    {
        Vector3 testNum = new Vector3(1, 1, 1);
        Matrix4x4 x = Matrix4x4.identity;
        Vector3 result = Vector3.one;
        x = GetShearMat(x);
        result = x.MultiplyPoint3x4(testNum);

        print(result);
    }
    Matrix4x4 GetShearMat(Matrix4x4 x)
    {
        x.m00 = 1;
        x.m01 = 0;
        x.m02 = 2;
        x.m03 = 0;
        x.m10 = 0;
        x.m11 = 1;
        x.m12 = 0;
        x.m13 = 0;
        x.m20 = 0;
        x.m21 = 0;
        x.m22 = 1;
        x.m23 = 0;
        x.m30 = 0;
        x.m31 = 0;
        x.m32 = 0;
        x.m33 = 1;

        return x;
    }
    Matrix4x4 GetScaleMatrix(Matrix4x4 x, Vector3 point)
    {
        x.m00 = point.x;
        x.m01 = 0;
        x.m02 = 0;
        x.m03 = 0;
        x.m10 = 0;
        x.m11 = point.y;
        x.m12 = 0;
        x.m13 = 0;
        x.m20 = 0;
        x.m21 = 0;
        x.m22 = point.z;
        x.m23 = 0;
        x.m30 = 0;
        x.m31 = 0;
        x.m32 = 0;
        x.m33 = 1;

        return x;
    }

    Matrix4x4 GetDirMatrix(Matrix4x4 F, Vector3 dir)
    {
        F.m00 = dir.x;
        F.m01 = dir.y;
        F.m02 = dir.z;
        F.m03 = 0;
        F.m10 = 0;
        F.m11 = 0;
        F.m12 = 0;
        F.m13 = 0;
        F.m20 = 0;
        F.m21 = 0;
        F.m22 = 0;
        F.m23 = 0;
        F.m30 = 0;
        F.m31 = 0;
        F.m32 = 0;
        F.m33 = 1;

        return F;
    }
    void TransScale(int tag)
    {
        Matrix4x4 x = Matrix4x4.identity;
        MeshFilter mf = GetComponent<MeshFilter>();
        Vector3[] origVertex = mf.mesh.vertices;
        Vector3[] newVertex = new Vector3[origVertex.Length];

        switch (tag)
        {
            case 0:
                x = GetScaleMatrix(x, scale); //확대
                break;
            case 1:
                x = GetScaleMatrix(x, new Vector3(1/scale.x,1/scale.y,1/scale.z)); //축소
                break;
        }

        for (int i = 0; i < newVertex.Length; ++i)
        {
            newVertex[i] = x.MultiplyPoint3x4(origVertex[i]);
        }
     
        mf.mesh.vertices = newVertex;
    }

    Matrix4x4 CenterPointAxisRot(Vector3 centerPoint,Matrix4x4 x)
    {
        Matrix4x4 centerMat = TransposePoint(centerPoint);
        return centerMat * x  * centerMat.inverse;
    }
    Matrix4x4 TransposePoint(Vector3 point)
    {
        Matrix4x4 centerPoint;
        centerPoint.m00 = 1;
        centerPoint.m01 = 0;
        centerPoint.m02 = 0;
        centerPoint.m03 = point.x;
        centerPoint.m10 = 0;
        centerPoint.m11 = 1;
        centerPoint.m12 = 0;
        centerPoint.m13 = point.y;
        centerPoint.m20 = 0;
        centerPoint.m21 = 0;
        centerPoint.m22 = 1;
        centerPoint.m23 = point.z;
        centerPoint.m30 = 0;
        centerPoint.m31 = 0;
        centerPoint.m32 = 0;
        centerPoint.m33 = 1;

        return centerPoint;
    }
    void TransformRot(int tag)
    {
        float angle = 1.0f;
        float angle_R = Mathf.PI * angle / 180.0f;
        Matrix4x4 x = Matrix4x4.identity;
        MeshFilter mf = GetComponent<MeshFilter>();
        Vector3[] origVertex = mf.mesh.vertices;
        Vector3[] newVertex = new Vector3[origVertex.Length];
        Matrix4x4 moveMat = TransposePoint(centerPoint);

        switch (tag)
        {
            case 0:
                x = Transfer_MatrixRotX(angle_R);
                break;
            case 1:
                x = Transfer_MatrixRotY(angle_R);
                break;
            case 2:
                x = Transfer_MatrixRotZ(angle_R);
                break;
        }
        //moveMat = Matrix4x4.Translate(movePoint);
        x = CenterPointAxisRot(centerPoint,x);
        if (IsVerticalMatrix(x))
        {
            print("직교행렬입니다");
        }

        for (int i = 0; i < newVertex.Length; ++i)
        {
            newVertex[i] = x.MultiplyPoint3x4(origVertex[i]);
        }
        mf.mesh.vertices = newVertex;
    }
    Matrix4x4 Transfer_MatrixRotX(float angle_R)
    {
        Matrix4x4 x;
        x.m00 = 1;
        x.m01 = 0;
        x.m02 = 0;
        x.m03 = 0;
        x.m10 = 0;
        x.m11 = Mathf.Cos(angle_R);
        x.m12 = -Mathf.Sin(angle_R);
        x.m13 = 0;
        x.m20 = 0;
        x.m21 = Mathf.Sin(angle_R);
        x.m22 = Mathf.Cos(angle_R);
        x.m23 = 0;
        x.m30 = 0;
        x.m31 = 0;
        x.m32 = 0;
        x.m33 = 1;

        return x;
    }

    Matrix4x4 Transfer_MatrixRotY(float angle_R)
    {
        Matrix4x4 x;
        x.m00 = Mathf.Cos(angle_R); 
        x.m01 = 0;
        x.m02 = Mathf.Sin(angle_R);
        x.m03 = 0;
        x.m10 = 0;
        x.m11 = 1;
        x.m12 = 0;
        x.m13 = 0;
        x.m20 = -Mathf.Sin(angle_R);
        x.m21 = 0;
        x.m22 = Mathf.Cos(angle_R);
        x.m23 = 0;
        x.m30 = 0;
        x.m31 = 0;
        x.m32 = 0;
        x.m33 = 1;

        return x;
    }

    Matrix4x4 Transfer_MatrixRotZ(float angle_R)
    {
        Matrix4x4 x;
        x.m00 = Mathf.Cos(angle_R);
        x.m01 = -Mathf.Sin(angle_R);
        x.m02 = 0;
        x.m03 = 0;
        x.m10 = Mathf.Sin(angle_R);
        x.m11 = Mathf.Cos(angle_R);
        x.m12 = 0;
        x.m13 = 0;
        x.m20 = 0;
        x.m21 = 0;
        x.m22 = 1;
        x.m23 = 0;
        x.m30 = 0;
        x.m31 = 0;
        x.m32 = 0;
        x.m33 = 1;

        return x;
    }

    bool IsVerticalMatrix(Matrix4x4 mat)
    {
        if(Matrix4x4.identity == mat  * mat.transpose)
        {
            return true;
        }
        return false;
    }
}
