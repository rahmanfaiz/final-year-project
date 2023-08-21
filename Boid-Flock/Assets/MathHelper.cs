using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathHelper : MonoBehaviour
{
    public static MathHelper Instance { get; private set; }


    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(this); }
        else { Instance = this; }
    }

    public int[,] MultiplyMatrix(int[,] A, int[,] B)
    {
        int rA = A.GetLength(0);
        int cA = A.GetLength(1);
        int rB = B.GetLength(0);
        int cB = B.GetLength(1);

        if (cA != rB)
        {
            Debug.Log("Matrixes can't be multiplied!!");
            return new int[0,0];
        }
        else
        {
            int temp = 0;
            int[,] kHasil = new int[rA, cB];

            for (int i = 0; i < rA; i++)
            {
                for (int j = 0; j < cB; j++)
                {
                    temp = 0;
                    for (int k = 0; k < cA; k++)
                    {
                        temp += A[i, k] * B[k, j];
                    }
                    kHasil[i, j] = temp;
                }
            }

            return kHasil;
        }
    }
    public int FindTrace(int[,] mat, int n)
    {
        int sum = 0;

        for (int i = 0; i < n; i++)
            sum += mat[i, i];

        return sum;
    }

    public Vector2 UnitOf(Vector2 vector)
    {
        float magnitude = vector.magnitude;
        Vector2 result = (1 / magnitude) * vector;

        return result;
    }
}
