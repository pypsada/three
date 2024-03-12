using System;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

public class DbManager
{
    public static MySqlConnection? mysql;

    //连接mysql数据库
    public static bool Connect(string db, string ip, int port, string user, string pw)
    {
        mysql = new();
        string s = string.Format("DataBase={0}; Data Source={1}; port={2}; User Id={3}; Password={4}", db, ip, port, user, pw);
        mysql.ConnectionString = s;
        //连接
        try
        {
            mysql.Open();
            LogManager.Log("[DataBase]Connect succ");
            return true;
        }
        catch (Exception e)
        {
            LogManager.Log("[DataBase]Connect fail:" + e.Message);
            return false;
        }
    }

    public static void SendHeartbeat()
    {
        try
        {
            MySqlCommand command = new("SELECT 1", mysql);
            command.ExecuteScalar();
            //LogManager.Log("[Database]Heart beat sent", true, false);
        }
        catch (Exception ex)
        {
            Console.WriteLine("[Database]Database has no heart beat:"+ex.Message);
        }
    }

    //判定安全字符串
    private static bool IsSafeString(string str)
    {
        return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
    }

    //是否存在该用户(能否注册？存在或报错则不能注册返回非，不存在则可以注册返回真)
    public static bool IsAccountExist(string id)
    {
        //防止sql注入
        if (!DbManager.IsSafeString(id))
        {
            return false;
        }
        //sql语句
        string s = string.Format("select * from account where id='{0}';", id);
        //查询
        try
        {
            MySqlCommand cmd = new(s, mysql);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            bool hasRows = dataReader.HasRows;
            dataReader.Close();
            return !hasRows;
        }
        catch (Exception e)
        {
            LogManager.Log("[Database]IsAccountExist err, " + e.Message);
            return false;
        }
    }

    //注册
    public static bool Register(string id, string pw)
    {
        if(!DbManager.IsSafeString(id))
        {
            LogManager.Log("[Database]Register fail, id not safe:"+id);
            return false;
        }
        if (!DbManager.IsSafeString(pw))
        {
            LogManager.Log("[Database]Register fail, pw not safe:"+pw);
            return false;
        }

        //能否注册？
        if(!IsAccountExist(id))
        {
            LogManager.Log("[Database]Register fail, id exist:"+id);
            return false;
        }
        string sql = string.Format("insert into account set id='{0}' , pw='{1}';", id, pw);
        try
        {
            MySqlCommand cmd = new(sql, mysql);
            cmd.ExecuteNonQuery();
            LogManager.Log("[Database]Register succ:"+id);
            return true;
        }
        catch(Exception ex)
        {
            LogManager.Log("[Database]Register fail:" + ex.Message);
            return false;
        }
    }

    //创建角色
    public static bool CreatePlayer(string id)
    {
        if (!DbManager.IsSafeString(id))
        {
            LogManager.Log("[Database]CreatePlayer fail, id not safe:" + id);
            return false;
        }
        //序列化
        PlayerData playerData = new();
        string data = JsonConvert.SerializeObject(playerData);
        //写入db
        string sql = string.Format("insert into player set id='{0}',data='{1}';", id, data);
        try
        {
            MySqlCommand cmd = new(sql, mysql);
            cmd.ExecuteNonQuery();
            LogManager.Log("[Database]CreatPlayer succ:"+id);
            return true;
        }
        catch (Exception ex)
        {
            LogManager.Log("[Database]CreatPlayer fail:" + ex.Message);
            return false;
        }
    }

    //检测用户名密码
    public static bool CheckPassword(string id,string pw)
    {
        if (!DbManager.IsSafeString(id))
        {
            LogManager.Log("[Database]CheckPassword fail, id not safe:" + id);
            return false;
        }
        if (!DbManager.IsSafeString(pw))
        {
            LogManager.Log("[Database]CheckPassword fail, pw not safe:" + pw);
            return false;
        }

        //查询
        string sql = string.Format("select * from account where id='{0}' and pw='{1}';", id, pw);
        try
        {
            MySqlCommand cmd = new(sql, mysql);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            bool hasRows = dataReader.HasRows;
            dataReader.Close();
            return hasRows;
        }
        catch (Exception e)
        {
            LogManager.Log("[Database]CheckPassword err, " + e.Message);
            return false;
        }
    }

    //获取玩家数据
    public static PlayerData? GetPlayerData(string id)
    {
        if (!DbManager.IsSafeString(id))
        {
            LogManager.Log("[Database]GetPlayerData fail, id not safe:"+id);
            return null;
        }
        string sql = string.Format("select * from player where id='{0}';", id);
        try
        {
            //查询
            MySqlCommand cmd = new(sql, mysql);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            if (!dataReader.HasRows)
            {
                dataReader.Close();
                return null;
            }
            //读取
            dataReader.Read();
            string data = dataReader.GetString("data");
            //反序列化
            PlayerData? playerData = JsonConvert.DeserializeObject<PlayerData>(data);
            dataReader.Close();
            return playerData;
        }
        catch(Exception e)
        {
            LogManager.Log("[Database]GetPlayerData fail " + e.Message);
            return null;
        }
    }

    //保存角色
    public static bool UpdatePlayerData(string id,PlayerData playerData)
    {
        //序列化
        string data = JsonConvert.SerializeObject(playerData);
        //sql
        string sql = string.Format("update player set data='{0}' where id='{1}';", data, id);
        //更新
        try
        {
            MySqlCommand cmd = new(sql, mysql);
            cmd.ExecuteNonQuery();
            return true;
        }
        catch(Exception e)
        {
            LogManager.Log("[Database]UpdatePlayer err " + e.Message);
            return false;
        }
    }

    //给新用户分配uid
    public static int GetNewUid()
    {
        string fileName = "./NewUid.json";
        int buff;
        if (!File.Exists(fileName))
        {
            buff = 1;
        }
        else
        {
            string buffJson = File.ReadAllText(fileName);
            buff = (int)JsonConvert.DeserializeObject(buffJson, typeof(int));
        }
        buff++;
        string json = JsonConvert.SerializeObject(buff);
        File.WriteAllText(fileName, json);
        return buff - 1;
    }
}