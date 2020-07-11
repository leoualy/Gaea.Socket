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
using System.Threading;
using GSocket;


namespace WpfServer {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }
        int countAsy = 0, countSyn = 0;
        private void Button_Click(object sender, RoutedEventArgs e) {
            IServer srv = TcpProxy.GetServerAsync();
            srv.SetConnectedHandler((ce) => {
                Interlocked.Increment(ref countAsy);
                this.Dispatcher.Invoke(new Action(() => { this.txtAsyn.Text = countAsy.ToString(); }));
            });
            srv.Start(6001, out _);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            IServer srv = TcpProxy.GetServer();
            srv.SetConnectedHandler((ce) => {
                Interlocked.Increment(ref countSyn);
                this.Dispatcher.Invoke(new Action(() => { this.txtSyn.Text = countSyn.ToString(); }));
            });
            srv.Start(6000, out _);
        }
    }
}
