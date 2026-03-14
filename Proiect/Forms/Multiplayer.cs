using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Proiect.Forms
{
    public partial class Multiplayer : GameWindow
    {
        protected bool asteaptaClient { get; private set; } = true;
        private readonly bool isHost;
        private bool esteTuraMea;
        private volatile bool inchidereForm = false;
        private volatile bool running = true;

  
        private TcpListener server;
        private TcpClient client;
        private NetworkStream stream;
        private Thread conectareThread;
        private Thread ascultareThread;
        private Panel panouDeAsteptare;
        private Label etichetaDeAsteptare;
        private Button btnAnulare;


        private readonly CuloareJucator culoareaMea;


        public Multiplayer(bool isHost, string serverIp) : base()
        {
            if (DesignMode) return;

            this.isHost = isHost;
            culoareaMea = isHost ? CuloareJucator.Negru : CuloareJucator.Alb;
            esteTuraMea = isHost;
    

        FormBorderStyle = FormBorderStyle.Sizable;
            WindowState = FormWindowState.Maximized;
            MaximizeBox = true;
            MinimizeBox = true;

            if (isHost)
            {
                server = new TcpListener(IPAddress.Any, 8080);
                server.Start();
                ArataEcranAsteptare("Asteptare adversar...");
                ascultareThread = new Thread(AsteaptaClient);
                ascultareThread.Start();
            }
            else
            {
                conectareThread = new Thread(() => ConectareLaServer(serverIp));
                conectareThread.Start();
            }
        }

        public bool EsteTuraMea
        {
            get { return esteTuraMea; }
        }

        #region Client+Server
        private void ConectareLaServer(string serverIp)
        {
            try
            {
                client = new TcpClient();
                client.Connect(serverIp, 8080);
                stream = client.GetStream();

                if (!inchidereForm)  
                {
                    this.Invoke((MethodInvoker)(() =>
                    {
                        if (!inchidereForm) 
                        {
                            AscundeEcranAsteptare();
                        }
                    }));
                }

                if (!inchidereForm)
                {
                    PrimesteMutariContinuu();
                }
            }
            catch (Exception ex)
            {
                if (!inchidereForm) 
                {
                    this.Invoke((MethodInvoker)(() =>
                    {
                        if (!inchidereForm)  
                        {
                            MessageBox.Show($"Eroare conectare: {ex.Message}");
                            AscundeEcranAsteptare();
                        }
                    }));
                }
            }
        }

        private void AsteaptaClient()
        {
            try
            {
                client = server.AcceptTcpClient();
                stream = client.GetStream();

                if (!inchidereForm)
                {
                    this.Invoke((MethodInvoker)(() =>
                    {
                        if (!inchidereForm)
                        {
                            MessageBox.Show("Adversarul s-a conectat!");
                            AscundeEcranAsteptare();
                        }
                    }));
                }

                if (!inchidereForm)
                {
                    PrimesteMutariContinuu();
                }
            }
            catch (Exception ex)
            {
                if (!inchidereForm) 
                {
                    this.Invoke((MethodInvoker)(() =>
                    {
                        if (!inchidereForm) 
                        {
                            MessageBox.Show($"Eroare: {ex.Message}");
                        }
                    }));
                }
            }
        }


        #endregion Client+Server


        #region Aspect Grafic
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
               
                return cp;
            }
        }
        private void ArataEcranAsteptare(string text)
        {
            if (DesignMode || panouDeAsteptare != null) return;

            panouDeAsteptare = new Panel
            {
                BackColor = Color.FromArgb(128, Color.Black),
                Dock = DockStyle.Fill
            };

            etichetaDeAsteptare = new Label
            {
                Text = text,
                ForeColor = Color.White,
                Font = new Font("Arial", 24, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(100, 100),
                TextAlign = ContentAlignment.MiddleCenter
            };

            btnAnulare = new Button
            {
                Text = "Anuleaza",
                ForeColor = Color.White,
                BackColor = Color.FromArgb(200, 220, 50, 50),
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                Font = new Font("Arial", 16, FontStyle.Bold),
                Size = new Size(150, 50),
                Location = new Point((ClientSize.Width - 150) / 2, 250)
            };
            btnAnulare.FlatAppearance.MouseOverBackColor = Color.FromArgb(200, 255, 70, 70);
            btnAnulare.Click += (s, e) => { this.Close(); };

            panouDeAsteptare.Controls.Add(etichetaDeAsteptare);
            panouDeAsteptare.Controls.Add(btnAnulare);
            Controls.Add(panouDeAsteptare);
            Controls.SetChildIndex(panouDeAsteptare, 0);
        }
        private void AscundeEcranAsteptare()
        {
            asteaptaClient = false;

            panouDeAsteptare?.Dispose();        
            etichetaDeAsteptare?.Dispose();       
            btnAnulare?.Dispose();               

            panouDeAsteptare = null;            
            etichetaDeAsteptare = null;           
            btnAnulare = null;                     
        }

        #endregion Aspect Grafic


        #region Transfer Date/Mutari
        public void TrimiteMutare(int rand, int coloana)
        {
            if (!esteTuraMea || stream == null) return;

           
            string mutare = $"M{rand:00},{coloana:00}|END|";
            byte[] bytes = Encoding.UTF8.GetBytes(mutare);

           
            byte[] lungime = BitConverter.GetBytes(bytes.Length);
            stream.Write(lungime, 0, 4);
            stream.Write(bytes, 0, bytes.Length);

            esteTuraMea = false;
            this.Invoke((MethodInvoker)(() => game.AflaMiscariValide()));
        }

        private void PrimesteMutariContinuu()
        {
            byte[] lungimeBuffer = new byte[4];  

            while (running && !inchidereForm)
            {
                try
                {
                    if (stream == null || !stream.CanRead) break;

                    
                    int bytesLungime = stream.Read(lungimeBuffer, 0, 4);
                    if (bytesLungime < 4) break;

                    int lungimeMesaj = BitConverter.ToInt32(lungimeBuffer, 0);
                    byte[] bufferMesaj = new byte[lungimeMesaj]; 

                    
                    int totalCitit = 0;
                    while (totalCitit < lungimeMesaj)
                    {
                        int citit = stream.Read(bufferMesaj, totalCitit, lungimeMesaj - totalCitit);
                        if (citit == 0) break;
                        totalCitit += citit;
                    }

                    if (totalCitit == lungimeMesaj)
                    {
                        string mesaj = Encoding.UTF8.GetString(bufferMesaj);
                        ProceseazaMutare(mesaj);
                    }
                }
                catch { break; }

                Thread.Sleep(1);
            }
        }

        private void ProceseazaMutare(string mesaj)
        {
            if (mesaj.StartsWith("M") && mesaj.Contains("|END|"))
            {
                string date = mesaj.Substring(1).Split('|')[0];  // "3,4"
                string[] parts = date.Split(',');

                if (parts.Length == 2 && int.TryParse(parts[0], out int rand) &&
                    int.TryParse(parts[1], out int coloana))
                {
                    this.Invoke((MethodInvoker)(() =>
                    {
                        game.PunePiesa(rand, coloana);
                        esteTuraMea = true;
                        game.AflaMiscariValide();
                    }));
                }
            }
        }


        #endregion Transfer Date/Mutari


        #region Override la InchidereForm

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            running = false;
            inchidereForm = true;

            if (panouDeAsteptare?.Parent != null)
                panouDeAsteptare.Parent.Controls.Remove(panouDeAsteptare);
            panouDeAsteptare?.Dispose();
            etichetaDeAsteptare?.Dispose();
            btnAnulare?.Dispose();

            conectareThread?.Join(3000);
            ascultareThread?.Join(3000);

            stream?.Close();
            stream?.Dispose();
            client?.Close();
            client?.Dispose();
            server?.Stop();

            conectareThread = null;
            ascultareThread = null;
            stream = null;
            client = null;
            server = null;

            base.OnFormClosing(e);
        }

        #endregion Override la inchidereForm


    }
}
