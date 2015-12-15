
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GathererDB
{
    class DBConnector
    {
        private SQLiteConnection _oCon;

        internal DBConnector(string sFileName)
        {
            if (!File.Exists(sFileName))
                CreateDB(sFileName);
            else
            {
                _oCon = new SQLiteConnection("Data Source=" + sFileName + ";Version=3;");
                _oCon.Open();
            }
        }

        private void CreateDB(string sFileName)
        {
            SQLiteConnection.CreateFile(sFileName);
            _oCon = new SQLiteConnection("Data Source=" + sFileName + ";Version=3;");
            _oCon.Open();
            //Create ManaTable
            new SQLiteCommand(Resources.SQL.CreateTables, _oCon).ExecuteNonQuery();

            
            HtmlDocument oDoc = new HtmlWeb().Load("http://gatherer.wizards.com/Pages/Default.aspx");
            List<string> oSets = new List<string>();
            HtmlNode oSelect = oDoc.GetElementbyId("ctl00_ctl00_MainContent_Content_SearchControls_setAddText");
            foreach (HtmlNode oNode in oSelect.ChildNodes)
            {
                if (oNode.Name != "option")
                    continue;
                string sInnerText = oNode.InnerText;
                if (!string.IsNullOrEmpty(sInnerText))
                    oSets.Add(sInnerText);
            }
        }

        
    }
}
