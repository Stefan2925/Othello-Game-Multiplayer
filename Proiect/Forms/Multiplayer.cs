using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Proiect.Forms
{
    public partial class Multiplayer : GameWindow
    {
        protected bool asteaptaClient { get; private set; } = true;
        private volatile bool inchidereForm = false;
        private TcpListener server;
        private TcpClient client;
        private NetworkStream stream;
        private bool isHost;
        private bool esteTuraMea;
        private culoareJucator culoareaMea;
        private Thread conectareThread;
        private Thread ascultareThread;
        private volatile bool running = true;
        private Panel panouDeAsteptare;
        private Label etichetaDeAsteptare;
        private Button btnAnulare;




        public Multiplayer(bool isHost, string serverIp) : base()
        {
            if (DesignMode) return;

            this.isHost = isHost;
            culoareaMea = isHost ? culoareJucator.Negru : culoareJucator.Alb;
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
                ArataEcranAsteptare("Conectare la server...");
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
            if (DesignMode) return;

            panouDeAsteptare = new Panel
            {
                BackColor = Color.FromArgb(128, Color.Black),
                Dock = DockStyle.Fill
            };
            panouDeAsteptare.BringToFront();

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
                Location = new Point((this.ClientSize.Width - 150) / 2, 250)
            };
            btnAnulare.FlatAppearance.MouseOverBackColor = Color.FromArgb(200, 255, 70, 70);
            btnAnulare.Click += (s, e) => { this.Close(); };

            panouDeAsteptare.Controls.Add(etichetaDeAsteptare);
            panouDeAsteptare.Controls.Add(btnAnulare);
            this.Controls.Add(panouDeAsteptare);
            this.Controls.SetChildIndex(panouDeAsteptare, 0);


            btnAnulare.FlatAppearance.MouseOverBackColor = Color.FromArgb(200, 255, 70, 70);
            btnAnulare.Click += (s, e) => { this.Close();
    };

            panouDeAsteptare.Controls.Add(etichetaDeAsteptare);
            panouDeAsteptare.Controls.Add(btnAnulare);
    this.Controls.Add(panouDeAsteptare);
    this.Controls.SetChildIndex(panouDeAsteptare, 0);
    }

        private void AscundeEcranAsteptare()
        {
            asteaptaClient = false;

            if (panouDeAsteptare != null)
            {
                if (panouDeAsteptare.Parent != null)
                    panouDeAsteptare.Parent.Controls.Remove(panouDeAsteptare);

                panouDeAsteptare.Dispose();
                panouDeAsteptare = null;
                etichetaDeAsteptare?.Dispose();
                btnAnulare?.Dispose();
                etichetaDeAsteptare = null;
            }
        }

        #endregion Aspect Grafic


        #region Transfer Date/Mutari
        public void TrimiteMutare(int row, int col)
        {
            if (!esteTuraMea || stream == null)
                return;

            string mutare = $"{row},{col}<|EOM|>";
            byte[] bytes = Encoding.UTF8.GetBytes(mutare);
            stream.Write(bytes, 0, bytes.Length);

            esteTuraMea = false;
            game.AflaMiscariValide();
        }

        private void PrimesteMutariContinuu()
        {
            byte[] buffer = new byte[1024];

            while (running && !inchidereForm)  // Adaugă verificarea
            {
                try
                {
                    if (stream == null || !stream.CanRead || inchidereForm)
                        break;

                    if (stream.DataAvailable)
                    {
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);
                        if (bytesRead == 0)
                            break;

                        string mesaj = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                        if (mesaj.Contains("<|EOM|>") && !inchidereForm)
                        {
                            string[] parts = mesaj.Split(',');
                            if (parts.Length >= 2)
                            {
                                int row = int.Parse(parts[0]);
                                int col = int.Parse(parts[1].Split('<')[0]);

                                if (!inchidereForm)
                                {
                                    this.Invoke((MethodInvoker)(() =>
                                    {
                                        if (!inchidereForm)
                                        {
                                            game.PunePiesa(row, col);
                                            esteTuraMea = true;
                                            game.AflaMiscariValide();
                                        }
                                    }));
                                }
                            }
                        }
                    }
                    else
                    {
                        Thread.Sleep(16);
                    }
                }
                catch
                {
                    break;
                }
            }
        }


        #endregion Transfer Date/Mutari


        #region Override la InchidereForm
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            running = false;
            inchidereForm = true;
            base.OnFormClosing(e);

    
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
        }


        #endregion Override la inchidereForm

       
    }
}
