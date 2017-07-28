using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Admin.Model
{
    class MainModel
    {
        public static void Admin_Received(byte[] data)
        {
            Task.Factory.StartNew(() =>
            {
                string encStr = Encoding.UTF8.GetString(data);
                CMD.ReceiveCommand(Cryptography.Decrypt(encStr));
            }, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public static void Admin_ServerNotResponding()
        {
            MessageBox.Show("Server not responding!", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }
    }
}
