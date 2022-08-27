using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Collections.Generic;
using System.Linq;
using kuiperbeltgame.interfaces;
using kuiperbeltgame.entities;
using kuiperbeltgame.drawing;
using kuiperbeltgame.utilities;
using DeviceId;
using System.Text;
using System.Xml;
using System.Xml.Serialization;


namespace kuiperbeltgame.utilities
{
    public class Backup
    {



        private string PATH = (Directory.GetCurrentDirectory() + "/save.kbg");
        private BinaryFormatter formatter;

        public bool IsBackupFileExists()
        {
            if (File.Exists(PATH))
            {
                return true;
            }
            else
            {
                return false;
            }
        }




        public void SaveBackupFile(ListOfScores score)
        {
            try
            {
           XmlSerializer dehydrator = new XmlSerializer(typeof(ListOfScores));
  
           
    using (System.IO.FileStream fs = new FileStream(PATH, FileMode.Create))
    {
        dehydrator.Serialize(fs, score);
    }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
            }
            finally {
               // file.Close();
               
                Console.WriteLine(PATH+" has been successfully written" ) ; 
            }
        }

        public ListOfScores GetPreviousSave()
        {


            try
            {

         


                FileStream readerFileStream = new FileStream(PATH, FileMode.Open, FileAccess.Read);


                XmlSerializer serializer = new XmlSerializer(typeof(ListOfScores));

                ListOfScores i;

                

                using (Stream reader = new FileStream(PATH, FileMode.Open))
                {
                   i = (ListOfScores)serializer.Deserialize(reader); 
                }

                readerFileStream.Close();

                 

                return i;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
                return new ListOfScores();
            }
            finally {
                Console.WriteLine(PATH+" has been successfully checked out" ) ; 
            }
        }



    }
}