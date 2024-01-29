using Org.BouncyCastle.Asn1.Cms;

public static class LogManager
{
    private static string logAdr = "./netlogs.txt";

    public static void ResetAdr(string str)
    {
        logAdr = str;
    }

    /// <summary>
    ///  日志管理函数
    /// </summary>
    /// <param name="str">日志内容</param>
    /// <param name="console">是否出现在控制台</param>
    /// <param name="file">是否出现在日志文件</param>
    public static void Log(string str, bool console = true, bool file = true)
    {
        DateTime now = DateTime.Now;
        string cur = string.Format($"\n[{now.ToString()}]");
        if(console)
        {
            Console.WriteLine(cur);
            Console.WriteLine(str);
        }

        if (!file) return;
        if(File.Exists(logAdr))
        {
            using (StreamWriter sw = File.AppendText(logAdr))
            {
                sw.WriteLine(cur);
                sw.WriteLine(str);
            }
        }
        else
        {
            using (StreamWriter sw = File.CreateText(logAdr))
            {
                sw.WriteLine(cur);
                sw.WriteLine(str);
            }
        }
    }
}