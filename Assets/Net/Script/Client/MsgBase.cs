using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class MsgBase
{
    //Э����
    public string protoName = "";

    //���������JsonЭ����
    private byte[] EncodeBody()
    {
        string s = JsonUtility.ToJson(this);
        return System.Text.Encoding.UTF8.GetBytes(s);
    }
    private static MsgBase DecodeBody(string protoName,byte[] bytes,int offset,int count)
    {
        string s = System.Text.Encoding.UTF8.GetString(bytes, offset, count);
        MsgBase msgBase = (MsgBase)JsonUtility.FromJson(s, Type.GetType(protoName));
        return msgBase;
    }

    //���������Э����
    //Э������Int16+����
    private byte[] EncodeName()
    {
        //Э��������
        byte[] nameByte = System.Text.Encoding.UTF8.GetBytes(this.protoName);
        Int16 len = (Int16)nameByte.Length;
        byte[] bytes = new byte[len + 2];
        //����С�˷�ʽ��װ
        bytes[0] = (byte)(len % 256);
        bytes[1]= (byte)(len / 256);
        //��װ����
        Array.Copy(nameByte, 0, bytes, 2, len);

        return bytes;
    }
    private static string DecodeName(byte[] bytes,int offset,out int count)
    {
        count = 0;
        //����Ҫ����2�ֽ�
        if (offset + 2 > bytes.Length) return "";
        //��ȡ����
        Int16 len = (Int16)(bytes[offset + 0] | (bytes[offset + 1] << 8));
        if (len <= 0) return "";
        //���ȱ����㹻
        if (offset + len + 2 > bytes.Length) return "";
        //����
        count = 2 + len;
        string name = System.Text.Encoding.UTF8.GetString(bytes, offset + 2, len);
        return name;
    }

    //����Э����
    public byte[] Encode()
    {
        byte[] name = EncodeName();
        byte[] body = EncodeBody();
        int len = name.Length + body.Length;
        byte[] buff = new byte[len + 2];
        //ͬ��С����װ
        buff[0]= (byte)(len % 256);
        buff[1]= (byte)(len / 256);
        Array.Copy(name, 0, buff, 2, name.Length);
        Array.Copy(body, 0, buff, 2 + name.Length, body.Length);
        return buff;
    }

    //����Э����
    public static MsgBase Decode(byte[] bytes, int offset, out int count)
    {
        count = 0;
        if (offset + 2 > bytes.Length) return null;
        //��ȡ����
        Int16 len = (Int16)(bytes[offset + 0] | (bytes[offset + 1] << 8));
        if (len <= 0) return null;
        if (offset + len + 2 > bytes.Length) return null;

        int nameLen;
        string name = DecodeName(bytes, 2, out nameLen);
        if (name == "") return null;
        MsgBase msgBase = DecodeBody(name, bytes, 2 + nameLen, len - nameLen);
        count = len + 2;
        return msgBase;
    }
}
