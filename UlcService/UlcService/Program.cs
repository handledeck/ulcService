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
    static void Main(string[] args)
    {
      ReadIni();
      //.WriteData(s,)
    }


    static void WriteIni() {
      StreamWriter s = new StreamWriter(@"d:\123.ini", false);
      //IniParser.Model.SectionData data = new IniParser.Model.SectionData("one");
      IniParser.Model.SectionDataCollection sectionDatas = new IniParser.Model.SectionDataCollection();
      IniParser.Model.SectionData db = new IniParser.Model.SectionData("DB");
      db.Comments.Add("test to connection");
      db.Keys.AddKey("ip", "127.0.0.1");
      db.Keys.AddKey("port", "5432");
      //db.Keys.AddKey("user", "");
      IniParser.Model.IniData iniData = new IniParser.Model.IniData();
      iniData.Sections.Add(db);
      IniParser.FileIniDataParser fileIniDataParser = new IniParser.FileIniDataParser();
      fileIniDataParser.WriteData(s, iniData);
      s.Flush();
      s.Close();
    }

    static void ReadIni() {
      StreamReader s = new StreamReader(@"d:\123.ini", false);
      IniParser.Parser.IniDataParser p= new IniDataParser();
      IniParser.FileIniDataParser fileIniDataParser = new IniParser.FileIniDataParser();
      var iData=fileIniDataParser.ReadData(s);
      var ip=iData["DB"].GetKeyData("ip").Value;

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
  }
}
