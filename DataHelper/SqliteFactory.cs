using System.Data.SQLite;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace DatabaseLib
{
    public class SqliteFactory : IDataFactory
    {
        public void CallException(string message)
        {
            DataHelper.AddErrorLog(new Exception(message));
        }

        public bool ConnectionTest()
        {
            //创建连接对象
            using (SQLiteConnection m_Conn = new SQLiteConnection(DataHelper.ConnectString))
            {
                //SQLiteiteConnection.ConnectionTimeout = 1;//设置连接超时的时间
                try
                {
                    //Open DataBase
                    //打开数据库
                    m_Conn.Open();
                    if (m_Conn.State == ConnectionState.Open)
                    {
                        return true;
                    }
                }
                catch (Exception e)
                {
                    CallException(e.Message);
                }
            }
            //SQLiteiteConnection   is   a   SQLiteConnection   object 
            return false;
        }

        public TypeAffinity ConvertType(SqlDbType dbType)
        {
            switch (dbType)
            {
                case SqlDbType.BigInt:
                    return TypeAffinity.Int64;
                case SqlDbType.Binary:
                    return TypeAffinity.Blob;
                case SqlDbType.Bit:
                    return TypeAffinity.Blob;
                case SqlDbType.Date:
                    return TypeAffinity.DateTime;
                case SqlDbType.SmallDateTime:
                case SqlDbType.DateTime2:
                case SqlDbType.DateTime:
                    return TypeAffinity.DateTime;
                case SqlDbType.DateTimeOffset:
                    return TypeAffinity.DateTime;
                case SqlDbType.Decimal:
                    return TypeAffinity.Int64;
                case SqlDbType.Float:
                    return TypeAffinity.Double;
                case SqlDbType.Image:
                    return TypeAffinity.Blob;
                case SqlDbType.Int:
                    return TypeAffinity.Int64;
                case SqlDbType.Money:
                    return TypeAffinity.Double;
                case SqlDbType.NText:
                case SqlDbType.Text:
                    return TypeAffinity.Text;
                case SqlDbType.Real:
                    return TypeAffinity.Double;
                case SqlDbType.SmallInt:
                    return TypeAffinity.Int64;
                case SqlDbType.Structured:
                    return TypeAffinity.Text;
                case SqlDbType.Time:
                    return TypeAffinity.DateTime;
                case SqlDbType.Timestamp:
                    return TypeAffinity.DateTime;
                case SqlDbType.TinyInt:
                    return TypeAffinity.Blob;
                case SqlDbType.VarBinary:
                    return TypeAffinity.Blob;
                case SqlDbType.Char:
                case SqlDbType.NVarChar:
                case SqlDbType.VarChar:
                    return TypeAffinity.Text;
                default:
                    return TypeAffinity.Text;
            }
        }

        public DbParameter CreateParam(string paramName, SqlDbType dbType, object objValue, int size = 0, ParameterDirection direction = ParameterDirection.Input)
        {
            if (string.IsNullOrEmpty(paramName)) return null;
            if (paramName[0] == '@') paramName = 'p' + paramName.TrimStart('@');
            SQLiteParameter parameter = new SQLiteParameter(paramName, ConvertType(dbType));
            if (size > 0) parameter.Size = size;
            if (objValue == null)
            {
                if (direction == ParameterDirection.Output)
                {
                    parameter.Direction = direction;
                    return parameter;
                }
                parameter.IsNullable = true;
                parameter.Value = DBNull.Value;
                return parameter;
            }
            parameter.Value = objValue;
            return parameter;
        }

        #region ExecuteDataset  //执行查询语句，返回一个记录集

        /// <summary>
        /// 返回记录集
        /// </summary>
        /// <param name="SQL">用于返回记录集的SQL语句</param>
        /// <returns>记录集</returns>
        public DataSet ExecuteDataset(string SQL)
        {
            DataSet ds = new DataSet();
            using (SQLiteConnection m_Conn = new SQLiteConnection(DataHelper.ConnectString))
            {
                try
                {
                    var da = new SQLiteDataAdapter();
                    SQLiteCommand cmd = new SQLiteCommand(SQL, m_Conn);
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                }
                catch (Exception e)
                {
                    CallException(SQL + "        " + e.Message);
                }
            }
            return ds;
        }


        /// <summary>
        /// 返回记录集
        /// </summary>
        /// <param name="SQL">用于返回记录集的SQL语句</param>
        /// <param name="TableName">映射表名</param>
        /// <returns>记录集</returns>
        public DataSet ExecuteDataset(string SQL, string TableName)
        {
            DataSet ds = new DataSet();
            using (SQLiteConnection m_Conn = new SQLiteConnection(DataHelper.ConnectString))
            {
                try
                {
                    var da = new SQLiteDataAdapter();
                    SQLiteCommand cmd = new SQLiteCommand(SQL, m_Conn);
                    da.SelectCommand = cmd;
                    da.Fill(ds, TableName);
                }
                catch (Exception e)
                {
                    CallException(SQL + "        " + e.Message);
                }
            }
            return ds;
        }

        /// <summary>
        /// 返回包含多个表的记录集
        /// </summary>
        /// <param name="SQLs">用于返回记录集的SQL语句</param>
        /// <param name="TableNames">映射表名</param>
        /// <returns>记录集</returns>

        public DataSet ExecuteDataset(string[] SQLs, string[] TableNames)
        {
            DataSet ds = new DataSet();
            using (SQLiteConnection m_Conn = new SQLiteConnection(DataHelper.ConnectString))
            {
                try
                {
                    for (int i = 0; i < SQLs.Length; i++)
                    {
                        var da = new SQLiteDataAdapter();
                        SQLiteCommand cmd = new SQLiteCommand(SQLs[i], m_Conn);
                        da.SelectCommand = cmd;
                        da.Fill(ds, TableNames[i]);
                    }
                }
                catch (Exception e)
                {
                    CallException(SQLs + "        " + e.Message);
                }
            }
            return ds;
        }

        #endregion ExecuteDataset

        /// <summary>
        /// 返回表
        /// </summary>
        /// <param name="SQL">用于返回记录集的SQL语句</param>
        /// <returns>记录集</returns>
        public DataTable ExecuteDataTable(string SQL)
        {
            DataTable dt = new DataTable();
            using (SQLiteConnection m_Conn = new SQLiteConnection(DataHelper.ConnectString))
            {
                try
                {
                    var da = new SQLiteDataAdapter();
                    SQLiteCommand cmd = new SQLiteCommand(SQL, m_Conn);
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
                catch (Exception e)
                {
                    CallException(SQL + "        " + e.Message);
                }
            }
            return dt;
        }

        #region ExecuteNonQuery //执行非查询语句

        /// <summary>
        /// 执行一条INSERT、UPDATE、DELETE语句
        /// </summary>
        /// <param name="SQL">T-SQL语句</param>
        /// <returns>返回影响的行数</returns>
        public int ExecuteNonQuery(string SQL)
        {
            int res = -1;
            using (SQLiteConnection m_Conn = new SQLiteConnection(DataHelper.ConnectString))
            {
                SQLiteTransaction sqlT = null; //SQLiteBulkLoader loader = new SQLiteBulkLoader(m_Conn);
                //loader.Columns
                try
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(strSQLiteSQL(SQL), m_Conn))
                    {
                        if (m_Conn.State == ConnectionState.Closed)
                            m_Conn.Open();
                        cmd.Connection = m_Conn;
                        sqlT = m_Conn.BeginTransaction();
                        cmd.Transaction = sqlT;
                        res = cmd.ExecuteNonQuery();
                        sqlT.Commit();
                    }
                }
                catch (Exception e)
                {
                    if (sqlT != null)
                        sqlT.Rollback();
                    CallException(SQL + "   " + e.Message);
                    return -1;
                }
                return res;
            }
        }

        /// <summary>
        /// 执行一组INSERT、UPDATE、DELETE语句
        /// </summary>
        /// <param name="SQLs">T-SQL语句</param>
        /// <returns>返回影响的行数</returns>
        public int ExecuteNonQuery(string[] SQLs)
        {
            int res = -1;
            using (SQLiteConnection m_Conn = new SQLiteConnection(DataHelper.ConnectString))
            {
                SQLiteTransaction sqlT = null;
                SQLiteCommand cmd = new SQLiteCommand();
                try
                {
                    if (m_Conn.State == ConnectionState.Closed)
                        m_Conn.Open();
                    cmd.Connection = m_Conn;
                    sqlT = m_Conn.BeginTransaction();
                    cmd.Transaction = sqlT;
                    for (int i = 0; i < SQLs.Length; i++)
                    {
                        cmd.CommandText = SQLs[i];
                        res = cmd.ExecuteNonQuery();
                    }
                    sqlT.Commit();
                }
                catch (Exception e)
                {
                    if (sqlT != null)
                        sqlT.Rollback();
                    CallException(SQLs + "        " + e.Message);
                    res = -1;
                }
                return res;
            }
        }

        /// <summary>
        /// 执行一组INSERT、UPDATE、DELETE语句
        /// </summary>
        /// <param name="SQLs">T-SQL语句</param>
        /// <param name="Pars">执行参数</param>
        /// <returns>返回影响的行数</returns>
        public int ExecuteNonQuery(string[] SQLs, object[][] Pars)
        {
            int res = -1;
            using (SQLiteConnection m_Conn = new SQLiteConnection(DataHelper.ConnectString))
            {
                SQLiteTransaction sqlT = null;
                SQLiteCommand cmd = new SQLiteCommand();
                try
                {
                    if (m_Conn.State == ConnectionState.Closed)
                        m_Conn.Open();
                    cmd.Connection = m_Conn;
                    sqlT = m_Conn.BeginTransaction();
                    cmd.Transaction = sqlT;
                    for (int i = 0; i < SQLs.Length; i++)
                    {
                        cmd.CommandText = SQLs[i];
                        cmd.Parameters.Clear();
                        for (int j = 0; j < Pars[i].Length; j++)
                        {
                            cmd.Parameters.AddWithValue("@p" + j.ToString(), Pars[i][j]);
                        }
                        res = cmd.ExecuteNonQuery();
                    }
                    sqlT.Commit();
                }
                catch (Exception e)
                {
                    if (sqlT != null)
                        sqlT.Rollback();
                    CallException(SQLs + "        " + e.Message);
                    res = -1;
                }
                return res;
            }
        }

        #endregion ExecuteNonQuery

        #region FillDataSet //填充一个记录集

        /// <summary>
        /// 用指定的SQL语句来填充一个记录集
        /// </summary>
        /// <param name="ds">记录集</param>
        /// <param name="SQL">SELECT语句</param>
        /// <param name="TableName">映射表名</param>
        public void FillDataSet(ref DataSet ds, string SQL, string TableName)
        {
            try
            {
                SQLiteConnection m_Conn;
                m_Conn = new SQLiteConnection(DataHelper.ConnectString);
                var da = new SQLiteDataAdapter();
                SQLiteCommand cmd = new SQLiteCommand(SQL, m_Conn);
                da.SelectCommand = cmd;
                da.Fill(ds, TableName);
            }
            catch (Exception e)
            {
                CallException(SQL + "        " + e.Message);
            }
        }

        #endregion FillDataSet

        #region
        // <summary>
        /// 返回一个SQLiteDataReader
        /// </summary>
        public DbDataReader ExecuteReader(string sSQL)
        {
            string isSQL;
            isSQL = sSQL.ToUpper();

            if (isSQL.Contains("ISNULL"))  
            {
                isSQL = isSQL.Replace("ISNULL", "IFNULL");
            }
            isSQL = strSQLiteSQL(isSQL);
            SQLiteConnection connection = new SQLiteConnection(DataHelper.ConnectString);
            SQLiteCommand command = new SQLiteCommand(isSQL, connection);
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }


        public DbDataReader ExecuteProcedureReader(string sSQL, params DbParameter[] ParaName)
        {
            string Asql = "";
            string[] sql = new string[8];
            sql[0] = "SELECT DriverID, PropertyName, PropertyValue FROM Argument;";
            sql[1] = "SELECT M.DRIVERID, DRIVERNAME, R.AssemblyName, R.ClassFullName FROM META_DRIVER M INNER JOIN RegisterModule R ON M.DRIVERTYPE = R.DriverID;";
            sql[2] = "SELECT COUNT(*) FROM META_TAG;";
            sql[3] = "SELECT TAGID, GROUPID, RTRIM(TAGNAME),[ADDRESS], DATATYPE, DATASIZE, ARCHIVE, MAXIMUM, MINIMUM, CYCLE FROM META_TAG WHERE ISACTIVE = 1;";
            sql[4] = "SELECT DRIVERID, GROUPNAME, GROUPID, UPDATERATE, DEADBAND, ISACTIVE FROM META_GROUP;";
            sql[5] = "SELECT SOURCE FROM META_Condition WHERE EVENTTYPE = 2;";
            sql[6] = "SELECT TYPEID, SOURCE, ALARMTYPE, A.ISENABLED,CONDITIONTYPE,PARA,IFNULL(COMMENT, ''),DEADBAND,DELAY,SUBALARMTYPE,Threshold,SEVERITY," +
                    "IFNULL([MESSAGE],''),B.ISENABLE FROM META_Condition a LEFT OUTER JOIN META_SUBCONDITION b ON a.TypeID = b.ConditionID WHERE EVENTTYPE<> 2; ";
            sql[7] = "SELECT SCALEID, SCALETYPE, EUHI, EULO, RAWHI, RAWLO FROM META_SCALE;";
            switch (sSQL.ToUpper())
            {
                case "INITSERVER":
                    {
                        switch (ParaName[0].Value.ToString())
                        {
                            case "0": //服务端
                                {
                                    Asql = sql[0] + sql[1] + sql[2] + sql[3] + sql[4] + sql[5] + sql[6] + sql[7];

                                }
                                break;
                            case "1": //客户端
                                {
                                    Asql = sql[2] + sql[3] + sql[6] + sql[7];

                                }
                                break;
                            case "2": //控制器端
                                {
                                    Asql = sql[0] + sql[1] + sql[2] + sql[3] + sql[4] + sql[7];

                                }
                                break;
                        }
                    }
                    break;
                case "ADDEVENTLOG":
                    {
                        Asql = "";
                    }
                    break;
                case "GETALARM":
                    {
                        Asql = string.Format(@"SELECT StartTime,AlarmText,AlarmValue,SubAlarmType,Severity,ConditionID,Source,Duration FROM 
                                                LOG_ALARM WHERE StartTime BETWEEN '{0}' AND '{1}' ORDER BY StartTime", strSQLiteDateTime(ParaName[0].Value.ToString()), strSQLiteDateTime(ParaName[1].Value.ToString()));
                    }
                    break;
                case "GETEVENTTIME":
                    {

                    }
                    break;
                case "READALL":
                    {

                    }
                    break;
                case "READHDATA":
                    {
                        if (ParaName[2].Value != null)
                        {
                            Asql = string.Format(@"SELECT [TIMESTAMP],[VALUE],M.DATATYPE FROM LOG_HDATA L INNER JOIN META_TAG M ON L.ID=M.TAGID 
                                                    WHERE ID='{2}' AND [TIMESTAMP] BETWEEN '{0}' AND '{1}'  ORDER BY [TIMESTAMP];", strSQLiteDateTime(ParaName[0].Value.ToString()), strSQLiteDateTime(ParaName[1].Value.ToString()), ParaName[2].Value.ToString());
                           }
                        else
                        {
                            Asql = string.Format(@"SELECT ID,[TIMESTAMP],[VALUE], M.DATATYPE FROM LOG_HDATA L INNER JOIN META_TAG M ON L.ID = M.TAGID
                                                    WHERE[TIMESTAMP] BETWEEN '{0}' AND '{1}' ORDER BY ID,[TIMESTAMP];", strSQLiteDateTime(ParaName[0].Value.ToString()), strSQLiteDateTime(ParaName[1].Value.ToString()));
                        }

                    }
                    break;
                case "READVALUEBYID":
                    {

                    }
                    break;
                case "UPDATEVALUEBYID":
                    {

                    }
                    break;
                case "WRITEHDATA":
                    {
                        Asql = string.Format(@"SELECT COUNT(*),COUNT(DISTINCT ID) FROM LOG_HDATA WHERE julianday(datetime('{0}'))-julianday(TIMESTAMP) <1;
                                                SELECT H.ID,T.DATATYPE,C FROM( SELECT ID,COUNT(*)C FROM LOG_HDATA WHERE julianday(datetime('{0}'))-julianday(TIMESTAMP) <1 GROUP BY ID)H INNER JOIN META_TAG T ON H.ID=T.TAGID ORDER BY ID;
                                                SELECT [TIMESTAMP],VALUE FROM LOG_HDATA WHERE   julianday(datetime('{0}'))-julianday(TIMESTAMP) <1 ORDER BY ID,[TIMESTAMP];",strSQLiteDateTime( ParaName[0].Value.ToString()));
                    }
                    break;


            }

            SQLiteConnection connection = new SQLiteConnection(DataHelper.ConnectString);
            SQLiteCommand command = new SQLiteCommand(Asql, connection);
           
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                return command.ExecuteReader();
            }
            catch (Exception e)
            {
                CallException(Asql + "        " + e.Message);
                return null;
            }
        }
        public DbDataReader ExecuteProcedureReader1(string sSQL, params DbParameter[] ParaName)
        {
            SQLiteConnection connection = new SQLiteConnection(DataHelper.ConnectString);
            SQLiteCommand command = new SQLiteCommand(sSQL, connection);
            command.CommandType = CommandType.StoredProcedure;
            if (ParaName != null)
            {
                command.Parameters.AddRange(ParaName);
            }
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                return command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception e)
            {
                CallException(sSQL + "        " + e.Message);
                return null;
            }
        }
        #endregion

        #region ExecuteScalar //执行查询，并返回查询所返回的结果集中第一行的第一列

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列
        /// </summary>
        /// <param name="sSQL">SQL语句</param>
        /// <returns></returns>
        public object ExecuteScalar(string sSQL)
        {
            SQLiteTransaction sqlT = null;
            using (SQLiteConnection m_Conn = new SQLiteConnection(DataHelper.ConnectString))
            {
                SQLiteCommand cmd = new SQLiteCommand(sSQL, m_Conn);
                try
                {
                    if (m_Conn.State == ConnectionState.Closed)
                        m_Conn.Open();
                    sqlT = m_Conn.BeginTransaction();
                    cmd.Transaction = sqlT;
                    var res = cmd.ExecuteScalar();
                    sqlT.Commit();
                    if (res == DBNull.Value) res = null;
                    return res;
                }
                catch (Exception e)
                {
                    if (sqlT != null)
                        sqlT.Rollback();
                    CallException(sSQL + "        " + e.Message);
                    return null;
                }
            }
        }

        #endregion ExecuteScalar

        #region ExecuteStoredProcedure //执行一个存储过程

        /// <summary>
        /// 执行一个带参数的存储过程
        /// </summary>
        /// <param name="ProName">存储过程名</param>
        /// <param name="ParaName">参数名称</param>
        /// <param name="ParaDir">参数方向，Input参数是输入参数 InputOutput参数既能输入，也能输出 Output参数是输出参数 ReturnValue参数存储过程返回值。</param>
        /// <param name="Para">参数对象数组</param>
        /// <returns>成功返回true，失败返回false</returns>
        public int ExecuteStoredProcedure(string ProName, params DbParameter[] ParaName)
        {
            using (SQLiteConnection m_Conn = new SQLiteConnection(DataHelper.ConnectString))
            {
                try
                {
                    SQLiteCommand cmd = new SQLiteCommand(ProName, m_Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    if (ParaName != null)
                    {
                        cmd.Parameters.AddRange(ParaName);
                    }
                    SQLiteParameter param = new SQLiteParameter();
                    cmd.Parameters.Add(param);
                    param.Direction = ParameterDirection.ReturnValue;
                    if (m_Conn.State == ConnectionState.Closed)
                    {
                        m_Conn.Open();
                    }
                    cmd.ExecuteNonQuery();
                    return (int)param.Value;
                }
                catch (Exception e)
                {
                    CallException(ProName + "        " + e.Message);
                    return -1;
                }
            }
        }

        /// <summary>
        /// 执行一个没有参数和返回值的存储过程（默认参数类型）
        /// </summary>
        /// <param name="ProName">存储过程名</param>
        /// <param name="Para">参数对象数组</param>
        /// <returns>成功返回true，失败返回false</returns>
        public bool ExecuteStoredProcedure(string ProName)
        {
            using (SQLiteConnection m_Conn = new SQLiteConnection(DataHelper.ConnectString))
            {
                try
                {
                    if (m_Conn.State == ConnectionState.Closed)
                        m_Conn.Open();
                    SQLiteCommand cmd = new SQLiteCommand(ProName, m_Conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    CallException(ProName + "        " + e.Message);
                    return false;
                }
            }
        }

        /// <summary>
        /// 执行一个带参数的存储过程，并返回数据集
        /// </summary>
        /// <param name="ProName">存储过程名</param>
        /// <param name="ParaName">参数名称</param>
        /// <param name="Para">参数对象数组</param>
        /// <param name="ds">执行过程中返回的数据集</param>
        /// <returns>成功返回true，失败返回false</returns>
        public DataSet ExecuteDataSetProcedure(string ProName, ref int returnValue, params DbParameter[] ParaName)
        {
            using (SQLiteConnection m_Conn = new SQLiteConnection(DataHelper.ConnectString))
            {
                DataSet ds = new DataSet();
                try
                {
                    SQLiteCommand cmd = new SQLiteCommand(ProName, m_Conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (ParaName != null)
                    {
                        cmd.Parameters.AddRange(ParaName);
                    }
                    SQLiteParameter param = new SQLiteParameter { Direction = ParameterDirection.ReturnValue };
                    cmd.Parameters.Add(param);
                    if (m_Conn.State == ConnectionState.Closed)
                        m_Conn.Open();
                    var da = new SQLiteDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    returnValue = (int)param.Value;
                    return ds;
                }
                catch (Exception e)
                {
                    CallException(ProName + "        " + e.Message);
                    return null;
                }
            }
        }

        public DataSet ExecuteDataSetProcedure(string ProName, params DbParameter[] ParaName)
        {
            using (SQLiteConnection m_Conn = new SQLiteConnection(DataHelper.ConnectString))
            {
                DataSet ds = new DataSet();
                try
                {
                    SQLiteCommand cmd = new SQLiteCommand(ProName, m_Conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (ParaName != null)
                    {
                        cmd.Parameters.AddRange(ParaName);
                    }
                    if (m_Conn.State == ConnectionState.Closed)
                        m_Conn.Open();
                    var da = new SQLiteDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    return ds;
                }

                catch (Exception e)
                {
                    CallException(ProName + "        " + e.Message);
                    return null;
                }
            }
        }

        /// <summary>
        /// 执行一个带参数的存储过程，并返回数据集
        /// </summary>
        /// <param name="ProName">存储过程名</param>
        /// <param name="ParaName">参数名称</param>
        /// <param name="Para">参数对象数组</param>
        /// <param name="ds">执行过程中返回的数据集</param>
        /// <returns>成功返回true，失败返回false</returns>
        /// 
        public DataTable ExecuteDataTableProcedure(string ProName, params DbParameter[] ParaName)
        {
            using (SQLiteConnection m_Conn = new SQLiteConnection(DataHelper.ConnectString))
            {
                DataTable ds = new DataTable();
                try
                {
                    SQLiteCommand cmd = new SQLiteCommand(ProName, m_Conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (ParaName != null)
                    {
                        cmd.Parameters.AddRange(ParaName);
                    }
                    if (m_Conn.State == ConnectionState.Closed)
                        m_Conn.Open();
                    var da = new SQLiteDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    return ds;
                }
                catch (Exception e)
                {
                    CallException(ProName + "        " + e.Message);
                    return null;
                }
            }
        }

        public DataTable ExecuteDataTableProcedure(string ProName, ref int returnValue, DbParameter[] ParaName)
        {
            using (SQLiteConnection m_Conn = new SQLiteConnection(DataHelper.ConnectString))
            {
                DataTable ds = new DataTable();
                try
                {
                    SQLiteCommand cmd = new SQLiteCommand(ProName, m_Conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (ParaName != null)
                    {
                        cmd.Parameters.AddRange(ParaName);
                    }
                    SQLiteParameter param = new SQLiteParameter { Direction = ParameterDirection.ReturnValue };
                    cmd.Parameters.Add(param);
                    if (m_Conn.State == ConnectionState.Closed)
                        m_Conn.Open();
                    var da = new SQLiteDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    returnValue = (int)param.Value;
                    return ds;
                }
                catch (Exception e)
                {
                    CallException(ProName + "        " + e.Message);
                    return null;
                }
            }
        }

        /// <summary>
        /// 执行一个带参数的存储过程,同时输出一行
        /// </summary>
        /// <param name="ProName">存储过程名</param>
        /// <param name="ParaName">参数名称</param>
        /// <param name="Para">参数对象数组</param>
        /// <returns>返回整数</returns>		
        public DataRow ExecuteDataRowProcedure(string ProName, params DbParameter[] ParaName)
        {
            using (SQLiteConnection m_Conn = new SQLiteConnection(DataHelper.ConnectString))
            {
                try
                {
                    SQLiteCommand cmd = new SQLiteCommand(ProName, m_Conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (ParaName != null)
                    {
                        cmd.Parameters.AddRange(ParaName);
                    }
                    if (m_Conn.State == ConnectionState.Closed)
                        m_Conn.Open();
                    DataTable table = new DataTable();
                    var da = new SQLiteDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(table);
                    if (table.Rows.Count > 0)
                        return table.Rows[0];
                    else
                        return table.NewRow();
                }
                catch (Exception e)
                {
                    CallException(ProName + "        " + e.Message);
                    return null;
                }
            }
        }

        /// <summary>
        /// 执行一个带参数的存储过程,同时输出一行
        /// </summary>
        /// <param name="ProName">存储过程名</param>
        /// <param name="ParaName">参数名称</param>
        /// <param name="Para">参数对象数组</param>
        /// <returns>返回整数</returns>		
        public DataRowView ExecuteDataRowViewProcedure(string ProName, params DbParameter[] ParaName)
        {
            using (SQLiteConnection m_Conn = new SQLiteConnection(DataHelper.ConnectString))
            {
                try
                {
                    SQLiteCommand cmd = new SQLiteCommand(ProName, m_Conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (ParaName != null)
                    {
                        cmd.Parameters.AddRange(ParaName);
                    }
                    if (m_Conn.State == ConnectionState.Closed)
                        m_Conn.Open();
                    DataTable table = new DataTable();
                    var da = new SQLiteDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(table);
                    if (table.Rows.Count > 0)
                        return table.DefaultView[0];
                    else
                        return table.DefaultView.AddNew();
                }
                catch (Exception e)
                {
                    CallException(ProName + "        " + e.Message);
                    return null;
                }
            }
        }

        public bool BulkCopy(IDataReader reader, string tableName, string command = null, SqlBulkCopyOptions options = SqlBulkCopyOptions.Default)
        {
            
            using (SQLiteConnection m_Conn = new SQLiteConnection(DataHelper.ConnectString))
            {
                SQLiteTransaction sqlT = null;
                try
                {
                    if (m_Conn.State == ConnectionState.Closed)
                        m_Conn.Open();
                    sqlT = m_Conn.BeginTransaction();
                    
                    string tmpPath = Path.GetTempFileName();
                    string csv = ReaderToCsv(reader,tableName);
                    string[] readers = csv.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                    if (!string.IsNullOrEmpty(command))
                    {
                        SQLiteCommand cmd = new SQLiteCommand(command, m_Conn);
                        cmd.Transaction = sqlT;
                        cmd.ExecuteNonQuery();
                    }

                    for (int i = 0; i < readers.Length; i++)
                    {
                        SQLiteCommand cmd = new SQLiteCommand( m_Conn);
                        cmd.Transaction = sqlT;
                        string sql = string.Format("insert into[{0}] values({1})",tableName,readers[i]);
         
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }
                  

                    File.WriteAllText(tmpPath, csv);
                    //SQLiteBulkLoader copy = new SQLiteBulkLoader(m_Conn)
                    //{
                    //    FieldTerminator = ",",
                    //    FieldQuotationCharacter = '"',
                    //    EscapeCharacter = '"',
                    //    LineTerminator = "\r\n",
                    //    FileName = tmpPath,
                    //    NumberOfLinesToSkip = 0,
                    //    TableName = tableName,
                   // };
                    //copy.BatchSize = _capacity;
                   // copy.Load();//如果写入失败，考虑不能无限增加线程数
                                //Clear();
                    sqlT.Commit();
                    m_Conn.Close();
                    File.Delete(tmpPath);
                    return true;
                }
                catch (Exception e)
                {
                    if (sqlT != null)
                        sqlT.Rollback();
                    m_Conn.Close();
                    DataHelper.AddErrorLog(e);
                    return false;
                }
            }
        }
        public static string ReaderToCsv(IDataReader reader, string tablename)
        {
            StringBuilder sb = new StringBuilder();
            var colcount = tablename.ToUpper() == "META_TAG" ? reader.FieldCount + 1 : reader.FieldCount;
            while (reader.Read())
            {
                for (int i = 0; i < colcount; i++)
                {
                    var txt = "";

                    if (i != 0) sb.Append(",");

                    if(reader[i] == null)
                    {
                         txt = "''";
                    }
                    else
                    {
                        txt = reader[i].ToString();

                        if (txt.ToUpper() == "FALSE")
                        {
                            txt = "FALSE";
                        }
                        else if (txt.ToUpper() == "TRUE")
                        {
                            txt = "TRUE";
                        }
                        else
                        {
                            txt = "'" + strSQLiteDateTime(txt) + "'";
                        }
                    }
                   
                   
                     sb.Append(txt);
                }
               
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public static string strSQLiteDateTime(string strDateTime)
        {
            string s;
            DateTime dt;
            try
            {
                dt=DateTime.Parse(strDateTime);
                s = dt.ToString("yyyy-MM-dd HH:mm:ss");
                return s;           
            }
            catch
            {
                return strDateTime;
            }
        }
        public static string strSQLiteSQL(string SQL)
        {
            string s = "";
            string[] sArray = SQL.Split('\'');
            for(int i = 0; i < sArray.Length; i++)
            {
                sArray[i] = strSQLiteDateTime(sArray[i]);
            }
            s = string.Join("'", sArray);
            return s;
        }
        #endregion ExecuteStoredProcedure
    }
}
