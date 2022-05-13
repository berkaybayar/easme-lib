using System.Windows.Forms;

namespace EasMe
{
    public class EasBox
    {
        public bool Confirm(string message, string header = "Confirm") //Shows a confirmation box and returns the result taken from user
        {
            if (DialogResult.Yes == MessageBox.Show(message, header, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return true;
            return false;

        }
        public void Show(string message) //Shows simple message box 
        {
            MessageBox.Show(message);
        }
        public void Warn(string message) //Shows a warning message box with only OK button
        {
            MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public void Info(string message) //Shows an information message box with only OK buttong
        {
            MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void Error(string message) //Shows an Error message box with only OK buttong
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void Stop(string message) //Shows an Stop message box with only OK buttong
        {
            MessageBox.Show(message, "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

    }

}
