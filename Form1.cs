using System;
using System.Data;
using System.Windows.Forms;
//--------------------------
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.ServiceProcess;
using System.Collections.Generic;
using System.Data.Sql;
using System.Threading.Tasks;
using Microsoft.Win32;
using Microsoft.SqlServer.Management.Smo;

namespace dbManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        int flag = 0;
        int btnIndex;

        string dbName;
        string dbPath;
        string separator = Path.DirectorySeparatorChar.ToString();

        string dbConnString = System.Configuration.ConfigurationManager.ConnectionStrings["appConnString"].ToString();




        // keep SQL Server in Running state
        public void keepSqlServerInRunnig(string myServiceName)
        {
            ServiceController myService = new ServiceController();
            myService.ServiceName = myServiceName;

            try
            {
                string svcStatus = myService.Status.ToString();

                switch (svcStatus)
                {
                    case "ContinuePending":
                        MessageBox.Show(myServiceName + " Service is attempting to continue ...");
                        break;

                    case "Paused":
                        MessageBox.Show(myServiceName + " Service is paused." + "\n" + "Attempting to continue the service ...");
                        myService.Continue();
                        break;

                    case "PausePending":
                        MessageBox.Show(myServiceName + " Service is pausing.");

                        Thread.Sleep(5000);
                        try
                        {
                            myService.Start();
                            MessageBox.Show(myServiceName + " Attempting to continue the service ...");
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                        break;

                    case "Running":
                        //MessageBox.Show("Service is already running.");
                        break;

                    case "StartPending":
                        MessageBox.Show(myServiceName + " Service is starting ...");
                        break;

                    case "Stopped":
                        myService.Start();
                        MessageBox.Show(myServiceName + " Service is stopped." + "\n" + "Attempting to start service ...");
                        break;

                    case "StopPending":
                        MessageBox.Show(myServiceName + " Service is stopping.");

                        Thread.Sleep(5000);

                        try
                        {
                            myService.Start();
                            MessageBox.Show("Attempting to Restart service ...");
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }


        }




        //Locate Database Name
        public void addDbNameToList()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Database Files|*.mdf|All Files|*.*";
            open.Title = "Locate Database File";

            try
            {
                if (open.ShowDialog() == DialogResult.OK)
                {
                    string temp = open.FileName;

                    dbPath = Path.GetDirectoryName(temp);
                    dbPath = dbPath.TrimEnd('\\') + separator;
                    dbName = Path.GetFileNameWithoutExtension(temp);

                    lblInfoMsg.Text = dbPath + Path.GetFileName(temp);

                }
            }
            catch (Exception ex)
            {
                lblInfoMsg.Text = ex.Message;
            }

        }




        /// <summary>
        /// checking Database is Attached or Detached and Exists or not...
        /// </summary>
        /// <param name="myDbName">myDbName</param>
        /// <returns></returns>
        public bool checkDbState(string myDbName)
        {
            bool result = false;
            string cmdText = "use master select name from sys.databases where name= '" + myDbName + "'";

            using (SqlConnection sqlConn = new SqlConnection(dbConnString))
            using (SqlCommand cmd = new SqlCommand(cmdText, sqlConn))
            {
                sqlConn.Open();
                using (SqlDataReader sqlReader = cmd.ExecuteReader())
                {
                    if (sqlReader.Read())
                    {
                        result = true;
                    }
                }
            }

            return result;
        }



        /// <summary>
        /// Remove Database!!!
        /// </summary>
        /// <param name="name">Database Name</param>
        public void RemoveDb(string dbName)
        {
            string cmdText = "use master exec sp_dbremove @dbname=[" + dbName + "]";

            using (SqlConnection sqlConn = new SqlConnection(dbConnString))
            using (SqlCommand cmd = new SqlCommand(cmdText, sqlConn))
            {
                sqlConn.Open();
                cmd.ExecuteNonQuery();
                flag = 1;
            }

        }



        /// <summary>
        /// Backup Database without any procedure
        /// </summary>
        /// <param name="dbName">dbName</param>
        public void backupDb(string dbName)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "Specify Path for Backup Database : " + dbName;
            save.DefaultExt = ".bak";

            try
            {
                if (save.ShowDialog() == DialogResult.OK)
                {
                    string savePath = save.FileName;
                    string dynamicBackup = "use master  DECLARE @dynamicSql NVARCHAR(4000), @dbName NVARCHAR(500), @path NVARCHAR(4000)  set @dbName='" + dbName + "'   set @path='" + savePath + "'  SELECT @dynamicSql = 'BACKUP DATABASE ' + @dbName + ' TO DISK ='+CHAR(39) + @path + CHAR(39)+' WITH INIT'   PRINT @dynamicSql   EXEC sp_executeSQL @dynamicSql";

                    using (SqlConnection sqlConn = new SqlConnection(dbConnString))
                    using (SqlCommand cmd = new SqlCommand(dynamicBackup, sqlConn))
                    {
                        sqlConn.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                            flag = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                flag = -1;
                lblInfoMsg.Text = ex.Message;
            }

        }


        /// <summary>
        /// Restore Database
        /// </summary>
        /// <param name="dbName">dbName</param>
        public void restoreDb(string dbName)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "open your backup file to Restore Database : " + dbName;

            if (open.ShowDialog() == DialogResult.OK)
            {
                string openPath = open.FileName;
                string cmdText = "use master alter database [" + dbName + "] set single_user with rollback immediate restore database " + dbName + " from disk='" + openPath + "' with replace";

                using (SqlConnection sqlConn = new SqlConnection(dbConnString))
                using (SqlCommand cmd = new SqlCommand(cmdText, sqlConn))
                {
                    sqlConn.Open();
                    cmd.ExecuteNonQuery();
                    flag = 1;
                }
            }

        }

        //***************************************************************

        /// <summary>
        /// Attach I
        /// </summary>
        /// <param name="dbName">dbName</param>
        /// <param name="dbPath">dbPath</param>
        public void attach01(string dbName, string dbPath)
        {
            string cmdText = "exec sp_attach_db @dbname=N'" + dbName + "',@filename1=N'" + dbPath + dbName + ".mdf',@filename2=N'" + dbPath + dbName + "_log.ldf'";

            using (SqlConnection sqlConn = new SqlConnection(dbConnString))
            using (SqlCommand cmd = new SqlCommand(cmdText, sqlConn))
            {
                sqlConn.Open();
                cmd.ExecuteNonQuery();
                flag = 1;
            }

        }



        //*******************************************************************

        /// <summary>
        /// Detach I
        /// </summary>
        /// <param name="dbName">dbName</param>
        public void detach01(string dbName)
        {
            string cmdText = "use master Alter database [" + dbName + "] set OFFLINE with rollback immediate; Alter database [" + dbName + "] set SINGLE_USER with rollback immediate exec sp_detach_db '" + dbName + "'";

            using (SqlConnection sqlConn = new SqlConnection(dbConnString))
            using (SqlCommand cmd = new SqlCommand(cmdText, sqlConn))
            {
                sqlConn.Open();
                cmd.ExecuteNonQuery();
                flag = 1;
            }
        }



        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        /// <summary>
        /// Attach II
        /// </summary>
        /// <param name="dbName">dbPath</param>
        /// <param name="dbPath">dbName</param>
        public void attach02(string dbName, string dbPath)
        {
            string dbConnStringII = "Data Source=.;AttachDbFilename=" + dbPath + dbName + ".mdf" + ";Integrated Security=True";
            string cmdText = "if(1=1) print 'ok'";

            using (SqlConnection sqlConn = new SqlConnection(dbConnStringII))
            using (SqlCommand cmd = new SqlCommand(cmdText, sqlConn))
            {
                sqlConn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                    flag = 1;
            }

        }



        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        /// <summary>
        /// Detach II
        /// </summary>
        /// <param name="dbName">dbName</param>
        /// <param name="dbPath">dbPath</param>
        public void detach02(string dbName, string dbPath)
        {
            string fullName = "[" + dbPath + dbName + ".mdf" + "]";
            string cmdText = "use master Alter database " + fullName + " set OFFLINE with rollback immediate Alter database " + fullName + " set SINGLE_USER with rollback immediate exec sp_detach_db " + fullName;

            using (SqlConnection sqlConn = new SqlConnection(dbConnString))
            using (SqlCommand cmd = new SqlCommand(cmdText, sqlConn))
            {
                sqlConn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                    flag = 1;
            }

        }


        // Restart a Windows Service
        private void restartWindowsService(string serviceName)
        {
            ServiceController serviceController = new ServiceController(serviceName);
            try
            {
                if ((serviceController.Status.Equals(ServiceControllerStatus.Running)) || (serviceController.Status.Equals(ServiceControllerStatus.StartPending)))
                {
                    serviceController.Stop();
                    lblInfoMsg.Text += serviceName + " is Stopping ...\n";
                }
                serviceController.WaitForStatus(ServiceControllerStatus.Stopped);
                lblInfoMsg.Text += serviceName + " is Stopped ...\n";

                serviceController.Start();
                lblInfoMsg.Text += serviceName + " is Starting ...\n";

                serviceController.WaitForStatus(ServiceControllerStatus.Running);
                lblInfoMsg.Text += serviceName + " is Started ...\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        //Stop a Service
        public void stopService(string serviceName)
        {
            ServiceController[] sc = ServiceController.GetServices();

            foreach (ServiceController s in sc)
            {
                if (s.ServiceName.Equals(serviceName))
                {
                    if (s.Status.Equals(ServiceControllerStatus.Running))
                    {
                        s.Stop();
                    }

                    if (s.Status.Equals(ServiceControllerStatus.Paused))
                    {
                        s.Stop();
                    }

                    if (s.Status.Equals(ServiceControllerStatus.PausePending))
                    {
                        s.Stop();
                    }

                    if (s.Status.Equals(ServiceControllerStatus.StartPending))
                    {
                        s.Stop();
                    }

                    if (s.Status.Equals(ServiceControllerStatus.ContinuePending))
                    {
                        s.Stop();
                    }
                }
            }
        }


        //Start a Service
        public void startService(string serviceName)
        {
            ServiceController[] sc = ServiceController.GetServices();


            foreach (ServiceController s in sc)
            {
                if (s.ServiceName.Equals(serviceName))
                {

                    if (s.Status.Equals(ServiceControllerStatus.Stopped))
                    {
                        s.Start();
                    }

                    if (s.Status.Equals(ServiceControllerStatus.StopPending))
                    {
                        s.Start();
                    }

                    if (s.Status.Equals(ServiceControllerStatus.Paused))
                    {
                        s.Start();
                    }

                    if (s.Status.Equals(ServiceControllerStatus.PausePending))
                    {
                        s.Start();
                    }

                    if (s.Status.Equals(ServiceControllerStatus.ContinuePending))
                    {
                        s.Start();
                    }


                }
            }
        }




        //Restart Service (Stop + Start)
        public async void restartService(string serviceName)
        {
            stopService(serviceName);
            lblInfoMsg.Text += serviceName + " is Stopped ...\n";
            await Task.Delay(5000); // wait for delay Time Asynchronously ...

            startService(serviceName);


            lblInfoMsg.Text = serviceName + " is Restarted.\n";
        }


        public void setDb(string dbName, string state, string userAccess)
        {
            string cmdText = "use master Alter database " + dbName + " set " + state + " with rollback immediate Alter database " + dbName + " set " + userAccess + " with rollback immediate";

            using (SqlConnection sqlConn = new SqlConnection(dbConnString))
            using (SqlCommand cmd = new SqlCommand(cmdText, sqlConn))
            {
                sqlConn.Open();
                cmd.ExecuteNonQuery();
                sqlConn.Close();

                flag = 1;

                lblInfoMsg.Text = "Database is " + state + " and " + userAccess + "\n";
            }

        }







        // Check procedure Exists or Not Exists...
        public bool checkSpExists(string myProcedureName)
        {
            bool spExists = false;
            string cmdText = "select name from sys.objects where type='P' and name='" + myProcedureName + "'";

            using (SqlConnection sqlConn = new SqlConnection(dbConnString))
            using (SqlCommand cmd = new SqlCommand(cmdText, sqlConn))
            {
                sqlConn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        spExists = true;
                    }
                }
            }


            return spExists;
        }


        //Get attached user databases
        public string[] getAttachedDbNames(string myProcedureName)
        {
            var arrMyDbNames = new string[0];

            using (SqlConnection sqlConn = new SqlConnection(dbConnString))
            using (SqlDataAdapter sda = new SqlDataAdapter(myProcedureName, sqlConn))
            {
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;

                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    string myDbNames = dt.Rows[0].Field<string>(0).ToString();
                    arrMyDbNames = myDbNames.Split(',');
                }
            }

            return arrMyDbNames;
        }


        // Create Procedure Show Attached Dbs
        public bool createProcedure(string myProcedureName)
        {
            bool result = false;
            string dynamicCreateProcedure = @"CREATE procedure " + myProcedureName + @"

                as

                DECLARE @i int= 1;
                                DECLARE @maxDbId int;
                                DECLARE @query nvarchar(4000);
                                DECLARE @dbName nvarchar(500);
                                DECLARE @dbNames nvarchar(4000);

                                set @dbNames = '';
                                set @maxDbId = (select MAX(database_id) from sys.databases)

                while (@i <= @maxDbId)
                  Begin

                    while (Not Exists(select name from sys.databases where database_id = @i) )
		                set @i = @i + 1;

                                set @query = N'select @name2= name from sys.databases where database_id =' + CHAR(39) + STR(@i) + CHAR(39);

                                EXEC sp_executesql @query , N'@name2 nvarchar(500) output', @name2 = @dbName output;

                                IF(@dbName Not IN('master', 'tempdb', 'model', 'msdb', 'ReportServer', 'ReportServerTempDB'))

                       set @dbNames = @dbNames + @dbName + ',';

                                set @i = @i + 1;

                 End

                IF(RIGHT(@dbNames, 1) = ',')
                   set @dbNames = LEFT(@dbNames, LEN(@dbNames) - 1)

                select @dbNames; ";


            using (SqlConnection conn = new SqlConnection(dbConnString))
            using (SqlCommand cmd = new SqlCommand(dynamicCreateProcedure, conn))
            {
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    result = true; // indicates procedure has been created successfully
                }
            }

            return result;
        }



        //Show databases' names in ComboBox
        public void showDbNames()
        {
            string[] dbNameItems = null;
            string procedureName = "sp-getOnlyUserDbNames-dbManagerProject";

            if (checkSpExists(procedureName))
            {
                dbNameItems = getAttachedDbNames(procedureName);

                cmbDatabases.Text = "";
                cmbDatabases.Items.Clear();

                if (dbNameItems[0] != "")
                {
                    for (int i = 0; i < dbNameItems.Length; i++)
                    {
                        cmbDatabases.Items.Add(dbNameItems[i]);
                    }

                    changeComboBoxItems();
                }
                else
                {
                    dbNameItems = null;
                }


            }
            else
            {
                // when Procedure Not Exists! it will be Create ...
                createProcedure(procedureName);
                showDbNames();
            }

        }


        // Add or Remove comboBox Elements ...
        public void changeComboBoxItems()
        {
            if (cmbDatabases.Items.Count > 0)
            {

                cmbDatabases.SelectedText = cmbDatabases.Items[0].ToString();

                cmbDatabases.SelectedIndexChanged += (obj, arg) =>
                {
                    dbName = cmbDatabases.SelectedItem.ToString();

                };
            }
            else
            {
                cmbDatabases.SelectedText = dbName;
            }
        }


        //showing Messages
        public string showState()
        {
            string state = "";

            if (flag == 1)
            {
                switch (btnIndex)
                {
                    case 1:
                        state = state + dbName + " >> Attached in method I" + "\n";
                        break;

                    case 2:
                        state = state + dbName + " >> Detached in method I" + "\n";
                        break;

                    case 3:
                        state = state + dbName + " >> Attached in method II" + "\n";
                        break;

                    case 4:
                        state = state + dbName + " >> Detached  in method II" + "\n";
                        break;

                    case 5:
                        state = state + dbName + " >> Database Removed !" + "\n";
                        break;

                    case 6:
                        state = state + dbName + " >> Backup Succeed." + "\n";
                        break;

                    case 7:
                        state = state + dbName + " >> Restore Succeed." + "\n";
                        break;

                }
            }


            if (flag == 2)
                state = state + "Not need to Retry, maybe Already done. ";

            if (flag == -1)
                state = state + "Operation Not Succeed or Succeed with Error...\n Please Retry... if Not Succeed yet,\nPress Button Restart SQL Server...\nWait for a moments ...\nand then Retry...";

            return state;
        }


        //Swich to selected Index
        public void selectIndex()
        {
            keepSqlServerInRunnig("MSSQLSERVER");

            if (dbName != null)
            {
                string dbName02 = dbPath + dbName + ".mdf";

                switch (btnIndex)
                {
                    //Attach I
                    case 1:

                        if (!checkDbState(dbName))
                        {
                            attach01(dbName, dbPath);
                            showDbNames();
                        }
                        else
                            flag = 2;
                        break;

                    //Detach I
                    case 2:

                        if (checkDbState(dbName))
                        {
                            detach01(dbName);
                            showDbNames();

                        }
                        else
                            flag = 2;
                        break;

                    //Attach II
                    case 3:

                        if (!checkDbState(dbName02))
                        {
                            attach02(dbName, dbPath);
                            showDbNames();
                        }
                        else
                            flag = 2;
                        break;

                    //Detach II
                    case 4:

                        if (checkDbState(dbName02))
                        {
                            detach02(dbName, dbPath);
                            showDbNames();

                        }
                        else
                            flag = 2;
                        break;


                    //Remove Database!!!
                    case 5:

                        if (checkDbState(dbName))
                        {
                            DialogResult yes = MessageBox.Show("Are you sure you want to Remove Database " + dbName + " !!!?", "Remove Database", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                            if (yes == DialogResult.Yes)
                            {
                                RemoveDb(dbName);
                                showDbNames();
                            }
                        }
                        else
                            flag = 2;

                        if (checkDbState(dbName02))
                        {
                            DialogResult yes = MessageBox.Show("Are you sure you want to Remove Database " + dbName02 + " !!!?", "Remove Database", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                            if (yes == DialogResult.Yes)
                            {
                                RemoveDb(dbName02);
                            }
                        }
                        else
                            flag = 2;
                        break;


                    //Backup Database I
                    case 6:

                        if (checkDbState(dbName))
                        {
                            backupDb(dbName);
                        }

                        if (checkDbState(dbName02))
                        {
                            backupDb(dbName02);
                        }
                        break;


                    //Restore Database
                    case 7:

                        if (checkDbState(dbName))
                        {
                            restoreDb(dbName);
                        }

                        if (checkDbState(dbName02))
                        {
                            restoreDb(dbName02);
                        }
                        break;

                }

                lblInfoMsg.Text = showState();

                if (flag == -1)
                {
                    lblInfoMsg.Text = showState() + "\n" + "خطــــا در اجرای عملیات" + "\n";
                }

            }

            else
                lblInfoMsg.Text = showState() + "Database Name is not selected" + "\n";

        }



        //Restart SQLSERVER
        private void btnRestartSql_Click(object sender, EventArgs e)
        {
            string serviceName = "MSSQLSERVER";

            restartService(serviceName);

            //restartWindowsService(serviceName);
        }





        // get dbNames by Smo ...
        private List<string> getDbNames(string myServerName)
        {
            var dbList = new List<string>();
            var server = new Microsoft.SqlServer.Management.Smo.Server(myServerName);

            foreach (Database db in server.Databases)
            {
                dbList.Add(db.Name);
            }

            return dbList;
        }



        // Load Database Names in ComboBox
        private void loadDbNamesInComboBox(string instanceName)
        {
            cmbDatabases.Items.Clear();
            cmbDatabases.Text = "";

            var dbList = getDbNames(instanceName);

            foreach (var item in dbList)
            {
                cmbDatabases.Items.Add(item);
            }

            cmbDatabases.SelectedItem = cmbDatabases.Items[0];
        }





        // Accept Enter key for instance Names of SQL server ...
        private void getEnterKey(TextBox myTextBox)
        {
            myTextBox.KeyDown += (o, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnGetInstance_Click(o, e);

                }
            };
        }






        // \\\\\\\\\\\\\\\\\\\\\\   Events  \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

        private void Form1_Load(object sender, EventArgs e)
        {
            keepSqlServerInRunnig("MSSQLSERVER");

            showDbNames();

            getEnterKey(txtInstanceName);
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        //Add Database to List
        private void btnAddDbNameToList_Click(object sender, EventArgs e)
        {
            addDbNameToList();
            showDbNames();
        }


        //Attach
        private void btnAttach_Click(object sender, EventArgs e)
        {
            btnIndex = 1;
            selectIndex();
        }


        //Detach
        private void btnDetach_Click(object sender, EventArgs e)
        {
            btnIndex = 2;
            selectIndex();
        }


        //Attach II
        private void btnAttach02_Click(object sender, EventArgs e)
        {
            btnIndex = 3;
            selectIndex();
        }


        //Detach II
        private void btnDetach02_Click(object sender, EventArgs e)
        {
            btnIndex = 4;
            selectIndex();
        }


        //Remove
        private void btnRemoveDb_Click(object sender, EventArgs e)
        {
            btnIndex = 5;
            selectIndex();
        }


        //Backup
        private void btnBackup_Click(object sender, EventArgs e)
        {
            btnIndex = 6;
            selectIndex();
        }


        //Restore
        private void btnRestore_Click(object sender, EventArgs e)
        {
            btnIndex = 7;
            selectIndex();
        }



        // set Database in Offline state ...
        private void btnRestart02_Click(object sender, EventArgs e)
        {
            string myDbName;

            if (!string.IsNullOrEmpty(dbName))
            {
                myDbName = cmbDatabases.SelectedItem.ToString();

                if (checkDbState(myDbName))
                {
                    setDb(myDbName, "OFFLINE", "SINGLE_USER");
                }
            }

        }



        // btn get Instance name
        private void btnGetInstance_Click(object sender, EventArgs e)
        {
            keepSqlServerInRunnig("MSSQLSERVER");

            if (!string.IsNullOrEmpty(txtInstanceName.Text))
            {
                string instanceName = txtInstanceName.Text.ToUpper();

                if (instanceName == @".\SQLEXPRESS")
                {
                    loadDbNamesInComboBox(instanceName);
                }
                if (instanceName == ".")
                {
                    loadDbNamesInComboBox(instanceName);
                }
                if (instanceName == "192.168.1.177")
                {

                }

            }
        }


        // About
        private void btnAbout_Click(object sender, EventArgs e)
        {
            lblInfoMsg.Text = "Coding by Mohsen Kazemi" + "\n" +
                "Gmail : devCodePlus@gmail.com" + "\n\n" +
                "Please send your Opinions for me ..." + "\n" +
                "Thanks for Advanced." + "\n";
        }



    }
}


