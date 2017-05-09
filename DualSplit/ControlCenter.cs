using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

// Declare the delegate prototype to send data back to the form
//delegate void AddMessage(string sNewMessage);

namespace DualSplit
{
    public partial class ControlCenter : Form
    {
        Main main;
        private Socket m_sock;                      // Server connection
        private byte[] m_byBuff = new byte[256];    // Recieved data buffer
        private ArrayList m_aryClients = new ArrayList();	// List of Client Connections
        //private event AddMessage m_AddMessage;		// Add Message Event handler for Form

        public ControlCenter()
        {
            InitializeComponent();
        }

        private void ControlCenter_Load(object sender, EventArgs e)
        {
            // Add Message Event handler for Form decoupling from input thread
            //m_AddMessage = new AddMessage(OnAddMessage);
        }

        public void setupForm(Main firstForm)
        {
            main = firstForm;
        }

        private void startClocks_Click(object sender, EventArgs e)
        {
            main.startClocks();
        }

        private void adjustClocks(object sender, EventArgs e)
        {
            Button actualSender = (Button)sender;
            main.adjustTime(actualSender.Name == "pOneA" || actualSender.Name == "mOneA" || actualSender.Name == "pTenthA" || actualSender.Name == "mTenthA",
                actualSender.Name == "pTenthA" || actualSender.Name == "mTenthA" || actualSender.Name == "pTenthB" || actualSender.Name == "mTenthB",
                actualSender.Name == "mOneB" || actualSender.Name == "mOneA" || actualSender.Name == "mTenthB" || actualSender.Name == "mTenthA");
        }

        private void splitA_Click(object sender, EventArgs e)
        {
            main.split(true);
        }

        private void splitB_Click(object sender, EventArgs e)
        {
            main.split(false);
        }

        private void reverseA_Click(object sender, EventArgs e)
        {
            main.reverseSplit(true);
        }

        private void reverseB_Click(object sender, EventArgs e)
        {
            main.reverseSplit(false);
        }

        private void splitPOneA_Click(object sender, EventArgs e)
        {
            main.adjustSplit(true, true, false);
        }

        private void splitMOneA_Click(object sender, EventArgs e)
        {
            main.adjustSplit(true, false, false);
        }

        private void splitPOneB_Click(object sender, EventArgs e)
        {
            main.adjustSplit(false, true, false);
        }

        private void splitMOneB_Click(object sender, EventArgs e)
        {
            main.adjustSplit(false, false, false);
        }

        private void btnResetClocks_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "DualSplit", MessageBoxButtons.YesNo) == DialogResult.Yes)
                main.resetClocks();
        }

        private void txtPlayerB_Leave(object sender, EventArgs e)
        {
            main.lblPlayerB.Text = txtPlayerB.Text;
        }

        private void txtPlayerA_TextChanged(object sender, EventArgs e)
        {
            main.lblPlayerA.Text = txtPlayerA.Text;
        }

        private void txtCommentary_Leave(object sender, EventArgs e)
        {
            main.lblCommentary.Text = "Commentary:  " + txtCommentary.Text;
        }

        private void splitPTenA_Click(object sender, EventArgs e)
        {
            main.adjustSplit(true, true, true);
        }

        private void splitMTenA_Click(object sender, EventArgs e)
        {
            main.adjustSplit(true, false, true);
        }

        private void splitPTenB_Click(object sender, EventArgs e)
        {
            main.adjustSplit(false, true, true);
        }

        private void splitMTenB_Click(object sender, EventArgs e)
        {
            main.adjustSplit(false, false, true);
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
                Console.WriteLine("Error trying to get local address {0} ", ex.Message);
            }

            // Verify we got an IP address. Tell the user if we did
            if (aryLocalAddr == null || aryLocalAddr.Length < 1)
            {
                Console.WriteLine("Unable to get local address");
                return;
            }
            //Console.WriteLine("Listening on : [{0}] {1}:{2}", strHostName, aryLocalAddr[0], nPortListen);

            // Create the listener socket in this machines IP address
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(new IPEndPoint(aryLocalAddr[0], Convert.ToInt32(txtPort.Text)));
            //listener.Bind( new IPEndPoint( IPAddress.Loopback, 399 ) );	// For use with localhost 127.0.0.1
            listener.Listen(10);

            // Setup a callback to be notified of connection requests
            listener.BeginAccept(new AsyncCallback(OnConnectRequest), listener);
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
        //public void NewConnection( TcpListener listener )
        public void NewConnection(Socket sockClient)
        {
            // Program blocks on Accept() until a client connects.
            //SocketChatClient client = new SocketChatClient( listener.AcceptSocket() );
            SocketChatClient client = new SocketChatClient(sockClient);
            m_aryClients.Add(client);
            Console.WriteLine("Client {0}, joined", client.Sock.RemoteEndPoint);

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
                Console.WriteLine("Client {0}, disconnected", client.Sock.RemoteEndPoint);
                client.Sock.Close();
                m_aryClients.Remove(client);
                return;
            }

            // Do something with the received data


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

                // Connect to the server blocking method and setup callback for recieved data
                // m_sock.Connect( epServer );
                // SetupRecieveCallback( m_sock );

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
                    MessageBox.Show(this, "Unable to connect to remote machine", "Connect Failed!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Unusual error during Connect!");
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

                    //// WARNING : The following line is NOT thread safe. Invoke is
                    //// m_lbRecievedData.Items.Add( sRecieved );
                    //Invoke(m_AddMessage, new string[] { sRecieved });

                    // If the connection is still usable restablish the callback
                    SetupRecieveCallback(sock);
                }
                else
                {
                    // If no data was recieved then the connection is probably dead
                    Console.WriteLine("Client {0}, disconnected", sock.RemoteEndPoint);
                    sock.Shutdown(SocketShutdown.Both);
                    sock.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Unusual error druing Recieve!");
            }
        }

        /// <summary>
        /// Send the Message in the Message area. Only do this if we are connected
        /// </summary>
        private void sendBytes()
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
                Byte[] byteDateLine = Encoding.ASCII.GetBytes("test message".ToCharArray());
                m_sock.Send(byteDateLine, byteDateLine.Length, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Send Message Failed!");
            }
        }


        //public void OnAddMessage(string sMessage)
        //{
        //    // Do stuff with received data.
        //}

        private void cmdTest_Click(object sender, EventArgs e)
        {
            sendBytes();
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
        public void SetupRecieveCallback(ControlCenter app)
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

            /*
			// Check for any remaining data and display it
			// This will improve performance for large packets 
			// but adds nothing to readability and is not essential
			int nToBeRead = m_sock.Available;
			if( nToBeRead > 0 )
			{
				byte [] byData = new byte[nToBeRead];
				m_sock.Receive( byData );
				// Append byData to byReturn here
			}
			*/
            return byReturn;
        }
    }
}
