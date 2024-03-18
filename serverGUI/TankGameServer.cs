using ConsoleControl;
using System;
using System.Diagnostics;
using System.IO.Pipes;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.Serialization.Formatters.Binary;
using System.Timers;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace serverGUI
{
    public struct Client
    {
        public IPEndPoint Address { get; set; }
        public string Name { get; set; }
        public string IP { get; set; }
        public Client(IPEndPoint addr, string ipAddress, string clientName)
        {
            Address = addr;
            IP = ipAddress;
            Name = clientName;
        }
    }
    public partial class TankGameServer : Form
    {
        private Process serverProcess;
        private bool running = false;
        private bool firstTime = true;
        private bool stopped = true;
        private List<Client> clients = new List<Client>();
        private System.Timers.Timer updateTimer;
        private FileSystemWatcher watcher;
        private string clientsFilePath = "clients.txt";

        public TankGameServer()
        {
            InitializeComponent();
            LoadClientsFromFile();
            InitializeFileWatcher();
            InitializeServer();
            SetFontSize(11);
        }

        private void InitializeServer()
        {
            serverProcess = new Process();

            string exeDirectory = AppDomain.CurrentDomain.BaseDirectory;

            string serverExePath = System.IO.Path.Combine(exeDirectory, "server.exe");

            if (!System.IO.File.Exists(serverExePath))
            {
                MessageBox.Show("Server executable not found! " + serverExePath);
                return;
            }

            serverProcess.StartInfo.FileName = serverExePath;
            serverProcess.StartInfo.UseShellExecute = false;
            serverProcess.StartInfo.RedirectStandardOutput = true;
            serverProcess.StartInfo.RedirectStandardInput = true;
            serverProcess.StartInfo.RedirectStandardError = true;
            serverProcess.StartInfo.CreateNoWindow = true;

            serverProcess.OutputDataReceived += ServerOutputHandler;
            serverProcess.ErrorDataReceived += ServerErrorHandler;

            try
            {
                serverProcess.Start();
                serverProcess.BeginOutputReadLine();
                serverProcess.BeginErrorReadLine();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error starting server: " + ex.Message);
            }
        }

        private void LoadClientsFromFile()
        {
            if (File.Exists(clientsFilePath))
            {
                try
                {
                    // Clear existing items in clientList
                    if (clientList.InvokeRequired)
                    {
                        clientList.Invoke((MethodInvoker)delegate { clientList.Items.Clear(); });
                    }
                    else
                    {
                        clientList.Items.Clear();
                    }

                    clients.Clear(); // Clear existing clients

                    using (StreamReader reader = new StreamReader(clientsFilePath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] parts = line.Split(' '); // Assuming each part is separated by space
                            if (parts.Length == 3)
                            {
                                IPAddress ip;
                                if (IPAddress.TryParse(parts[2], out ip))
                                {
                                    IPEndPoint endPoint = new IPEndPoint(ip, 0); // Assuming port is not provided in the file
                                    Client client = new Client(endPoint, parts[1], parts[2]);
                                    clients.Add(client);
                                    // Add client to the list box
                                    if (clientList.InvokeRequired)
                                    {
                                        clientList.Invoke((MethodInvoker)delegate { clientList.Items.Add(client.Name + " - " + client.IP); });
                                    }
                                    else
                                    {
                                        clientList.Items.Add(client.Name + " - " + client.IP);
                                    }
                                }
                                else
                                {
                                    AppendTextToConsole("Invalid IP address: " + parts[2]);
                                }
                            }
                            else
                            {
                                AppendTextToConsole("Invalid line format: " + line);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    AppendTextToConsole("Error loading clients: " + ex.Message);
                }
            }
            else
            {
                AppendTextToConsole("Clients file not found.");
            }
        }


        private void InitializeFileWatcher()
        {
            watcher = new FileSystemWatcher("C:\\Users\\Eric\\OneDrive\\CST4\\4995-Gaming\\serverGUI\\serverGUI\\bin\\Debug\\net6.0-windows");
            watcher.Filter = Path.GetFileName(clientsFilePath);
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.CreationTime;
            watcher.Changed += ClientsFileChanged;
            watcher.EnableRaisingEvents = true;

            // Load clients initially
            LoadClientsFromFile();
        }

        private void ClientsFileChanged(object sender, FileSystemEventArgs e)
        {
            // Reload clients from the file when it changes
            LoadClientsFromFile();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Stop.Enabled = false;
            SetPort.Enabled = false;
        }

        private void StartServer()
        {
            if (serverProcess != null && !serverProcess.HasExited)
            {
                try
                {
                    serverProcess.StandardInput.WriteLine("set_port:" + textBox1.Text); // Write the command
                    running = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error:" + ex.Message);
                }
            }
        }

        private void ServerOutputHandler(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                // Handle server output here
                AppendTextToConsole(e.Data);
            }
        }

        private void ServerErrorHandler(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                // Handle server errors here
                AppendTextToConsole("Error: " + e.Data);
            }
        }

        private void AppendTextToConsole(string text)
        {
            if (consoleControl1.InvokeRequired)
            {
                consoleControl1.Invoke((MethodInvoker)(() => consoleControl1.WriteOutput(text + Environment.NewLine, Color.White)));
            }
            else
            {
                consoleControl1.WriteOutput(text + Environment.NewLine, Color.White);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopServer();
        }

        private void StopServer()
        {
            if (serverProcess != null && !serverProcess.HasExited)
            {
                try
                {
                    serverProcess.StandardInput.WriteLine("designation_eric"); // Write the command
                    serverProcess.StandardInput.Flush(); // Flush the standard input stream
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error exiting:" + ex.Message);
                }
            }
            LoadClientsFromFile();
            stopped = true;
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            StopServer();
            stopped = true;
            running = false;
            firstTime = false;
            Stop.Enabled = false;
            SetPort.Enabled = true;
        }

        private void SetPort_Click(object sender, EventArgs e)
        {
            if (stopped && !firstTime)
            {
                InitializeServer();
            }
            if (textBox1.Text.Length > 0)
            {
                StartServer();
                Stop.Enabled = true;
                SetPort.Enabled = false;
            }
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                if (networkInterface.OperationalStatus == OperationalStatus.Up &&
                    networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                    networkInterface.NetworkInterfaceType != NetworkInterfaceType.Tunnel)
                {
                    IPInterfaceProperties properties = networkInterface.GetIPProperties();
                    foreach (UnicastIPAddressInformation addressInfo in properties.UnicastAddresses)
                    {
                        if (addressInfo.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            label4.Text = "IP: " + addressInfo.Address.ToString();
                        }
                    }
                }
            }
        }
        private void SetFontSize(int size)
        {
            consoleControl1.InternalRichTextBox.Font = new Font(consoleControl1.InternalRichTextBox.Font.FontFamily, size);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && !running && stopped)
            {
                SetPort.Enabled = true;
            }
            else
            {
                SetPort.Enabled = false;
            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
