using System;

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
    public int Remain { get { return capacity - writeIdx; } }
    //数据长度
    public int Length { get { return writeIdx - readIdx; } }

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
        readIdx = 0;
        writeIdx = defaultBytes.Length;
    }

    //重设尺寸
    public void ReSize(int size)
    {
        if (size < Length) return;
        if (size < initSize) return;
        int n = 1;
        while (n < size) n *= 2;
        capacity = n;
        byte[] newBytes = new byte[n];
        Array.Copy(bytes, readIdx, newBytes, 0, writeIdx - readIdx);
        bytes = newBytes;
        writeIdx = Length;
        readIdx = 0;
    }

    //检查并移动数据
    public void CheckAndMoveBytes()
    {
        if(Length<8)
        {
            MoveBytes();
        }
    }

    //移动数据
    public void MoveBytes()
    {
        if(Length>0)
        {
            Array.Copy(bytes, readIdx, bytes, 0, Length);
        }
        writeIdx = Length;
        readIdx = 0;
    }

    //写入数据
    public int Write(byte[] bs,int offset,int count)
    {
        if(Remain<count)
        {
            ReSize(Length + count);
        }
        Array.Copy(bs, offset, bytes, writeIdx, count);
        writeIdx += count;
        return count;
    }

    //读取数据
    public int Read(byte[] bs,int offset,int count)
    {
        count = Math.Min(count, Length);
        Array.Copy(bytes, readIdx, bs, offset, count);
        readIdx += count;
        CheckAndMoveBytes();
        return count;
    }

    //读取Int16
    public Int16 ReadInt16()
    {
        if (Length < 2) return 0;
        Int16 ret = (Int16)((bytes[readIdx + 1] << 8) | bytes[readIdx]);
        readIdx += 2;
        CheckAndMoveBytes();
        return ret;
    }

    //读取Int32
    public Int32 ReadInt32()
    {
        if (Length < 4) return 0;
        Int32 ret = (Int32)((bytes[readIdx + 3] << 24) |
                            (bytes[readIdx + 2] << 16) |
                            (bytes[readIdx + 1] << 8) |
                            (bytes[readIdx]));
        readIdx += 4;
        CheckAndMoveBytes();
        return ret;
    }
}
