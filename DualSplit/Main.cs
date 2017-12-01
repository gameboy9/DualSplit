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
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        enum KeyModifier
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            WinKey = 8
        }

        Label[,] splitTimes;
        Label[,] diffTimes;
        Label[,] splitTexts;

        Stopwatch[] clocks = { new Stopwatch(), new Stopwatch(), new Stopwatch(), new Stopwatch() };

        TimeSpan[] adjustment = { new TimeSpan(), new TimeSpan(), new TimeSpan(), new TimeSpan() };

        TimeSpan[,] splitSpan;

        int splits = 0;
        int splitLimit = 0;
        int players = 0;

        int splitYStart = 0;
        int splitY2Start = 0;
        int splitYGap = 0;

        int[] splitsDone = { 0, 0, 0, 0 };

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

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 0x0312)
            {
                /* Note that the three lines below are not needed if you only want to register one hotkey.
                 * The below lines are useful in case you want to register multiple keys, which you can use a switch with the id as argument, or if you want to know which key/modifier was pressed for some particular reason. */

                Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);                  // The key of the hotkey that was pressed.
                KeyModifier modifier = (KeyModifier)((int)m.LParam & 0xFFFF);       // The modifier of the hotkey that was pressed.
                int id = m.WParam.ToInt32();                                        // The id of the hotkey that was pressed.

                if (id < 10)
                {
                    if (id < players)
                    {
                        if (client == true)
                            sendBytes(new byte[] { (byte)(id + 3) });
                        else
                        {
                            split(id);
                            limitSplits();
                            serverSendBytes(new byte[] { (byte)(id + 3) });
                        }
                    }
                } else
                {
                    id -= 10;
                    if (id < players)
                    {
                        if (client == true)
                            sendBytes(new byte[] { (byte)(id + 7) });
                        else
                        {
                            reverseSplit(id);
                            limitSplits();
                            serverSendBytes(new byte[] { (byte)(id + 7) });
                        }
                    }
                }
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                using (TextReader reader = File.OpenText("dualSettings.txt"))
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

            loadGame();

            this.Left = 200;
            this.Top = 200;
        }

        public void startClocks()
        {
            for (int i = 0; i < players; i++)
                clocks[i].Start();
            timer1.Enabled = true;
        }

        public void adjustTime(int player, bool subtract)
        {
            if (subtract)
                adjustment[player] = adjustment[player].Subtract(new TimeSpan(0, 0, 0, 1, 0));
            else
                adjustment[player] = adjustment[player].Add(new TimeSpan(0, 0, 0, 1, 0));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < players; i++)
            {
                if (splitsDone[i] >= splits) continue;
                TimeSpan ts = clocks[i].Elapsed + adjustment[i];
                string timeElapsed = Math.Floor(ts.TotalHours) + ":" + Math.Floor((double)ts.Minutes).ToString("00") + ":" + Math.Floor((double)ts.Seconds).ToString("00") + "." + ts.Milliseconds / 100;
                if (i == 0) lblTimerA.Text = timeElapsed; else if (i == 1) lblTimerB.Text = timeElapsed; else if (i == 2) lblTimerC.Text = timeElapsed; else lblTimerD.Text = timeElapsed;
            }
        }

        public void split(int player, byte[] aryInfo = null)
        {
            if (splitsDone[player] >= splits) return;
            int split = splitsDone[player];

            TimeSpan ts = clocks[player].Elapsed + adjustment[player];
            if (aryInfo != null)
                ts = new TimeSpan(aryInfo[1], aryInfo[2], aryInfo[3]);

            splitSpan[player, split] = ts;

            splitTimes[player, split].Text = Math.Floor(ts.TotalHours) + ":" + Math.Floor((double)ts.Minutes).ToString("00") + ":" + Math.Floor((double)ts.Seconds).ToString("00");

            calcDiffs(player, splitsDone[player]);

            splitsDone[player]++;
        }

        public void calcDiffs(int player, int split)
        {
            int playerA = (player == 0 || player == 1 ? 0 : 2);
            int playerB = (player == 0 || player == 1 ? 1 : 3);

            if (splitSpan[playerA, split].TotalSeconds != 0 && splitSpan[playerB, split].TotalSeconds != 0)
            {
                TimeSpan diff = splitSpan[playerA, split].Subtract(splitSpan[playerB, split]);
                if (diff.TotalSeconds < 0)
                {
                    diff = splitSpan[playerB, split].Subtract(splitSpan[playerA, split]);
                    if (diff.TotalSeconds < 60)
                    {
                        diffTimes[playerA, split].Text = "-" + Math.Floor((double)diff.Seconds).ToString("00") + "." + diff.Milliseconds / 100;
                        diffTimes[playerA, split].ForeColor = Color.LawnGreen;
                        diffTimes[playerB, split].Text = "+" + Math.Floor((double)diff.Seconds).ToString("00") + "." + diff.Milliseconds / 100;
                        diffTimes[playerB, split].ForeColor = Color.LightCoral;
                    }
                    else
                    {
                        diffTimes[playerA, split].Text = "-" + Math.Floor(diff.TotalMinutes).ToString("00") + ":" + Math.Floor((double)diff.Seconds).ToString("00");
                        diffTimes[playerA, split].ForeColor = Color.LawnGreen;
                        diffTimes[playerB, split].Text = "+" + Math.Floor(diff.TotalMinutes).ToString("00") + ":" + Math.Floor((double)diff.Seconds).ToString("00");
                        diffTimes[playerB, split].ForeColor = Color.LightCoral;
                    }
                }
                else
                {
                    if (diff.TotalSeconds < 60)
                    {
                        diffTimes[playerA, split].Text = "+" + Math.Floor((double)diff.Seconds).ToString("00") + "." + diff.Milliseconds / 100;
                        diffTimes[playerA, split].ForeColor = Color.LightCoral;
                        diffTimes[playerB, split].Text = "-" + Math.Floor((double)diff.Seconds).ToString("00") + "." + diff.Milliseconds / 100;
                        diffTimes[playerB, split].ForeColor = Color.LawnGreen;
                    }
                    else
                    {
                        diffTimes[playerA, split].Text = "+" + Math.Floor(diff.TotalMinutes).ToString("00") + ":" + Math.Floor((double)diff.Seconds).ToString("00");
                        diffTimes[playerA, split].ForeColor = Color.LightCoral;
                        diffTimes[playerB, split].Text = "-" + Math.Floor(diff.TotalMinutes).ToString("00") + ":" + Math.Floor((double)diff.Seconds).ToString("00");
                        diffTimes[playerB, split].ForeColor = Color.LawnGreen;
                    }
                }
            }
            else
            {
                diffTimes[playerA, split].Text = "";
                diffTimes[playerB, split].Text = "";
            }
        }

        public void reverseSplit(int player, byte[] aryInfo = null)
        {
            if (splitsDone[player] == 0) return;

            splitsDone[player]--;
            int split = splitsDone[player];

            splitSpan[player, split] = new TimeSpan();

            if (player == 0 || player == 1)
            {
                diffTimes[0, split].Text = "";
                diffTimes[1, split].Text = "";
            } else
            {
                diffTimes[2, split].Text = "";
                diffTimes[3, split].Text = "";
            }

            splitTimes[player, split].Text = "";
        }

        public void adjustSplit(int player, bool plus, bool ten, byte[] aryInfo = null)
        {
            int split = splitsDone[player] - 1;
            if (split < 0) return;

            splitSpan[player, split] = splitSpan[player, split].Add(new TimeSpan(0, 0, (plus ? (ten ? 10 : 1) : (ten ? -10 : -1))));
            TimeSpan tsA = splitSpan[player, split];
            splitTimes[player, split].Text = Math.Floor(tsA.TotalHours) + ":" + Math.Floor((double)tsA.Minutes).ToString("00") + ":" + Math.Floor((double)tsA.Seconds).ToString("00");

            calcDiffs(player, split);
        }

        public void resetClocks()
        {
            timer1.Enabled = false;
            lblTimerA.Text = lblTimerB.Text = lblTimerC.Text = lblTimerD.Text = "0:00:00.0";

            for (int i = 0; i < players; i++)
            {
                for (int j = 0; j < splits; j++)
                {
                    splitSpan[i, j] = new TimeSpan();
                    splitTimes[i, j].Text = "";
                    diffTimes[i, j].Text = "";
                }
                adjustment[i] = new TimeSpan();
                clocks[i].Reset();
                splitsDone[i] = 0;
            }
        }

        private void startClocks_Click(object sender, EventArgs e)
        {
            if (client)
                sendBytes(new byte[] { 0x01 });
            else
            {
                startClocks();
                serverSendBytes(new byte[] { 0x01 });
            }
        }

        private void adjustClocks(object sender, EventArgs e)
        {
            string senderName = ((Button)sender).Name;
            if (client == true)
            {
                sendBytes(new byte[] { (byte)((senderName.Contains("A") ? 0x50 : senderName.Contains("B") ? 0x52 : senderName.Contains("C") ? 0x54 : 0x56) + (senderName.Contains("m") ? 1 : 0)) });
            } else
            {
                adjustTime(senderName.Contains("A") ? 0 : senderName.Contains("B") ? 1 : senderName.Contains("C") ? 2 : 3, senderName.Contains("m"));
                serverSendBytes(new byte[] { (byte)((senderName.Contains("A") ? 0x50 : senderName.Contains("B") ? 0x52 : senderName.Contains("C") ? 0x54 : 0x56) + (senderName.Contains("m") ? 1 : 0)) });
            }
        }

        private void btnSplit(object sender, EventArgs e)
        {
            string senderName = ((Button)sender).Name;

            if (client == true)
                sendBytes(new byte[] { (byte)((senderName.Contains("A") ? 0 : senderName.Contains("B") ? 1 : senderName.Contains("C") ? 2 : 3) + (senderName.Contains("split") ? 3 : 7)) });
            else
            {
                if (senderName.Contains("split"))
                    split(senderName.Contains("A") ? 0 : senderName.Contains("B") ? 1 : senderName.Contains("C") ? 2 : 3);
                else
                    reverseSplit(senderName.Contains("A") ? 0 : senderName.Contains("B") ? 1 : senderName.Contains("C") ? 2 : 3);

                limitSplits();

                serverSendBytes(new byte[] { (byte)((senderName.Contains("A") ? 0 : senderName.Contains("B") ? 1 : senderName.Contains("C") ? 2 : 3) + (senderName.Contains("split") ? 3 : 7)) });
            }
        }

        private void btnResetClocks_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "DualSplit", MessageBoxButtons.YesNo) == DialogResult.Yes)
                if (client == true)
                    sendBytes(new byte[] { 0x02 });
                else
                {
                    resetClocks();
                    serverSendBytes(new byte[] { 0x02 });
                }
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

        private void btnSplitAdjust(object sender, EventArgs e)
        {
            string senderName = ((Button)sender).Name;

            if (client == true)
                sendBytes(new byte[] { (byte)((senderName.Contains("A") ? 0x10 : senderName.Contains("B") ? 0x20 : senderName.Contains("C") ? 0x30 : 0x40) + (senderName.Contains("p") ? 1 : 0) + (senderName.Contains("Ten") ? 2 : 0)) });
            else
            {
                adjustSplit(senderName.Contains("A") ? 0 : senderName.Contains("B") ? 1 : senderName.Contains("C") ? 2 : 3, senderName.Contains("P"), senderName.Contains("Ten"));
                serverSendBytes(new byte[] { (byte)((senderName.Contains("A") ? 0x10 : senderName.Contains("B") ? 0x20 : senderName.Contains("C") ? 0x30 : 0x40) + (senderName.Contains("p") ? 1 : 0) + (senderName.Contains("Ten") ? 2 : 0)) });
            }
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

                if (aryRet[0] == 0x01) startClocks();
                else if (aryRet[0] == 0x02) resetClocks();
                else if (aryRet[0] == 0x03) split(0);
                else if (aryRet[0] == 0x04) split(1);
                else if (aryRet[0] == 0x05) split(2);
                else if (aryRet[0] == 0x06) split(3);
                else if (aryRet[0] == 0x07) reverseSplit(0);
                else if (aryRet[0] == 0x08) reverseSplit(1);
                else if (aryRet[0] == 0x09) reverseSplit(2);
                else if (aryRet[0] == 0x0a) reverseSplit(3);
                else if (aryRet[0] >= 0x10 && aryRet[0] < 0x50)
                    adjustSplit((aryRet[0] - 16) / 16, aryRet[0] % 4 == 1 || aryRet[0] % 4 == 3, aryRet[0] % 4 >= 2);
                else if (aryRet[0] >= 0x50)
                    adjustTime((aryRet[0] - 80) / 2, (aryRet[0] - 80) % 2 == 1);

                if (aryRet[0] >= 0x03 && aryRet[0] <= 0x0a) limitSplits();
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
                //this.BeginInvoke(new MethodInvoker(delegate
                //{
                //    listBox1.Items.Insert(0, "Connection successful");
                //}));
                client = true;
            }
            catch (Exception ex)
            {
                this.BeginInvoke(new MethodInvoker(delegate
                {
                    MessageBox.Show(this, ex.Message, "Setup Recieve Callback failed!");
                }));
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

                        if (m_byBuff[0] == 0x01) startClocks();
                        else if (m_byBuff[0] == 0x02) resetClocks();
                        else if (m_byBuff[0] == 0x03) split(0);
                        else if (m_byBuff[0] == 0x04) split(1);
                        else if (m_byBuff[0] == 0x05) split(2);
                        else if (m_byBuff[0] == 0x06) split(3);
                        else if (m_byBuff[0] == 0x07) reverseSplit(0);
                        else if (m_byBuff[0] == 0x08) reverseSplit(1);
                        else if (m_byBuff[0] == 0x09) reverseSplit(2);
                        else if (m_byBuff[0] == 0x0a) reverseSplit(3);
                        else if (m_byBuff[0] >= 0x10 && m_byBuff[0] < 0x50)
                            adjustSplit((m_byBuff[0] - 16) / 16, m_byBuff[0] % 4 == 1 || m_byBuff[0] % 4 == 3, m_byBuff[0] % 4 >= 2);
                        else if (m_byBuff[0] >= 0x50)
                            adjustTime((m_byBuff[0] - 80) / 2, (m_byBuff[0] - 80) % 2 == 1);

                        if (m_byBuff[0] >= 0x03 && m_byBuff[0] <= 0x0a) limitSplits();
                    }));

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
                m_sock.Send(bytesToSend, bytesToSend.Length, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Send Message Failed!");
            }
        }

        private void serverSendBytes(byte[] bytesToSend)
        {
            //Send the recieved data to all clients(including sender for echo)
            foreach (SocketChatClient clientSend in m_aryClients)
            {
                try
                {
                    clientSend.Sock.Send(bytesToSend);
                }
                catch
                {
                    // If the send fails the close the connection
                    Console.WriteLine("Send to client failed; closing client");
                    clientSend.Sock.Close();
                    m_aryClients.Remove(client);
                    return;
                }
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

            Unregister();
        }

        private void register()
        {
            RegisterHotKey(this.Handle, 0, (int)KeyModifier.Control, Keys.Q.GetHashCode());
            RegisterHotKey(this.Handle, 1, (int)KeyModifier.Control, Keys.O.GetHashCode());
            RegisterHotKey(this.Handle, 2, (int)KeyModifier.Control, Keys.A.GetHashCode());
            RegisterHotKey(this.Handle, 3, (int)KeyModifier.Control, Keys.L.GetHashCode());

            RegisterHotKey(this.Handle, 10, (int)KeyModifier.Alt, Keys.Q.GetHashCode());
            RegisterHotKey(this.Handle, 11, (int)KeyModifier.Alt, Keys.O.GetHashCode());
            RegisterHotKey(this.Handle, 12, (int)KeyModifier.Alt, Keys.A.GetHashCode());
            RegisterHotKey(this.Handle, 13, (int)KeyModifier.Alt, Keys.L.GetHashCode());
        }

        private void Unregister()
        {
            UnregisterHotKey(this.Handle, 0);
            UnregisterHotKey(this.Handle, 1);
            UnregisterHotKey(this.Handle, 2);
            UnregisterHotKey(this.Handle, 3);
            UnregisterHotKey(this.Handle, 10);
            UnregisterHotKey(this.Handle, 11);
            UnregisterHotKey(this.Handle, 12);
            UnregisterHotKey(this.Handle, 13);
        }

        private void cleanListBox()
        {
            while (listBox1.Items.Count > 50)
                listBox1.Items.RemoveAt(50);
        }

        private void btnChooseGame_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
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
            for (int i = 0; i < splits; i++)
            {
                for (int j = 0; j < players; j++)
                {
                    Controls.Remove(splitTimes[j, i]);
                    Controls.Remove(diffTimes[j, i]);
                }
                for (int j = 0; j < players / 2; j++)
                {
                    Controls.Remove(splitTexts[j, i]);
                }
            }

            XDocument gameXML = XDocument.Load(gameFile);
            XElement game = gameXML.Root.Element("game");
            lblGameName.Text = "Game:  " + gameXML.Element("game").Attribute("name").Value;
            players = Convert.ToInt32(gameXML.Element("game").Attribute("players").Value);
            if (players != 4) players = 2;
            // Make players 3&4 invisible if not 4 players.
            txtPlayerC.Visible = (players == 4);
            txtPlayerD.Visible = (players == 4);
            pOneC.Visible = (players == 4);
            pOneD.Visible = (players == 4);
            mOneC.Visible = (players == 4);
            mOneD.Visible = (players == 4);
            splitC.Visible = (players == 4);
            splitD.Visible = (players == 4);
            reverseC.Visible = (players == 4);
            reverseD.Visible = (players == 4);
            splitMOneC.Visible = (players == 4);
            splitMOneD.Visible = (players == 4);
            splitPOneC.Visible = (players == 4);
            splitPOneD.Visible = (players == 4);
            splitMTenC.Visible = (players == 4);
            splitMTenD.Visible = (players == 4);
            splitPTenC.Visible = (players == 4);
            splitPTenD.Visible = (players == 4);
            lblTimerC.Visible = (players == 4);
            lblTimerD.Visible = (players == 4);

            string gameFont = gameXML.Element("game").Attribute("Font").Value;

            splits = gameXML.Descendants("split").Count();
            if (gameXML.Descendants("splits").First().Attribute("limit") != null)
                splitLimit = Convert.ToInt32(gameXML.Descendants("splits").First().Attribute("limit").Value);
            else 
                splitLimit = splits;

            if (splitLimit <= 0 || splitLimit >= splits) splitLimit = splits;

            if (gameXML.Descendants("playerA").First().Attribute("visible").Value == "false")
                lblPlayerA.Visible = false;
            else
            {
                lblPlayerA.Visible = true;
            }
            if (gameXML.Descendants("playerB").First().Attribute("visible").Value == "false")
                lblPlayerB.Visible = false;
            else
            {
                lblPlayerB.Visible = true;
            }
            if (players < 3 || gameXML.Descendants("playerC").First().Attribute("visible").Value == "false")
                lblPlayerC.Visible = false;
            else
            {
                lblPlayerC.Visible = true;
            }
            if (players < 4 || gameXML.Descendants("playerD").First().Attribute("visible").Value == "false")
                lblPlayerD.Visible = false;
            else
            {
                lblPlayerD.Visible = true;
            }
            if (gameXML.Descendants("mic").First().Attribute("visible").Value == "false")
                lblCommentary.Visible = false;
            else
            {
                lblCommentary.Visible = true;
            }

            lblTimerA.Font = new Font(gameFont, Convert.ToInt32(gameXML.Descendants("clockA").First().Attribute("fontSize").Value));
            lblTimerB.Font = new Font(gameFont, Convert.ToInt32(gameXML.Descendants("clockB").First().Attribute("fontSize").Value));
            lblTimerA.Left = Convert.ToInt32(gameXML.Descendants("clockA").First().Attribute("locX").Value);
            lblTimerB.Left = Convert.ToInt32(gameXML.Descendants("clockB").First().Attribute("locX").Value);
            lblTimerA.Top = Convert.ToInt32(gameXML.Descendants("clockA").First().Attribute("locY").Value);
            lblTimerB.Top = Convert.ToInt32(gameXML.Descendants("clockB").First().Attribute("locY").Value);
            if (players == 4)
            {
                lblTimerC.Font = new Font(gameFont, Convert.ToInt32(gameXML.Descendants("clockC").First().Attribute("fontSize").Value));
                lblTimerD.Font = new Font(gameFont, Convert.ToInt32(gameXML.Descendants("clockD").First().Attribute("fontSize").Value));
                lblTimerC.Left = Convert.ToInt32(gameXML.Descendants("clockC").First().Attribute("locX").Value);
                lblTimerD.Left = Convert.ToInt32(gameXML.Descendants("clockD").First().Attribute("locX").Value);
                lblTimerC.Top = Convert.ToInt32(gameXML.Descendants("clockC").First().Attribute("locY").Value);
                lblTimerD.Top = Convert.ToInt32(gameXML.Descendants("clockD").First().Attribute("locY").Value);
            }

            Font timeFont = new Font(gameFont, Convert.ToInt32(gameXML.Descendants("times").First().Attribute("fontSize").Value));
            int timeALeft = Convert.ToInt32(gameXML.Descendants("times").First().Attribute("aLocX").Value);
            int timeBLeft = Convert.ToInt32(gameXML.Descendants("times").First().Attribute("bLocX").Value);

            Font diffFont = new Font(gameFont, Convert.ToInt32(gameXML.Descendants("diffs").First().Attribute("fontSize").Value));
            int diffALeft = Convert.ToInt32(gameXML.Descendants("diffs").First().Attribute("aLocX").Value);
            int diffBLeft = Convert.ToInt32(gameXML.Descendants("diffs").First().Attribute("bLocX").Value);

            Font titleFont = new Font(gameFont, Convert.ToInt32(gameXML.Descendants("title").First().Attribute("fontSize").Value));
            int titleLeft = Convert.ToInt32(gameXML.Descendants("title").First().Attribute("aLocX").Value);

            Font splitFont = new Font(gameFont, Convert.ToInt32(gameXML.Descendants("splits").First().Attribute("fontSize").Value));
            splitYStart = Convert.ToInt32(gameXML.Descendants("splits").First().Attribute("yStart").Value);
            splitY2Start = (players < 4 ? 0 : Convert.ToInt32(gameXML.Descendants("splits").First().Attribute("yStart2").Value));
            splitYGap = Convert.ToInt32(gameXML.Descendants("splits").First().Attribute("yGap").Value);

            splitTexts = new Label[players / 2, splits];
            splitTimes = new Label[players, splits];
            diffTimes = new Label[players, splits];
            splitSpan = new TimeSpan[players, splits];

            for (int i = 0; i < players; i++)
                for (int j = 0; j < splits; j++)
                {
                    splitSpan[i, j] = new TimeSpan();

                    splitTimes[i, j] = new Label();
                    diffTimes[i, j] = new Label();

                    splitTimes[i, j].Left = (i == 0 || i == 2 ? timeALeft : timeBLeft);
                    splitTimes[i, j].Top = (i == 0 || i == 1 ? splitYStart : splitY2Start) + (j * splitYGap);
                    splitTimes[i, j].ForeColor = Color.White;
                    splitTimes[i, j].Visible = true;
                    splitTimes[i, j].Text = "";
                    splitTimes[i, j].AutoSize = true;
                    splitTimes[i, j].Font = splitFont;

                    Controls.Add(splitTimes[i, j]);

                    diffTimes[i, j].Left = (i == 0 || i == 2 ? diffALeft : diffBLeft);
                    diffTimes[i, j].Top = (i == 0 || i == 1 ? splitYStart : splitY2Start) + (j * splitYGap);
                    diffTimes[i, j].ForeColor = Color.White;
                    diffTimes[i, j].Visible = true;
                    diffTimes[i, j].Text = "";
                    diffTimes[i, j].AutoSize = true;
                    diffTimes[i, j].Font = splitFont;
                    Controls.Add(diffTimes[i, j]);
                }

            for (int i = 0; i < players / 2; i++)
                for (int j = 0; j < splits; j++)
                {
                    splitTexts[i, j] = new Label();
                    splitTexts[i, j].Text = gameXML.Descendants("split").Skip(j).First().Attribute("name").Value;
                    splitTexts[i, j].Left = titleLeft;
                    splitTexts[i, j].Top = (i == 0 ? splitYStart : splitY2Start) + (j * splitYGap);
                    splitTexts[i, j].AutoSize = true;
                    splitTexts[i, j].Font = splitFont;
                    splitTexts[i, j].ForeColor = Color.White;
                    Controls.Add(splitTexts[i, j]);
                }

            limitSplits();
        }

        private void limitSplits()
        {
            // Figure out max of splits done between the two battles.
            int maxSplits = Math.Max(splitsDone[0], splitsDone[1]); // Should be minus 1 because splits is zero-based, but we want to see at least one split into the future.
            if (maxSplits >= splits) maxSplits = splits - 1; // Make sure we don't break the bounds of the array
            int firstSplit = Math.Max(maxSplits - splitLimit + 1, 0); // Plus 1 here because we want to see one split into the future.
            int lastSplit = Math.Max(splitLimit - 1, maxSplits);
            for (int i = 0; i < splits; i++)
            {
                splitTimes[0, i].Visible = splitTimes[1, i].Visible = diffTimes[0, i].Visible = diffTimes[1, i].Visible = splitTexts[0, i].Visible = (i >= firstSplit && i <= lastSplit);
                splitTimes[0, i].Top = splitTimes[1, i].Top = diffTimes[0, i].Top = diffTimes[1, i].Top = splitTexts[0, i].Top = splitYStart + ((i - firstSplit) * splitYGap);
            }
            if (players == 4)
            {
                maxSplits = Math.Max(splitsDone[2], splitsDone[3]); // Should be minus 1 because splits is zero-based, but we want to see at least one split into the future.
                if (maxSplits >= splits) maxSplits = splits - 1; // Make sure we don't break the bounds of the array
                firstSplit = Math.Max(maxSplits - splitLimit + 1, 0); // Plus 1 here because we want to see one split into the future.
                lastSplit = Math.Max(splitLimit - 1, maxSplits);
                for (int i = 0; i < splits; i++)
                {
                    splitTimes[2, i].Visible = splitTimes[3, i].Visible = diffTimes[2, i].Visible = diffTimes[3, i].Visible = splitTexts[1, i].Visible = (i >= firstSplit && i <= lastSplit);
                    splitTimes[2, i].Top = splitTimes[3, i].Top = diffTimes[2, i].Top = diffTimes[3, i].Top = splitTexts[1, i].Top = splitY2Start + ((i - firstSplit) * splitYGap);
                }
            }
        }

        private void chkGlobalHotkeys_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGlobalHotkeys.Checked)
                register();
            else
                Unregister();
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
