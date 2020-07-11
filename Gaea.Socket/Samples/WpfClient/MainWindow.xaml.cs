using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GSocket;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Threading;

namespace WpfClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //client = TCPManager.GetClient();
        }
        IClient client;
        IConnection conn;

        private void btn_click(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(() => {
                for (int i = 0; i <500; i++) {
                    client = TcpProxy.GetClient();
                    client.Connect(6000, "127.0.0.1");
                    //Thread.Sleep(10);
                }
            });
            //Task.Factory.StartNew(() => {
            //    for (int i = 0; i < 10000; i++) {
            //        client = TCPManager.GetClient();
            //        client.Connect(6001, "127.0.0.1");

            //        //Thread.Sleep(10);
            //    }
            //});


            //client.SetConnectedHandler((ce) => {
            //    if (ce.StatusCode != 0) {
            //        MessageBox.Show(ce.Msg);
            //        return;
            //    }
            //    MessageBox.Show(ce.Conn.ToString());
            //});

            //client.Connect(6000, "127.0.0.1");
        }
        


        private void btn_send(object sender, RoutedEventArgs e)
        {
            string msg = "hello";
            Task.Factory.StartNew(() =>
            {
                int i = 1000;
                while (i-->0)
                {
                    msg += "0";
                    conn.Send(Encoding.UTF8.GetBytes(msg));
                    Thread.Sleep(200);
                }
            });
        }
    }
}
