using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.IO;
using System.Xml.Linq;

namespace DualSplit
{
    public partial class Main : Form
    {
        Label[,] splitTimes;
        Label[,] diffTimes;
        Label[] splitTexts;

        Stopwatch clockA = new Stopwatch();
        Stopwatch clockB = new Stopwatch();

        TimeSpan adjustmentA = new TimeSpan();
        TimeSpan adjustmentB = new TimeSpan();

        TimeSpan[,] splitSpan;

        int splits = 0;

        int splitsDoneA = 0;
        int splitsDoneB = 0;

        private Socket m_sock;                      // Server connection
        private byte[] m_byBuff = new byte[256];    // Recieved data buffer
        private ArrayList m_aryClients = new ArrayList();	// List of Client Connections

        bool server = false;
        bool client = false;

        string gameFile = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "FF1PSP.xml");

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                using (TextReader reader = File.OpenText("lastDQ12.txt"))
                {
                    txtIP.Text = reader.ReadLine();
                    txtPort.Text = reader.ReadLine();

                    gameFile = reader.ReadLine();
                }
            }
            catch
            {
                // ignore error
            }

            //this.AutoScaleMode = AutoScaleMode.None;
            loadGame();

            //this.Height = 70 + (splits * 30) + 60;

            this.Left = 200;
            this.Top = 200;

            try
            {
                using (TextReader reader = File.OpenText("dualSettings.txt"))
                {
                    txtIP.Text = reader.ReadLine();
                    txtPort.Text = reader.ReadLine();
                }
            }
            catch
            {
                // ignore error
            }
        }

        public void startClocks()
        {
            clockA.Start();
            clockB.Start();
            timer1.Enabled = true;
            timer2.Enabled = true;
        }

        public void adjustTime(bool stopWatchA, bool subtract)
        {
            {
                if (subtract)
                    if (stopWatchA)
                        adjustmentA = adjustmentA.Subtract(new TimeSpan(0, 0, 0, 1, 0));
                    else
                        adjustmentB = adjustmentB.Subtract(new TimeSpan(0, 0, 0, 1, 0));
                else
                    if (stopWatchA)
                    adjustmentA = adjustmentA.Add(new TimeSpan(0, 0, 0, 1, 0));
                else
                    adjustmentB = adjustmentB.Add(new TimeSpan(0, 0, 0, 1, 0));
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan tsA = clockA.Elapsed + adjustmentA;
            lblTimerA.Text = Math.Floor(tsA.TotalHours) + ":" + Math.Floor((double)tsA.Minutes).ToString("00") + ":" + Math.Floor((double)tsA.Seconds).ToString("00") + "." + tsA.Milliseconds / 100;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            TimeSpan tsB = clockB.Elapsed + adjustmentB;
            lblTimerB.Text = Math.Floor(tsB.TotalHours) + ":" + Math.Floor((double)tsB.Minutes).ToString("00") + ":" + Math.Floor((double)tsB.Seconds).ToString("00") + "." + tsB.Milliseconds / 100;
        }

        public void split(bool firstPlayer, byte[] aryInfo = null)
        {
            if ((firstPlayer && splitsDoneA == splits) || (!firstPlayer && splitsDoneB == splits)) return;
            int split = (firstPlayer ? splitsDoneA : splitsDoneB);

            TimeSpan ts = (firstPlayer ? clockA.Elapsed + adjustmentA : clockB.Elapsed + adjustmentB);
            if (aryInfo != null)
                ts = new TimeSpan(aryInfo[1], aryInfo[2], aryInfo[3]);

            splitSpan[firstPlayer ? 0 : 1, split] = ts;

            splitTimes[firstPlayer ? 0 : 1, split].Text = Math.Floor(ts.TotalHours) + ":" + Math.Floor((double)ts.Minutes).ToString("00") + ":" + Math.Floor((double)ts.Seconds).ToString("00");

            calcDiffs(firstPlayer ? splitsDoneA : splitsDoneB);

            if (firstPlayer) splitsDoneA++; else splitsDoneB++;
            if (splitsDoneA == splits) timer1.Enabled = false;
            if (splitsDoneB == splits) timer2.Enabled = false;
        }

        public void calcDiffs(int split)
        {
            if (splitSpan[0, split].TotalSeconds != 0 && splitSpan[1, split].TotalSeconds != 0)
            {
                TimeSpan diff = splitSpan[0, split].Subtract(splitSpan[1, split]);
                if (diff.TotalSeconds < 0)
                {
                    diff = splitSpan[1, split].Subtract(splitSpan[0, split]);
                    if (diff.TotalSeconds < 60)
                    {
                        diffTimes[0, split].Text = "-" + Math.Floor((double)diff.Seconds).ToString("00") + "." + diff.Milliseconds / 100;
                        diffTimes[0, split].ForeColor = Color.LawnGreen;
                        diffTimes[1, split].Text = "+" + Math.Floor((double)diff.Seconds).ToString("00") + "." + diff.Milliseconds / 100;
                        diffTimes[1, split].ForeColor = Color.LightCoral;
                    }
                    else
                    {
                        diffTimes[0, split].Text = "-" + Math.Floor(diff.TotalMinutes).ToString("00") + ":" + Math.Floor((double)diff.Seconds).ToString("00");
                        diffTimes[0, split].ForeColor = Color.LawnGreen;
                        diffTimes[1, split].Text = "+" + Math.Floor(diff.TotalMinutes).ToString("00") + ":" + Math.Floor((double)diff.Seconds).ToString("00");
                        diffTimes[1, split].ForeColor = Color.LightCoral;
                    }
                }
                else
                {
                    if (diff.TotalSeconds < 60)
                    {
                        diffTimes[0, split].Text = "+" + Math.Floor((double)diff.Seconds).ToString("00") + "." + diff.Milliseconds / 100;
                        diffTimes[0, split].ForeColor = Color.LightCoral;
                        diffTimes[1, split].Text = "-" + Math.Floor((double)diff.Seconds).ToString("00") + "." + diff.Milliseconds / 100;
                        diffTimes[1, split].ForeColor = Color.LawnGreen;
                    }
                    else
                    {
                        diffTimes[0, split].Text = "+" + Math.Floor(diff.TotalMinutes).ToString("00") + ":" + Math.Floor((double)diff.Seconds).ToString("00");
                        diffTimes[0, split].ForeColor = Color.LightCoral;
                        diffTimes[1, split].Text = "-" + Math.Floor(diff.TotalMinutes).ToString("00") + ":" + Math.Floor((double)diff.Seconds).ToString("00");
                        diffTimes[1, split].ForeColor = Color.LawnGreen;
                    }
                }
            }
            else
            {
                diffTimes[0, split].Text = "";
                diffTimes[1, split].Text = "";
            }
        }

        public void reverseSplit(bool firstPlayer, byte[] aryInfo = null)
        {
            if ((firstPlayer && splitsDoneA == 0) || (!firstPlayer && splitsDoneB == 0)) return;

            int player = (firstPlayer ? 0 : 1);
            if (firstPlayer) splitsDoneA--; else splitsDoneB--;
            int split = (firstPlayer ? splitsDoneA : splitsDoneB);

            splitSpan[player, split] = new TimeSpan();

            diffTimes[0, split].Text = "";
            diffTimes[1, split].Text = "";

            splitTimes[player, split].Text = "";

            if (firstPlayer) timer1.Enabled = true; else timer2.Enabled = true;
        }

        public void adjustSplit(bool firstPlayer, bool plus, bool ten, byte[] aryInfo = null)
        {
            int player = (firstPlayer ? 0 : 1);
            int split = (firstPlayer ? splitsDoneA - 1 : splitsDoneB - 1);

            splitSpan[player, split] = splitSpan[player, split].Add(new TimeSpan(0, 0, (plus ? (ten ? 10 : 1) : (ten ? -10 : -1))));
            TimeSpan tsA = splitSpan[player, split];
            splitTimes[player, split].Text = Math.Floor(tsA.TotalHours) + ":" + Math.Floor((double)tsA.Minutes).ToString("00") + ":" + Math.Floor((double)tsA.Seconds).ToString("00");

            calcDiffs(split);
        }

        public void resetClocks()
        {
            timer1.Enabled = false;
            timer2.Enabled = false;

            adjustmentA = new TimeSpan();
            adjustmentB = new TimeSpan();
            lblTimerA.Text = lblTimerB.Text = "0:00:00.0";

            for (int i = 0; i < 2; i++)
                for (int j = 0; j < splits; j++)
                {
                    splitSpan[i, j] = new TimeSpan();
                    splitTimes[i, j].Text = "";
                    diffTimes[i, j].Text = "";
                }

            clockA.Reset();
            clockB.Reset();

            splitsDoneA = 0;
            splitsDoneB = 0;
        }

        private void startClocks_Click(object sender, EventArgs e)
        {
            if (client)
                sendBytes(new byte[] { 0x01 });
            else
                startClocks();
        }

        private void adjustClocks(object sender, EventArgs e)
        {
            Button actualSender = (Button)sender;
            if (client == true)
            {
                sendBytes(new byte[] { (byte)(actualSender.Name == "pOneA" ? 0x30 : actualSender.Name == "mOneA" ? 0x31 : actualSender.Name == "pOneB" ? 0x32 : 0x33) });
            } else
            {
                adjustTime(actualSender.Name == "pOneA" || actualSender.Name == "mOneA", actualSender.Name == "mOneA" || actualSender.Name == "mOneB");
            }
        }

        private void splitA_Click(object sender, EventArgs e)
        {
            if (client == true)
                sendBytes(new byte[] { 0x03 });
            else
                split(true);
        }

        private void splitB_Click(object sender, EventArgs e)
        {
            if (client == true)
                sendBytes(new byte[] { 0x04 });
            else
                split(false);
        }

        private void reverseA_Click(object sender, EventArgs e)
        {
            if (client == true)
                sendBytes(new byte[] { 0x05 });
            else
                reverseSplit(true);
        }

        private void reverseB_Click(object sender, EventArgs e)
        {
            if (client == true)
                sendBytes(new byte[] { 0x06 });
            else
                reverseSplit(false);
        }

        private void splitPOneA_Click(object sender, EventArgs e)
        {
            if (client == true)
                sendBytes(new byte[] { 0x11 });
            else
                adjustSplit(true, true, false);
        }

        private void splitMOneA_Click(object sender, EventArgs e)
        {
            if (client == true)
                sendBytes(new byte[] { 0x10 });
            else
                adjustSplit(true, false, false);
        }

        private void splitPOneB_Click(object sender, EventArgs e)
        {
            if (client == true)
                sendBytes(new byte[] { 0x21 });
            else
                adjustSplit(false, true, false);
        }

        private void splitMOneB_Click(object sender, EventArgs e)
        {
            if (client == true)
                sendBytes(new byte[] { 0x20 });
            else
                adjustSplit(false, false, false);
        }

        private void btnResetClocks_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "DualSplit", MessageBoxButtons.YesNo) == DialogResult.Yes)
                if (client == true)
                    sendBytes(new byte[] { 0x02 });
                else
                    resetClocks();
        }

        private void txtPlayerB_Leave(object sender, EventArgs e)
        {
            lblPlayerB.Text = txtPlayerB.Text;
        }

        private void txtPlayerA_TextChanged(object sender, EventArgs e)
        {
            lblPlayerA.Text = txtPlayerA.Text;
        }

        private void txtCommentary_Leave(object sender, EventArgs e)
        {
            lblCommentary.Text = "Commentary:  " + txtCommentary.Text;
        }

        private void splitPTenA_Click(object sender, EventArgs e)
        {
            if (client == true)
                sendBytes(new byte[] { 0x13 });
            else
                adjustSplit(true, true, true);
        }

        private void splitMTenA_Click(object sender, EventArgs e)
        {
            if (client == true)
                sendBytes(new byte[] { 0x12 });
            else
                adjustSplit(true, false, true);
        }

        private void splitPTenB_Click(object sender, EventArgs e)
        {
            if (client == true)
                sendBytes(new byte[] { 0x23 });
            else
                adjustSplit(false, true, true);
        }

        private void splitMTenB_Click(object sender, EventArgs e)
        {
            if (client == true)
                sendBytes(new byte[] { 0x22 });
            else
                adjustSplit(false, false, true);
        }

        private void cmdStartServer_Click(object sender, EventArgs e)
        {
            IPAddress[] aryLocalAddr = null;
            String strHostName = "";
            try
            {
                // NOTE: DNS lookups are nice and all but quite time consuming.
                strHostName = Dns.GetHostName();
                IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
                aryLocalAddr = ipEntry.AddressList;
            }
            catch (Exception ex)
            {
                listBox1.Items.Insert(0, "Error trying to get local address " + ex.Message);
            }

            // Verify we got an IP address. Tell the user if we did
            if (aryLocalAddr == null || aryLocalAddr.Length < 1)
            {
                listBox1.Items.Insert(0, "Unable to get local address");
                return;
            }

            // Create the listener socket in this machines IP address
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress actual = null;
            int lnI = -1;
            while (actual == null || actual.AddressFamily != AddressFamily.InterNetwork)
            {
                lnI++;
                actual = aryLocalAddr[lnI];
            }
            listener.Bind(new IPEndPoint(aryLocalAddr[lnI], Convert.ToInt32(txtPort.Text)));
            listener.Listen(10);

            // Setup a callback to be notified of connection requests
            listener.BeginAccept(new AsyncCallback(OnConnectRequest), listener);
            listBox1.Items.Insert(0, "Accepting connections");
            server = true;
        }

        /// <summary>
        /// Callback used when a client requests a connection. 
        /// Accpet the connection, adding it to our list and setup to 
        /// accept more connections.
        /// </summary>
        /// <param name="ar"></param>
        public void OnConnectRequest(IAsyncResult ar)
        {
            Socket listener = (Socket)ar.AsyncState;
            NewConnection(listener.EndAccept(ar));
            listener.BeginAccept(new AsyncCallback(OnConnectRequest), listener);
        }

        /// <summary>
        /// Add the given connection to our list of clients
        /// Note we have a new friend
        /// Send a welcome to the new client
        /// Setup a callback to recieve data
        /// </summary>
        /// <param name="sockClient">Connection to keep</param>
        public void NewConnection(Socket sockClient)
        {
            // Program blocks on Accept() until a client connects.
            SocketChatClient client = new SocketChatClient(sockClient);
            m_aryClients.Add(client);
            this.BeginInvoke(new MethodInvoker(delegate
            {
                listBox1.Items.Insert(0, "Client " + client.Sock.RemoteEndPoint + " joined");
            }));

            // Get current date and time.
            DateTime now = DateTime.Now;
            String strDateLine = "Welcome " + now.ToString("G") + "\n\r";

            // Convert to byte array and send.
            Byte[] byteDateLine = System.Text.Encoding.ASCII.GetBytes(strDateLine.ToCharArray());
            client.Sock.Send(byteDateLine, byteDateLine.Length, 0);

            client.SetupRecieveCallback(this);
        }

        /// <summary>
        /// Get the new data and send it out to all other connections. 
        /// Note: If not data was recieved the connection has probably 
        /// died.
        /// </summary>
        /// <param name="ar"></param>
        public void OnRecievedDataServer(IAsyncResult ar)
        {
            SocketChatClient client = (SocketChatClient)ar.AsyncState;
            byte[] aryRet = client.GetRecievedData(ar);

            // If no data was recieved then the connection is probably dead
            if (aryRet.Length < 1)
            {
                this.BeginInvoke(new MethodInvoker(delegate
                {
                    try
                    {
                        listBox1.Items.Insert(0, "Client " + client.Sock.RemoteEndPoint + " disconnected");
                    }
                    catch
                    {

                    }
                }));
                client.Sock.Close();
                m_aryClients.Remove(client);
                return;
            }

            // Do something with the received data
            this.BeginInvoke(new MethodInvoker(delegate
            {
                listBox1.Items.Insert(0, "Server Data Get:  " + aryRet[0]);
                cleanListBox();

                if (server == true)
                {
                    if (aryRet[0] == 0x01) startClocks();
                    if (aryRet[0] == 0x02) resetClocks();
                    if (aryRet[0] == 0x03) split(true);
                    if (aryRet[0] == 0x04) split(false);
                    if (aryRet[0] == 0x05) reverseSplit(true);
                    if (aryRet[0] == 0x06) reverseSplit(false);
                    if (aryRet[0] == 0x10) adjustSplit(true, false, false);
                    if (aryRet[0] == 0x11) adjustSplit(true, true, false);
                    if (aryRet[0] == 0x12) adjustSplit(true, false, true);
                    if (aryRet[0] == 0x13) adjustSplit(true, true, true);
                    if (aryRet[0] == 0x20) adjustSplit(false, false, false);
                    if (aryRet[0] == 0x21) adjustSplit(false, true, false);
                    if (aryRet[0] == 0x22) adjustSplit(false, false, true);
                    if (aryRet[0] == 0x23) adjustSplit(false, true, true);
                    if (aryRet[0] == 0x30) adjustTime(true, false);
                    if (aryRet[0] == 0x31) adjustTime(true, true);
                    if (aryRet[0] == 0x32) adjustTime(false, false);
                    if (aryRet[0] == 0x33) adjustTime(false, true);
                }
                else
                {
                    if (aryRet[0] == 0x01) startClocks();
                    if (aryRet[0] == 0x02) resetClocks();
                    if (aryRet[0] == 0x03) split(true);
                    if (aryRet[0] == 0x04) split(false);
                    if (aryRet[0] == 0x05) reverseSplit(true);
                    if (aryRet[0] == 0x06) reverseSplit(false);
                    if (aryRet[0] == 0x10) adjustSplit(true, false, false);
                    if (aryRet[0] == 0x11) adjustSplit(true, true, false);
                    if (aryRet[0] == 0x12) adjustSplit(true, false, true);
                    if (aryRet[0] == 0x13) adjustSplit(true, true, true);
                    if (aryRet[0] == 0x20) adjustSplit(false, false, false);
                    if (aryRet[0] == 0x21) adjustSplit(false, true, false);
                    if (aryRet[0] == 0x22) adjustSplit(false, false, true);
                    if (aryRet[0] == 0x23) adjustSplit(false, true, true);
                    if (aryRet[0] == 0x30) adjustTime(true, false);
                    if (aryRet[0] == 0x31) adjustTime(true, true);
                    if (aryRet[0] == 0x32) adjustTime(false, false);
                    if (aryRet[0] == 0x33) adjustTime(false, true);
                }
            }));

            //Send the recieved data to all clients(including sender for echo)
            foreach (SocketChatClient clientSend in m_aryClients)
            {
                try
                {
                    clientSend.Sock.Send(aryRet);
                }
                catch
                {
                    // If the send fails the close the connection
                    Console.WriteLine("Send to client {0} failed", client.Sock.RemoteEndPoint);
                    clientSend.Sock.Close();
                    m_aryClients.Remove(client);
                    return;
                }
            }
            client.SetupRecieveCallback(this);
        }

        // **************************************************************************************

        private void cmdConnect_Click(object sender, EventArgs e)
        {
            try
            {
                // Close the socket if it is still open
                if (m_sock != null && m_sock.Connected)
                {
                    m_sock.Shutdown(SocketShutdown.Both);
                    System.Threading.Thread.Sleep(10);
                    m_sock.Close();
                }

                // Create the socket object
                m_sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // Define the Server address and port
                IPEndPoint epServer = new IPEndPoint(IPAddress.Parse(txtIP.Text), Convert.ToInt32(txtPort.Text));

                // Connect to server non-Blocking method
                m_sock.Blocking = false;
                AsyncCallback onconnect = new AsyncCallback(OnConnect);
                m_sock.BeginConnect(epServer, onconnect, m_sock);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Server Connect failed!");
            }
        }

        public void OnConnect(IAsyncResult ar)
        {
            // Socket was the passed in object
            Socket sock = (Socket)ar.AsyncState;

            // Check if we were sucessfull
            try
            {
                //sock.EndConnect( ar );
                if (sock.Connected)
                    SetupRecieveCallback(sock);
                else
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        MessageBox.Show(this, "Unable to connect to remote machine", "Connect Failed!");
                    }));
                }
            }
            catch (Exception ex)
            {
                this.BeginInvoke(new MethodInvoker(delegate
                {
                    MessageBox.Show(this, ex.Message, "Unusual error during Connect!");
                }));
            }
        }

        /// <summary>
        /// Setup the callback for recieved data and loss of conneciton
        /// </summary>
        public void SetupRecieveCallback(Socket sock)
        {
            try
            {
                AsyncCallback recieveData = new AsyncCallback(OnRecievedData);
                sock.BeginReceive(m_byBuff, 0, m_byBuff.Length, SocketFlags.None, recieveData, sock);
                listBox1.Items.Insert(0, "Connection successful");
                client = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Setup Recieve Callback failed!");
            }
        }

        /// <summary>
        /// Get the new data and send it out to all other connections. 
        /// Note: If not data was recieved the connection has probably 
        /// died.
        /// </summary>
        /// <param name="ar"></param>
        public void OnRecievedData(IAsyncResult ar)
        {
            // Socket was the passed in object
            Socket sock = (Socket)ar.AsyncState;

            // Check if we got any data
            try
            {
                int nBytesRec = sock.EndReceive(ar);
                if (nBytesRec > 0)
                {
                    // Do something with the data passed.

                    // Wrote the data to the List
                    string sRecieved = Encoding.ASCII.GetString(m_byBuff, 0, nBytesRec);

                    // If in server mode, receive commands from client... 01 = Start clock, 02 = Reset Clock, 03 = Split A, 04 = Split B, 05 = Reverse A, 06 = Reverse B, 10 = Split A -1 sec, 11 = Split A +1 sec, 12 = Split A -10 sec, 13 = Split A +10 sec,
                    // 20 = Split B -1 sec, 21 = Split B +1 sec, 22 = Split B -10 sec, 23 = Split B +10 sec, 30 = Clock A +1 sec, 31 = Clock A -1 sec, 32 = Clock B +1 sec, 33 = Clock B -1 sec
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        listBox1.Items.Insert(0, "Client Data Get:  " + m_byBuff[0]);
                        cleanListBox();

                        if (server == true)
                        {
                            if (m_byBuff[0] == 0x01) startClocks();
                            if (m_byBuff[0] == 0x02) resetClocks();
                            if (m_byBuff[0] == 0x03) split(true);
                            if (m_byBuff[0] == 0x04) split(false);
                            if (m_byBuff[0] == 0x05) reverseSplit(true);
                            if (m_byBuff[0] == 0x06) reverseSplit(false);
                            if (m_byBuff[0] == 0x10) adjustSplit(true, false, false);
                            if (m_byBuff[0] == 0x11) adjustSplit(true, true, false);
                            if (m_byBuff[0] == 0x12) adjustSplit(true, false, true);
                            if (m_byBuff[0] == 0x13) adjustSplit(true, true, true);
                            if (m_byBuff[0] == 0x20) adjustSplit(false, false, false);
                            if (m_byBuff[0] == 0x21) adjustSplit(false, true, false);
                            if (m_byBuff[0] == 0x22) adjustSplit(false, false, true);
                            if (m_byBuff[0] == 0x23) adjustSplit(false, true, true);
                            if (m_byBuff[0] == 0x30) adjustTime(true, false);
                            if (m_byBuff[0] == 0x30) adjustTime(true, false);
                            if (m_byBuff[0] == 0x30) adjustTime(true, false);
                            if (m_byBuff[0] == 0x31) adjustTime(true, true);
                            if (m_byBuff[0] == 0x32) adjustTime(false, false);
                            if (m_byBuff[0] == 0x33) adjustTime(false, true);
                        }
                        else
                        {
                            if (m_byBuff[0] == 0x01) startClocks();
                            if (m_byBuff[0] == 0x02) resetClocks();
                            if (m_byBuff[0] == 0x03) split(true);
                            if (m_byBuff[0] == 0x04) split(false);
                            if (m_byBuff[0] == 0x05) reverseSplit(true);
                            if (m_byBuff[0] == 0x06) reverseSplit(false);
                            if (m_byBuff[0] == 0x10) adjustSplit(true, false, false);
                            if (m_byBuff[0] == 0x11) adjustSplit(true, true, false);
                            if (m_byBuff[0] == 0x12) adjustSplit(true, false, true);
                            if (m_byBuff[0] == 0x13) adjustSplit(true, true, true);
                            if (m_byBuff[0] == 0x20) adjustSplit(false, false, false);
                            if (m_byBuff[0] == 0x21) adjustSplit(false, true, false);
                            if (m_byBuff[0] == 0x22) adjustSplit(false, false, true);
                            if (m_byBuff[0] == 0x23) adjustSplit(false, true, true);
                            if (m_byBuff[0] == 0x30) adjustTime(true, false);
                            if (m_byBuff[0] == 0x31) adjustTime(true, true);
                            if (m_byBuff[0] == 0x32) adjustTime(false, false);
                            if (m_byBuff[0] == 0x33) adjustTime(false, true);
                        }
                    }));

                    //// WARNING : The following line is NOT thread safe. Invoke is
                    //// m_lbRecievedData.Items.Add( sRecieved );
                    //Invoke(m_AddMessage, new string[] { sRecieved });

                    // If the connection is still usable restablish the callback
                    SetupRecieveCallback(sock);
                }
                else
                {
                    // If no data was recieved then the connection is probably dead
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        listBox1.Items.Insert(0, "Client " + sock.RemoteEndPoint + ", disconnected");
                    }));

                    sock.Shutdown(SocketShutdown.Both);
                    sock.Close();
                }
            }
            catch (Exception ex)
            {
                this.BeginInvoke(new MethodInvoker(delegate
                {
                    MessageBox.Show(this, ex.Message, "Unusual error during Recieve!");
                }));
            }
        }

        /// <summary>
        /// Send the Message in the Message area. Only do this if we are connected
        /// </summary>
        private void sendBytes(byte[] bytesToSend)
        {
            // Check we are connected
            if (m_sock == null || !m_sock.Connected)
            {
                MessageBox.Show(this, "Must be connected to Send a message");
                return;
            }

            // Read the message from the text box and send it
            try
            {
                // Convert to byte array and send.
                //Byte[] byteDateLine = Encoding.ASCII.GetBytes("test message".ToCharArray());
                m_sock.Send(bytesToSend, bytesToSend.Length, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Send Message Failed!");
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (StreamWriter writer = File.CreateText("dualSettings.txt"))
            {
                writer.WriteLine(txtIP.Text);
                writer.WriteLine(txtPort.Text);
                writer.WriteLine(gameFile);
            }
        }

        private void cleanListBox()
        {
            while (listBox1.Items.Count > 50)
                listBox1.Items.RemoveAt(50);
        }

        private void btnChooseGame_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                gameFile = openFileDialog1.FileName;
                loadGame();
            }
        }

        private void loadGame()
        {
            XDocument gameXML = XDocument.Load(gameFile);
            XElement game = gameXML.Root.Element("game");
            lblGameName.Text = "Game:  " + gameXML.Element("game").Attribute("name").Value;
            string gameFont = gameXML.Element("game").Attribute("Font").Value;

            for (int i = 0; i < splits; i++)
            {
                Controls.Remove(splitTexts[i]);
                for (int j = 0; j < 2; j++)
                {
                    Controls.Remove(splitTimes[j, i]);
                    Controls.Remove(diffTimes[j, i]);
                }
            }

            splits = gameXML.Descendants("split").Count();

            if (gameXML.Descendants("playerA").First().Attribute("visible").Value == "false")
                lblPlayerA.Visible = false;
            else
            {

            }
            if (gameXML.Descendants("playerB").First().Attribute("visible").Value == "false")
                lblPlayerB.Visible = false;
            else
            {

            }
            if (gameXML.Descendants("mic").First().Attribute("visible").Value == "false")
                lblCommentary.Visible = false;
            else
            {

            }

            lblTimerA.Font = new Font(gameFont, Convert.ToInt32(gameXML.Descendants("clockA").First().Attribute("fontSize").Value));
            lblTimerB.Font = new Font(gameFont, Convert.ToInt32(gameXML.Descendants("clockA").First().Attribute("fontSize").Value));
            lblTimerA.Left = Convert.ToInt32(gameXML.Descendants("clockA").First().Attribute("locX").Value);
            lblTimerB.Left = Convert.ToInt32(gameXML.Descendants("clockB").First().Attribute("locX").Value);
            lblTimerA.Top = Convert.ToInt32(gameXML.Descendants("clockA").First().Attribute("locY").Value);
            lblTimerB.Top = Convert.ToInt32(gameXML.Descendants("clockB").First().Attribute("locY").Value);

            Font timeFont = new Font(gameFont, Convert.ToInt32(gameXML.Descendants("times").First().Attribute("fontSize").Value));
            int timeALeft = Convert.ToInt32(gameXML.Descendants("times").First().Attribute("aLocX").Value);
            int timeBLeft = Convert.ToInt32(gameXML.Descendants("times").First().Attribute("bLocX").Value);

            Font diffFont = new Font(gameFont, Convert.ToInt32(gameXML.Descendants("diffs").First().Attribute("fontSize").Value));
            int diffALeft = Convert.ToInt32(gameXML.Descendants("diffs").First().Attribute("aLocX").Value);
            int diffBLeft = Convert.ToInt32(gameXML.Descendants("diffs").First().Attribute("bLocX").Value);

            Font titleFont = new Font(gameFont, Convert.ToInt32(gameXML.Descendants("title").First().Attribute("fontSize").Value));
            int titleLeft = Convert.ToInt32(gameXML.Descendants("title").First().Attribute("aLocX").Value);

            Font splitFont = new Font(gameFont, Convert.ToInt32(gameXML.Descendants("splits").First().Attribute("fontSize").Value));
            int splitYStart = Convert.ToInt32(gameXML.Descendants("splits").First().Attribute("yStart").Value);
            int splitYGap = Convert.ToInt32(gameXML.Descendants("splits").First().Attribute("yGap").Value);

            splitTexts = new Label[splits];
            splitTimes = new Label[2, splits];
            diffTimes = new Label[2, splits];
            splitSpan = new TimeSpan[2, splits];

            for (int i = 0; i < 2; i++)
                for (int j = 0; j < splits; j++)
                {
                    splitSpan[i, j] = new TimeSpan();

                    splitTimes[i, j] = new Label();
                    diffTimes[i, j] = new Label();

                    splitTimes[i, j].Left = (i == 0 ? timeALeft : timeBLeft);
                    splitTimes[i, j].Top = splitYStart + (j * splitYGap);
                    splitTimes[i, j].ForeColor = Color.White;
                    splitTimes[i, j].Visible = true;
                    splitTimes[i, j].Text = "";
                    splitTimes[i, j].AutoSize = true;
                    splitTimes[i, j].Font = splitFont;

                    Controls.Add(splitTimes[i, j]);

                    diffTimes[i, j].Left = (i == 0 ? diffALeft : diffBLeft);
                    diffTimes[i, j].Top = splitYStart + (j * splitYGap);
                    diffTimes[i, j].ForeColor = Color.White;
                    diffTimes[i, j].Visible = true;
                    diffTimes[i, j].Text = "";
                    diffTimes[i, j].AutoSize = true;
                    diffTimes[i, j].Font = splitFont;
                    Controls.Add(diffTimes[i, j]);
                }

            for (int i = 0; i < splits; i++)
            {
                splitTexts[i] = new Label();
                splitTexts[i].Text = gameXML.Descendants("split").Skip(i).First().Attribute("name").Value;
                splitTexts[i].Left = titleLeft;
                splitTexts[i].Top = splitYStart + (i * splitYGap);
                splitTexts[i].AutoSize = true;
                splitTexts[i].Font = splitFont;
                splitTexts[i].ForeColor = Color.White;
                Controls.Add(splitTexts[i]);
            }
        }
    }

    /// <summary>
    /// Class holding information and buffers for the Client socket connection
    /// </summary>
    internal class SocketChatClient
    {
        private Socket m_sock;                      // Connection to the client
        private byte[] m_byBuff = new byte[50];     // Receive data buffer
                                                    /// <summary>
                                                    /// Constructor
                                                    /// </summary>
                                                    /// <param name="sock">client socket conneciton this object represents</param>
        public SocketChatClient(Socket sock)
        {
            m_sock = sock;
        }

        // Readonly access
        public Socket Sock
        {
            get { return m_sock; }
        }

        /// <summary>
        /// Setup the callback for recieved data and loss of conneciton
        /// </summary>
        /// <param name="app"></param>
        public void SetupRecieveCallback(Main app)
        {
            try
            {
                AsyncCallback recieveData = new AsyncCallback(app.OnRecievedDataServer);
                m_sock.BeginReceive(m_byBuff, 0, m_byBuff.Length, SocketFlags.None, recieveData, this);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Recieve callback setup failed! {0}", ex.Message);
            }
        }

        /// <summary>
        /// Data has been recieved so we shall put it in an array and
        /// return it.
        /// </summary>
        /// <param name="ar"></param>
        /// <returns>Array of bytes containing the received data</returns>
        public byte[] GetRecievedData(IAsyncResult ar)
        {
            int nBytesRec = 0;
            try
            {
                nBytesRec = m_sock.EndReceive(ar);
            }
            catch { }
            byte[] byReturn = new byte[nBytesRec];
            Array.Copy(m_byBuff, byReturn, nBytesRec);

            return byReturn;
        }
    }
}
