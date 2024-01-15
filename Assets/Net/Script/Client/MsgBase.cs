using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class MsgBase
{
    //协议名
    public string protoName = "";

    //编码与解码Json协议体
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

    //编码与解码协议名
    //协议名：Int16+名字
    private byte[] EncodeName()
    {
        //协议名长度
        byte[] nameByte = System.Text.Encoding.UTF8.GetBytes(this.protoName);
        Int16 len = (Int16)nameByte.Length;
        byte[] bytes = new byte[len + 2];
        //按照小端方式组装
        bytes[0] = (byte)(len % 256);
        bytes[1]= (byte)(len / 256);
        //组装所有
        Array.Copy(nameByte, 0, bytes, 2, len);

        return bytes;
    }
    private static string DecodeName(byte[] bytes,int offset,out int count)
    {
        count = 0;
        //必须要大于2字节
        if (offset + 2 > bytes.Length) return "";
        //获取长度
        Int16 len = (Int16)(bytes[offset + 0] | (bytes[offset + 1] << 8));
        if (len <= 0) return "";
        //长度必须足够
        if (offset + len + 2 > bytes.Length) return "";
        //解码
        count = 2 + len;
        string name = System.Text.Encoding.UTF8.GetString(bytes, offset + 2, len);
        return name;
    }

    //编码协议体
    public byte[] Encode()
    {
        byte[] name = EncodeName();
        byte[] body = EncodeBody();
        int len = name.Length + body.Length;
        byte[] buff = new byte[len + 2];
        //同样小端组装
        buff[0]= (byte)(len % 256);
        buff[1]= (byte)(len / 256);
        Array.Copy(name, 0, buff, 2, name.Length);
        Array.Copy(body, 0, buff, 2 + name.Length, body.Length);
        return buff;
    }

    //解码协议体
    public static MsgBase Decode(byte[] bytes, int offset, out int count)
    {
        count = 0;
        if (offset + 2 > bytes.Length) return null;
        //获取长度
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
