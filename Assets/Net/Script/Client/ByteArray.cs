using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;

public class ByteArray
{
    //默认大小
    const int DEFAULT_SIZE = 1024;
    //初始大小
    int initSize = 0;
    //缓冲区
    public byte[] bytes;
    //读写位置
    public int readIdx = 0;
    public int writeIdx = 0;
    //容量
    private int capacity = 0;
    //剩余空间
    public int remain { get { return capacity - writeIdx; } }
    //数据长度
    public int length { get { return writeIdx - readIdx; } }

    //构造函数
    public ByteArray(int size=DEFAULT_SIZE)
    {
        bytes = new byte[size];
        capacity = size;
        initSize = size;
        readIdx = 0;
        writeIdx = 0;
    }

    //构造函数
    public ByteArray(byte[] defaultBytes)
    {
        bytes = defaultBytes;
        capacity = defaultBytes.Length;
        initSize = defaultBytes.Length;
        readIdx = 0;
        writeIdx = defaultBytes.Length;
    }

    //重设尺寸
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
     *感觉下面这个函数并没有作用，于是先去掉
     */

    ////检查并移动数据
    //public void CheckAndMoveBytes(int threshold = 8)
    //{
    //    if (length < threshold)
    //    {
    //        MoveBytes();
    //    }
    //}

    //移动数据
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
            /* 先移动再判断， 给前面腾出一些位置
             * 再判断是否真的是空间不够，还是只是单纯地到了结尾 
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
        /* 由于在写的时候已经判断了是否到了结尾，
         * 因此不会出现数组过长的情况
         * 这里不再使用函数再次判断数组是否移动
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
