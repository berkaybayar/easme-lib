using System.Windows.Forms;

namespace EasMe.MessageBox
{
    public static class Box
    {
        /// <summary>
        /// Shows a confirmation box and returns the result taken from user
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool Confirm(string message) 
        {
            return DialogResult.Yes == System.Windows.Forms.MessageBox.Show(message, "Confirmation",
                                                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        /// <summary>
        /// Shows simple message box 
        /// </summary>
        /// <param name="message"></param>
        public static void Show(string message) 
        {
            System.Windows.Forms.MessageBox.Show(message);
        }

        /// <summary>
        /// Shows a warning message box with only OK button
        /// </summary>
        /// <param name="message"></param>
        public static void Warning(string message) 
        {
            System.Windows.Forms.MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Shows an information message box with only OK button
        /// </summary>
        /// <param name="message"></param>
        public static void Information(string message) 
        {
            System.Windows.Forms.MessageBox.Show(message, "Information", MessageBoxButtons.OK,
                                                 MessageBoxIcon.Information);
        }

        /// <summary>
        /// Shows an Error message box with only OK button
        /// </summary>
        /// <param name="message"></param>
        public static void Error(string message) 
        {
            System.Windows.Forms.MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Shows an Stop message box with only OK button
        /// </summary>
        /// <param name="message"></param>
        public static void Stop(string message) 
        {
            System.Windows.Forms.MessageBox.Show(message, "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }
    }
}