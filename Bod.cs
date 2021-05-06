using System;
using System.Xml.Linq;

namespace JackShaft_App
{
    class Bod
    {
        string A1;
        string B1;
        string C1;
        string D1;

        public void Create_Bod(string XML_Template, string New_XML_Destination, string Paka_In, string Operation_ID, string Operation_Task, string QTY)
        {
            XDocument dok1 = XDocument.Load(XML_Template);

            // Date & Time
            string Time_formatted = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ");

            A1 = Time_formatted;
            foreach (var item0 in dok1.Descendants("ApplicationArea"))
            {
                item0.Element("CreationDateTime").Value = A1;
            }

            // Paka
            string Paka = "1_" + Paka_In;
            foreach (var item1 in dok1.Descendants("DocumentID"))
            {
                item1.Element("ID").Value = Paka;
            }

            // Operation_ID
            B1 = Operation_ID;
            foreach (var item2 in dok1.Descendants("Operations"))
            {
                item2.Element("ID").Value = B1;
            }

            // Task
            C1 = Operation_Task;
            foreach (var item3 in dok1.Descendants("Operations"))
            {
                item3.Element("Task").Value = C1;
            }

            // QTY
            D1 = QTY;
            foreach (var item4 in dok1.Descendants("OutputItem"))
            {
                item4.Element("ProducedBaseUOMQuantity").Value = D1;
            }
            string format = "yyyyMMddhhmmss";
            string New_XML = New_XML_Destination + "BOD_" + Paka_In + DateTime.Now.ToString(format).ToString() + ".xml";

            dok1.Save(New_XML);
        }

    }
}
