using System;

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
    public int Remain { get { return capacity - writeIdx; } }
    //���ݳ���
    public int Length { get { return writeIdx - readIdx; } }

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
        readIdx = 0;
        writeIdx = defaultBytes.Length;
    }

    //����ߴ�
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

    //��鲢�ƶ�����
    public void CheckAndMoveBytes()
    {
        if(Length<8)
        {
            MoveBytes();
        }
    }

    //�ƶ�����
    public void MoveBytes()
    {
        if(Length>0)
        {
            Array.Copy(bytes, readIdx, bytes, 0, Length);
        }
        writeIdx = Length;
        readIdx = 0;
    }

    //д������
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

    //��ȡ����
    public int Read(byte[] bs,int offset,int count)
    {
        count = Math.Min(count, Length);
        Array.Copy(bytes, readIdx, bs, offset, count);
        readIdx += count;
        CheckAndMoveBytes();
        return count;
    }

    //��ȡInt16
    public Int16 ReadInt16()
    {
        if (Length < 2) return 0;
        Int16 ret = (Int16)((bytes[readIdx + 1] << 8) | bytes[readIdx]);
        readIdx += 2;
        CheckAndMoveBytes();
        return ret;
    }

    //��ȡInt32
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
