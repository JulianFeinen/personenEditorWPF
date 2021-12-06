using System.Data.SqlClient;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace nsTools
{
    internal class datenbankTools
    {

        //"User=julian;" +
        //                "Password=lkof4;" +
        public SqlConnection OeffneDatenbank(string datenbankName, string datenbankUsername, string datenbankPasswort)
        {

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=DESKTOP-T58GORU\SQLEXPRESS;" +
                                  "Initial Catalog=" + datenbankName + ";" +
                                  "User=" + datenbankUsername + ";" +
                                  "Password=" + datenbankPasswort + ";" +
                                  "Integrated Security=True";


            try
            {

                cn.Open();
                return cn;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        internal List<Person> holeAlleBearbeiterAusDB(SqlConnection dbVerbindung, string sqlquery, string dbFilter)
        {

            DataTable dt = new DataTable();
            Int64 mycount = 0;
            mycount = getDatatable(dbVerbindung, sqlquery, dt);
            dbVerbindung.Close();
            List<Person> personenListe = new List<Person>();
            datatableNachPersonenliste(dt, mycount, personenListe, dbFilter);
            return personenListe;
        }

        private static void datatableNachPersonenliste(DataTable dt, long mycount, List<Person> personenListe, string dbFilter)
        {
            for (int i = 0; i < mycount; i++)
            {
                if (dbFilter == "")
                {
                    Person tempPerson = new Person();
                    tempPerson.BearbeiterID = dt.Rows[i].Field<int>("bearbeiterid");
                    tempPerson.Username = dt.Rows[i].Field<string>("Username");
                    tempPerson.Nachname = dt.Rows[i].Field<string>("nachname");
                    tempPerson.Vorname = dt.Rows[i].Field<string>("vorname");
                    personenListe.Add(tempPerson);
                }
                else
                {
                    var nachname = dt.Rows[i].Field<string>("nachname");
                    var vorname = dt.Rows[i].Field<string>("vorname");
                    if ((nachname.ToLower().Contains(dbFilter.ToLower())) || (vorname.ToLower().Contains(dbFilter.ToLower())))
                    {
                        Person tempPerson = new Person();
                        tempPerson.BearbeiterID = dt.Rows[i].Field<int>("bearbeiterid");
                        tempPerson.Username = dt.Rows[i].Field<string>("Username");
                        tempPerson.Nachname = dt.Rows[i].Field<string>("nachname");
                        tempPerson.Vorname = dt.Rows[i].Field<string>("vorname");
                        personenListe.Add(tempPerson);
                    }

                }
            }
        }

        private static long getDatatable(SqlConnection dbVerbindung, string sqlquery, DataTable dt)
        {
            long mycount;
            SqlCommand com = new SqlCommand();
            com.CommandText = sqlquery;
            com.Connection = dbVerbindung;
            SqlDataAdapter da = new SqlDataAdapter(com);
            mycount = da.Fill(dt);
            return mycount;
        }

        internal void importNachDatenbank(List<Person> importpersonen)
        {
            
                SqlConnection dbVerbindung = OeffneDatenbank("julidatabank", "julian", "aidabella");
                string sqlResetscript =
                    "USE [julidatabank]\n" +
"GO\n" +

"drop table BEARBEITER;\n" +
                "SET ANSI_NULLS ON\n" +
                "GO\n" +

"SET QUOTED_IDENTIFIER ON\n" +
"GO\n" +

"CREATE TABLE[dbo].[BEARBEITER](\n" +

   "[BEARBEITERID][int] IDENTITY(1, 1) NOT NULL,\n" +

   "[USERNAME] [varchar](45) NULL,\n" +
    "[NACHNAME] [varchar](45) NULL,\n" +
    "[VORNAME] [varchar](45) NULL,\n" +
    "[RANG] [varchar](45) NULL,\n" +
    "[RITES] [varchar](45) NULL,\n" +
    "[INITIAL_] [varchar](45) NULL,\n" +
    "[AKTIV] [smallint] NULL,\n" +
    "[ABTEILUNG] [varchar](45) NULL,\n" +
    "[TELEFON] [varchar](145) NULL,\n" +
    "[FAX] [varchar](145) NULL,\n" +
    "[KUERZEL1] [char](2) NULL,\n" +
    "[NAMENSZUSATZ] [varchar](45) NULL,\n" +
    "[EMAIL] [varchar](45) NULL,\n" +
    "[ROLLE] [varchar](145) NULL,\n" +
    "[EXPANDHEADERINSACHGEBIET] [varchar](245) NULL,\n" +
    "[STDGRANTS] [varchar](200) NULL,\n" +
    "[anrede] [varchar](50) NOT NULL,\n" +
"PRIMARY KEY CLUSTERED\n" +
"(\n" +
    "[BEARBEITERID] ASC\n" +
")WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]\n" +
") ON[PRIMARY]\n" +
"GO\n" +

"ALTER TABLE[dbo].[BEARBEITER] ADD CONSTRAINT[DF_BEARBEITER_anrede]  DEFAULT('Herr') FOR[anrede]\n" +
"GO\n";
                string[] sqlResetScripts = sqlResetscript.Split(new string[] { "GO\r\n", "GO ", "GO","GO\n", "GO\t" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string SplitScript in sqlResetScripts)
                {
                    SqlCommand com2 = new SqlCommand();
                    com2.CommandText = SplitScript;
                    com2.Connection = dbVerbindung;
                    SqlDataAdapter da2 = new SqlDataAdapter(com2);
                    var kobjssss2 = com2.ExecuteScalar();
                    com2.Dispose();
                    da2.Dispose();
                }

                foreach (nsTools.Person person in importpersonen)
                {
                    string sqlquery = "INSERT INTO BEARBEITER (USERNAME, NACHNAME, VORNAME)" +
                " VALUES ('" + person.Username + "', '" + person.Nachname + "', '" + person.Vorname + "');";

                    SqlCommand com = new SqlCommand();
                    com.CommandText = sqlquery;
                    com.Connection = dbVerbindung;
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    var kobjssss = com.ExecuteScalar();
                    com.Dispose();
                    da.Dispose();


                }
            
            



            //"INSERT INTO BEARBEITER (BEARBEITERID, USERNAME, NACHNAME, VORNAME)" +
            //"VALUES ('', '', '', '');";
        }
    }
}