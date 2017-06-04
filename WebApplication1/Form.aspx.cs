using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace WebApplication1
{
    public partial class Form : System.Web.UI.Page
    {
        OracleConnection connect = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id = Sergey; Password=1234;");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                display();
            }
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            connect.Open();            
            OracleCommand command = new OracleCommand(String.Format("INSERT INTO KINOTOP (FILM,GENRE) VALUES ('{0}', '{1}')", 
                TextFilm.Text, TextGenre.Text),connect);
            command.ExecuteNonQuery();
            connect.Close();
            Label4.Text = "Строка успешно добавлена.";
            clearCells();
            display();
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Session["ID"]) != 0)
            {
                connect.Open();
                OracleCommand command = new OracleCommand(String.Format("UPDATE KINOTOP SET FILM='{0}', GENRE='{1}' WHERE ID = '{2}'",
                    TextFilm.Text, TextGenre.Text, Session["ID"]), connect);
                command.ExecuteNonQuery();
                connect.Close();
                Label4.Text = "Строка успешно изменена.";
                clearCells();
                display();
            }
            else
                Label4.Text = "Выберите строчку для редактирования";
        }

        protected void BtnDel_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Session["ID"]) != 0)
            {
                connect.Open();
                OracleCommand command = new OracleCommand(String.Format("DELETE FROM KINOTOP WHERE ID = '{0}'", Session["ID"]), connect);
                command.ExecuteNonQuery();
                connect.Close();
                Label4.Text = "Строка успешно удалена";
                clearCells();
                display();
            }
            else
                Label4.Text = "Выберите строчку для удаления";
        }

        protected void BtnClr_Click(object sender, EventArgs e)
        {
            clearCells();
            Label4.Text = "";
        }

        protected void lnkselect_click(object sender, EventArgs e)
        {
            LinkButton lnkbut = (LinkButton)sender; 
            Session["ID"] = lnkbut.CommandArgument; //передаю значение выбранной строки путем запоминания ID в сессию http запроса
            //int row = Convert.ToInt32(e.CommandArgument) - 21; // Тоже передача значения строки, но локальная, не использую т.к. нужно для update
            int row = IDcount(Convert.ToInt32(Session["ID"]));
            connect.Open();
            OracleDataAdapter ODA = new OracleDataAdapter("SELECT * FROM KINOTOP", connect);
            DataTable dt = new DataTable();
            ODA.Fill(dt);
            
            if(dt.Rows.Count>=0)
            {
                TextFilm.Text = dt.Rows[row]["Film"].ToString();
                TextGenre.Text = dt.Rows[row]["Genre"].ToString();
            }
            connect.Close();
            //Label4.Text = "Номер строки:" + dt.Rows[row]["ID"].ToString();
            Label4.Text = "Номер выбранной строки:" + (row + 1).ToString();
        }

        protected void display()
        {
            connect.Open();
            OracleDataAdapter ODA = new OracleDataAdapter("SELECT * FROM KINOTOP", connect);
            DataTable dt = new DataTable();
            ODA.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            connect.Close();
        }

        protected void clearCells() // Очистка значений в полях и id.
        {
            Session["ID"] = 0;
            TextFilm.Text = "";
            TextGenre.Text = "";
            display();
        }

        protected int IDcount(int SessionRow) // Вычисление номера строки в дататэйбле 
        {
            int IDcount = 0;
            connect.Open();
            OracleDataAdapter ODA = new OracleDataAdapter("SELECT * FROM KINOTOP", connect);
            DataTable dt = new DataTable();
            ODA.Fill(dt);
            connect.Close();
            for (int i=0; i<dt.Rows.Count; i++ )
            {
                if (SessionRow != Convert.ToInt32(dt.Rows[i]["ID"]))
                    IDcount++;
                else
                    return IDcount;
                   
            }
            return IDcount;
        }
    }
}