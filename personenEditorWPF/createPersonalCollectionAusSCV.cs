using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace nsTools
{
    public class Person : PersonBase
    {
        internal Person Kopie()
        {
            Person temp = new Person();
            temp.Username = this.Username;
            temp.Nachname = this.Nachname;
            temp.Vorname = this.Vorname;
            temp.BearbeiterID = this.BearbeiterID;
            return temp;
        }
    }
    class createPersonalCollectionAusSCV
    {

        public List<Person> holeAlleBearbeiter(string csvDatei, string Filter)
        {

            string filepath = csvDatei;
            StreamReader sr = new StreamReader(filepath);

            string[] Spalten;

            List<string> Zeilen = new List<string>();
            List<Person> personen = new List<Person>();
            while (!sr.EndOfStream)
            {
                Zeilen.Add(sr.ReadLine());

            }


            for (int i = 1; i < Zeilen.Count; i++)
            {
                if (Filter == "")
                {
                    Spalten = Zeilen[i].Split(';');
                    Person bearbeiter = new Person();
                    bearbeiter.BearbeiterID = Convert.ToInt32(Spalten[0]);
                    bearbeiter.Username = Spalten[1];
                    bearbeiter.Nachname = Spalten[2];
                    bearbeiter.Vorname = Spalten[3];
                    personen.Add(bearbeiter);
                }
                else
                {
                    Spalten = Zeilen[i].Split(';');
                    if ((Spalten[2].ToLower().Contains(Filter.ToLower())) || (Spalten[3].ToLower().Contains(Filter.ToLower())))
                    {
                        Person bearbeiter = new Person();
                        bearbeiter.BearbeiterID = Convert.ToInt32(Spalten[0]);
                        bearbeiter.Username = Spalten[1];
                        bearbeiter.Nachname = Spalten[2];
                        bearbeiter.Vorname = Spalten[3];
                        personen.Add(bearbeiter);
                    }


                }

            }


            sr.Close();
            sr.Dispose();
            return personen;
        }
    }
    



}

