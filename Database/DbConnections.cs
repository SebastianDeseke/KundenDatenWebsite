namespace KundenDatenWebsite;
using MySql.Data.MySqlClient;

public class DbConnection
{

    MySqlConnection connection { get; set; }

    public DbConnection()
    {

    }
    public void Connect()
    {
        // Set up a connection to your database
        connection = new MySqlConnection("Server=localhost;Database=dbkunden;Uid=root;Pwd=;");

        // Open the connection
        //Will haben with every method
        connection.Open();

    }

    public void Disconnect()
    {
        // Close the connection
        //will happen at the end of every method
        connection.Close();
    }

    public void CreateCustomer(string KundenId, string Title, string Vorname, string Nachname, string Firmenname, string Adresse, string Postleizahl, string Telefonnummer, string Email, string Umsatzsteuernummer, int preisKategorie)
    {
        string sqlQuery = @"INSERT INTO 
        kundendaten (KundenID, AnsprechTitle, AnsprechVorname, AnsprechNachname, Firmenname, Adresse, Postleizahl, Telefonnummer, Email, Umsatzsteuernummer, PreisKategorie)
        VALUES (@Value0, @Value1, @Value2, @Value3, @Value4, @Value5, @Value6, @Value7, @Value8, @Value9, @Value10)";

        // Set up a command object with your SQL query and connection
        Connect();
        MySqlCommand command = new MySqlCommand(sqlQuery, connection);

        // Set the parameter values for your query
        command.Parameters.AddWithValue("@Value0", KundenId);
        command.Parameters.AddWithValue("@Value1", Title);
        command.Parameters.AddWithValue("@Value2", Vorname);
        command.Parameters.AddWithValue("@Value3", Nachname);
        command.Parameters.AddWithValue("@Value4", Firmenname);
        command.Parameters.AddWithValue("@Value5", Adresse);
        command.Parameters.AddWithValue("@Value6", Postleizahl);
        command.Parameters.AddWithValue("@Value7", Telefonnummer);
        command.Parameters.AddWithValue("@Value8", Email);
        command.Parameters.AddWithValue("@Value9", Umsatzsteuernummer);
        command.Parameters.AddWithValue("@Value10", preisKategorie);

        // Execute the query
        command.ExecuteNonQuery();
        command.Dispose();
        Disconnect();
    }

    public void UpdateCustomer(string UpdateInput, string UpdateValue, string KundenID)
    {
        string SQLquery = @$"UPDATE kundendaten SET {UpdateInput} = @UpdateValue WHERE KundenID = @KundenID";

        // Set up a command object with your SQL query and connection
        Connect();
        MySqlCommand command = new MySqlCommand(SQLquery, connection);

        // Set the parameter values for your query

        command.Parameters.AddWithValue("@UpdateValue", UpdateValue);
        command.Parameters.AddWithValue("@KundenID", KundenID);
        // Execute the query
        command.ExecuteNonQuery();
        command.Dispose();
        Disconnect();
    }

    public Dictionary<string,string> ReadCustomer(string KundeID)
    {
        string SQLquery = @"SELECT * FROM kundendaten WHERE KundenID = @KundenID";

        Connect();
        MySqlCommand command = new MySqlCommand(SQLquery, connection);

        // Execute the query
        command.Parameters.AddWithValue("@KundenID", KundeID);
        MySqlDataReader reader = command.ExecuteReader();
        var Customer = new Dictionary<string, string>();

        while (reader.Read())
        {
            //Add requires me to know if the Key already exists
            //Customer.Add("KundenID", reader["KundenID"].ToString());
            //If the key already exists, it will overwrite the value, otherwise it will create it
            Customer["KundenID"] = reader["KundenID"].ToString();
            Customer["AnsprechTitle"] = reader["AnsprechTitle"].ToString();
            Customer["AnsprechVorname"] = reader["AnsprechVorname"].ToString();
            Customer["AnsprechNachname"] = reader["AnsprechNachname"].ToString();
            Customer["Firmenname"] = reader["Firmenname"].ToString();
            Customer["Adresse"] = reader["Adresse"].ToString();
            Customer["Postleizahl"] = reader["Postleizahl"].ToString();
            Customer["Telefonnummer"] = reader["Telefonnummer"].ToString();
            Customer["Email"] = reader["Email"].ToString();
            Customer["Umsatzsteuernummer"] = reader["Umsatzsteuernummer"].ToString();
            Customer["PreisKategorie"] = reader["PreisKategorie"].ToString();
        }

        reader.Close();

        command.Dispose();
        Disconnect();
        return Customer;
    }

    public void DeleteCustomer(string KundenID)
    {
        string SQLquery = @$"DELETE FROM 
        kundendaten WHERE KundenID = {KundenID}";

        Connect();
        MySqlCommand command = new MySqlCommand(SQLquery, connection);

        // Execute the query
        command.ExecuteNonQuery();
        command.Dispose();
        Disconnect();
    }

    public void CreateDemoCustomer(string DemoKundenname, string DemoKundenTitle, string DemoKundenadresse, string DemoKundenemail, string DemoKundenTelefonnummer, string DemoKundenGeburtsdatum, DateTime startTime, string DemoKundenID)
    {
        //Same as Customer, just diffrent information
        //Aslo startdate, to determine if and when it should be deleted
        string sqlQuery = @"INSERT INTO 
        demokunden (DemoKundenID, DemoKundenname, DemoKundenTitle, DemoKundenadresse, DemoKundenemail, DemoKundenTelefonnummer, DemoKundenGeburtsdatum, startTime)
        VALUES (@Value0, @Value1, @Value2, @Value3, @Value4, @Value5, @Value6, @Value7)";

        // Set up a command object with your SQL query and connection
        Connect();
        MySqlCommand command = new MySqlCommand(sqlQuery, connection);

        // Set the parameter values for your query
        command.Parameters.AddWithValue("@Value0", DemoKundenID);
        command.Parameters.AddWithValue("@Value1", DemoKundenname);
        command.Parameters.AddWithValue("@Value2", DemoKundenTitle);
        command.Parameters.AddWithValue("@Value3", DemoKundenadresse);
        command.Parameters.AddWithValue("@Value4", DemoKundenemail);
        command.Parameters.AddWithValue("@Value5", DemoKundenTelefonnummer);
        command.Parameters.AddWithValue("@Value6", DemoKundenGeburtsdatum);
        command.Parameters.AddWithValue("@Value7", startTime);
        // Execute the query
        command.ExecuteNonQuery();
        command.Dispose();
        Disconnect();
    }

    public void DeleteDemoCustomer(string DemoKundenID)
    {
        string SQLquery = @$"DELETE FROM 
        demokunden WHERE DemoKundenID = {DemoKundenID}";

        Connect();
        MySqlCommand command = new MySqlCommand(SQLquery, connection);

        // Execute the query
        command.ExecuteNonQuery();
        command.Dispose();
        Disconnect();
    }

}
