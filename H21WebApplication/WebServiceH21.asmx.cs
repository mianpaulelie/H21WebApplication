using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace H21WebApplication
{
    /// <summary>
    /// Description résumée de WebServiceH21
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 

    [System.Web.Script.Services.ScriptService]
    public class WebServiceH21 : System.Web.Services.WebService
    {

        [WebMethod(MessageName ="GetXML",Description ="This method return XML output")]
        [System.Xml.Serialization.XmlInclude(typeof(contact))]
        public contact GetXML()
        {
            contact mycontact = new contact();

            mycontact.name = "H21";
            mycontact.email = "h21@";
            mycontact.mobile = "05555";
            return mycontact;
        }

        [WebMethod(MessageName = "GetJson", Description = "This method return Json output")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public string GetJson()
        {
            //  contact mycontact = new contact();

            //   mycontact.name = "H21";
            //   mycontact.email = "h21@";
            //   mycontact.mobile = "05555";

            // string constring="server=myserver.net,1433;database=Test;userid=test;password=test"

            SqlConnection conn = new SqlConnection("server=DESKTOP-DEV-07/SQLSERVEREXPRESS;1433;database=aquatech");

            // here I have to creare list of contact calss
            List<contact> contactslist = new List<contact>();
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    SqlDataReader dr;

                    SqlCommand cmd = new SqlCommand("select id,name,email,mobile from contact", conn);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var contact = new contact
                        {
                            id = Int32.Parse(dr["id"].ToString()),
                            name = dr["name"].ToString(),
                            email = dr["email"].ToString(),
                            mobile = dr["mobile"].ToString()
                        };
                        contactslist.Add(contact);
                    }
                    dr.Close();

                }
            }
            catch (SqlException ex)
            {

            }
            finally
            {
                conn.Close();
            }
            JavaScriptSerializer ser = new JavaScriptSerializer();
            return ser.Serialize(contactslist);

        }
    }
}
