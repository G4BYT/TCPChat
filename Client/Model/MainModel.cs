using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace Client.Model
{
    class MainModel
    {
        public static void Client_Received(byte[] data)
        {
            Task.Factory.StartNew(() =>
            {
                string encStr = Encoding.UTF8.GetString(data);
                CMD.ReceiveCommand(Cryptography.Decrypt(encStr));
            }, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public static void Client_ServerNotResponding()
        {
            MessageBox.Show("Server not responding!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }

    }
}
