using System.Windows.Forms;

namespace EasMe
{
    public class EasBox
    {
        public bool Confirm(string Message) //Shows a confirmation box and returns the result taken from user
        {
            if (DialogResult.Yes == MessageBox.Show(Message, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return true;
            return false;

        }
        public void Show(string Message) //Shows simple message box 
        {
            MessageBox.Show(Message);
        }
        public void Warn(string Message) //Shows a warning message box with only OK button
        {
            MessageBox.Show(Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public void Info(string Message) //Shows an information message box with only OK buttong
        {
            MessageBox.Show(Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void Error(string Message) //Shows an Error message box with only OK buttong
        {
            MessageBox.Show(Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void Stop(string Message) //Shows an Stop message box with only OK buttong
        {
            MessageBox.Show(Message, "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

    }

}
