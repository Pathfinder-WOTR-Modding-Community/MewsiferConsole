namespace MewsiferConsole
{
  public partial class ChooseMessageSourceForm : Form
  {
    public ChooseMessageSourceForm()
    {
      InitializeComponent();
    }

    private void fromFile_Click(object sender, EventArgs e)
    {
      var dialog = new OpenFileDialog
      {
        CheckFileExists = true
      };

      var result = dialog.ShowDialog(this);
      if (result == DialogResult.OK && File.Exists(dialog.FileName))
      {
        App.MessageSource.Source = new MessagesFromFile(dialog.FileName);
        Close();
      }
    }

    private void fromGame_Click(object sender, EventArgs e)
    {
      App.MessageSource.Source = new IPC.Server();
      Close();
    }

    private void fromDpaste_Click(object sender, EventArgs e)
    {
      string input = "";
      if (ShowInputDialog(ref input) == DialogResult.OK)
        App.MessageSource.Source = new MessagesFromDpaste(input);
      Close();

    }

    private static DialogResult ShowInputDialog(ref string input)
    {
      System.Drawing.Size size = new System.Drawing.Size(600, 100);
      Form inputBox = new Form();

      inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      inputBox.ClientSize = size;
      inputBox.Text = "Input dpaste.org url";

      System.Windows.Forms.TextBox textBox = new TextBox();
      textBox.Size = new System.Drawing.Size(size.Width - 10, 40);
      textBox.Location = new System.Drawing.Point(5, 5);
      textBox.Text = input;
      inputBox.Controls.Add(textBox);

      Button okButton = new Button();
      okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
      okButton.Name = "okButton";
      okButton.Size = new System.Drawing.Size(75, 40);
      okButton.Text = "&OK";
      okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 39);
      inputBox.Controls.Add(okButton);

      Button cancelButton = new Button();
      cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      cancelButton.Name = "cancelButton";
      cancelButton.Size = new System.Drawing.Size(75, 40);
      cancelButton.Text = "&Cancel";
      cancelButton.Location = new System.Drawing.Point(size.Width - 80, 39);
      inputBox.Controls.Add(cancelButton);

      inputBox.AcceptButton = okButton;
      inputBox.CancelButton = cancelButton;

      DialogResult result = inputBox.ShowDialog();
      input = textBox.Text;
      return result;
    }
  }
}
