using IniParser.Parser;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlcService
{
  class Program
  {

    static string __workingDirectory = Environment.CurrentDirectory;
    static string __file = "serv.ini";
    static string __service_path;
    static void Main(string[] args)
    {
      __workingDirectory = Environment.CurrentDirectory;
      __service_path = string.Format("{0}\\{1}", __workingDirectory, __file);
      ReadIni(__service_path);
    }


    static void WriteIni(string pathFPath) {
      StreamWriter s = new StreamWriter(pathFPath, false);
      IniParser.Model.SectionData db = new IniParser.Model.SectionData("DB");
      IniParser.Model.IniData iniDb = new IniParser.Model.IniData();
      db.Comments.Add("test to connection");
      db.Keys.AddKey("ip", "127.0.0.1");
      db.Keys.AddKey("port", "5432");
      iniDb.Sections.Add(db);
      IniParser.Model.SectionData dbUser = new IniParser.Model.SectionData("DBUser");
      IniParser.Model.IniData iniUser = new IniParser.Model.IniData();
      dbUser.Comments.Add("section for user");
      dbUser.Keys.AddKey("user", "postgres");
      //dbUser.Keys.AddKey("port", "5432");

      iniDb.Sections.Add(dbUser);
      IniParser.FileIniDataParser fileIniDataParser = new IniParser.FileIniDataParser();
      fileIniDataParser.WriteData(s, iniDb);
      s.Flush();
      s.Close();
    }

    static void ReadIni(string srvIniPath) {
      try
      {
        bool fExt=FileExists(srvIniPath);
        if (!fExt) {
          WriteIni(srvIniPath);
        }
        StreamReader s = new StreamReader(srvIniPath, false);
        IniParser.Parser.IniDataParser p = new IniDataParser();
        IniParser.FileIniDataParser fileIniDataParser = new IniParser.FileIniDataParser();
        var iData = fileIniDataParser.ReadData(s);
        var ip = iData["DB"].GetKeyData("ip").Value;
      }
      catch (Exception exc)
      {

        throw;
      }
     

    }

    static void CreateDb()
    {
      string connection = string.Format("Host={0};Port={1};Username={2};Password={3};Database=''",
     "127.0.0.1", 5432, "postgres", "root");
      var dbFactory = new ServiceStack.OrmLite.OrmLiteConnectionFactory(
    connection, PostgreSqlDialect.Provider);
      try
      {
        using (var db = dbFactory.Open())
        {

        }
      }
      catch (Exception ex) { }
    }

    static bool FileExists(string fileName)
    {
      string workingDirectory = Environment.CurrentDirectory;
      var file = string.Format("{0}\\{1}",workingDirectory,fileName);
      return File.Exists(file);
    }
  }
}
