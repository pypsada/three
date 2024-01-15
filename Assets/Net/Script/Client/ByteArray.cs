using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;

public class ByteArray
{
    //Ĭ�ϴ�С
    const int DEFAULT_SIZE = 1024;
    //��ʼ��С
    int initSize = 0;
    //������
    public byte[] bytes;
    //��дλ��
    public int readIdx = 0;
    public int writeIdx = 0;
    //����
    private int capacity = 0;
    //ʣ��ռ�
    public int remain { get { return capacity - writeIdx; } }
    //���ݳ���
    public int length { get { return writeIdx - readIdx; } }

    //���캯��
    public ByteArray(int size=DEFAULT_SIZE)
    {
        bytes = new byte[size];
        capacity = size;
        initSize = size;
        readIdx = 0;
        writeIdx = 0;
    }

    //���캯��
    public ByteArray(byte[] defaultBytes)
    {
        bytes = defaultBytes;
        capacity = defaultBytes.Length;
        initSize = defaultBytes.Length;
        readIdx = 0;
        writeIdx = defaultBytes.Length;
    }

    //����ߴ�
    public void ReSize(int size)
    {
        if (size < length) return;
        if (size < initSize) return;
        int n = 1;
        while (n < size) n *= 2;
        capacity = n;
        byte[] newBytes = new byte[capacity];
        Array.Copy(bytes, readIdx, newBytes, 0, length);
        bytes = newBytes;
        writeIdx = length;
        readIdx = 0;
    }

    /*
     *�о��������������û�����ã�������ȥ��
     */

    ////��鲢�ƶ�����
    //public void CheckAndMoveBytes(int threshold = 8)
    //{
    //    if (length < threshold)
    //    {
    //        MoveBytes();
    //    }
    //}

    //�ƶ�����
    public void MoveBytes()
    {
        if(length>0)
        {
            Array.Copy(bytes, readIdx, bytes, 0, length);
        }
        writeIdx = length;
        readIdx = 0;
    }
    //Write
    public int Write(byte[] bs, int offset, int count)
    {
        if (remain < count)
        {
            MoveBytes();
            /* ���ƶ����жϣ� ��ǰ���ڳ�һЩλ��
             * ���ж��Ƿ�����ǿռ䲻��������ֻ�ǵ����ص��˽�β 
            */
            if (remain < count)
            {
                ReSize(length + count);
            }
        }
        Array.Copy(bs, offset, bytes, writeIdx, count);
        writeIdx += count;
        return count;
    }

    //Read
    public int Read(byte[] bs,int offset,int count)
    {
        count = Math.Min(length, count);
        Array.Copy(bytes, readIdx, bs, offset, count);
        readIdx += count;
        //CheckAndMoveBytes()
        /* ������д��ʱ���Ѿ��ж����Ƿ��˽�β��
         * ��˲������������������
         * ���ﲻ��ʹ�ú����ٴ��ж������Ƿ��ƶ�
         */
        return count;
    }

    public Int16 ReadInt16()
    {
        if (length < 2) return 0;
        Int16 ret = (Int16)(bytes[readIdx] | (bytes[readIdx + 1] << 8));
        readIdx += 2;
        return ret;
    }

    public Int32 ReadInt32()
    {
        if (length < 4) return 0;
        Int32 ret = (Int32)(bytes[readIdx] |
                            bytes[readIdx + 1] << 8 |
                            bytes[readIdx + 2] << 16 |
                            bytes[readIdx + 3] << 24);
        readIdx += 4;
        return ret;
    }
}
