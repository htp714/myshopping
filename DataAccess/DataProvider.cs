using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

/// <summary>
/// Summary description for DataProvider
/// </summary>
public class DataProvider
{
    private static MySqlConnection _con = null;

	static DataProvider()
	{
		//Get connection string from web.conf or app.conf
        string strConnection = System.Configuration.ConfigurationManager.ConnectionStrings["H-ShopConnectionString"].ConnectionString;
        _con = new MySqlConnection(strConnection);
	}

    public static DataTable ExecuteQuery(string strQuery)
    {
        DataTable resTable = new DataTable();

        try
        {
            _con.Open();

            MySqlDataAdapter adapter = new MySqlDataAdapter(strQuery, _con);
            
            adapter.Fill(resTable);
        }
        catch (Exception ex)
        {
            throw new Exception("Loi khi thuc thi lenh SQL: " + ex.Message);
        }
        finally
        {
            _con.Close();
        }

        return resTable;
    }

    public static bool ExecuteNonQuery(string strQuery)
    {
        try
        {
            _con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = strQuery;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = _con;
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Loi khi thuc thi lenh SQL: " + ex.Message);
            return false;
            
        }
        finally
        {
            _con.Close();
            
        }
    }


    public static DataTable ExecuteStoreProc(string storeProcName, List<string> arrParameterName, List<string> arrParameterValue)
    {
        DataTable resTable = null;

        try
        {
            _con.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = storeProcName;
            cmd.CommandType = CommandType.StoredProcedure;

            //Lay so parameter
            int N = arrParameterName.Count;
            for (int i = 0; i < N; i++)
            {
                MySqlParameter sqlParam = new MySqlParameter(arrParameterName[i], arrParameterValue[i]);
                cmd.Parameters.Add(sqlParam);
           }

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(resTable);

            _con.Close();
        }
        catch (Exception ex)
        {
            throw new Exception("Loi khi thuc thi store procedure: " + ex.Message);
        }

        return resTable;
    }
}
