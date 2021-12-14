using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class DBHelper : IDisposable
{
    private readonly SqlConnection _connection;
    private readonly SqlCommand _command = new SqlCommand();
    private SqlDataAdapter _adapter;

    public DBHelper()
    {
        _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["grainConnectionString"].ConnectionString);
        _command.Connection = _connection;
    }

    private void prepareCommand(string cmdText, List<SqlParameter> cmdParameters, CommandType cmdType)
    {
        _command.CommandText = cmdText;
        _command.CommandType = cmdType;

        _command.Parameters.Clear();

        if (cmdParameters != null)
            foreach (SqlParameter _parameter in cmdParameters)
                if (_parameter != null)
                    _command.Parameters.Add(_parameter);

        if (_connection.State != ConnectionState.Open)
            _connection.Open();
    }

    public object ExecuteScalar(string cmdText, List<SqlParameter> cmdParameters = null, CommandType cmdType = CommandType.StoredProcedure)
    {
        prepareCommand(cmdText, cmdParameters, cmdType);
        object _result = _command.ExecuteScalar();
        _connection.Close();

        return _result;
    }

    public int ExecuteNonQuery(string cmdText, List<SqlParameter> cmdParameters = null, CommandType cmdType = CommandType.StoredProcedure)
    {
        int _result;

        prepareCommand(cmdText, cmdParameters, cmdType);
        _result = _command.ExecuteNonQuery();
        _connection.Close();

        return _result;
    }

    public DataSet ExecuteDataSet(string cmdText, List<SqlParameter> cmdParameters = null, CommandType cmdType = CommandType.StoredProcedure)
    {
        DataSet _ds = new DataSet();

        prepareCommand(cmdText, cmdParameters, cmdType);
        _adapter = new SqlDataAdapter(_command);
        _command.CommandTimeout = 240;
        _adapter.Fill(_ds);
        _connection.Close();

        return _ds;
    }

    public IDataReader ExecuteReader(string cmdText, List<SqlParameter> cmdParameters = null, CommandType cmdType = CommandType.StoredProcedure)
    {
        prepareCommand(cmdText, cmdParameters, cmdType);
        IDataReader _dr = _command.ExecuteReader(CommandBehavior.CloseConnection);
        _command.CommandTimeout = 240;
        _connection.Close();

        return _dr;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _connection.Dispose();
            if (_command != null)
                _command.Dispose();
            if (_adapter != null)
                _adapter.Dispose();
        }
    }
}
