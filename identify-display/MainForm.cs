namespace identify_display
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            ScreenItem primary = null;
            foreach (var screenItem in Screen.AllScreens.Select(_ => new ScreenItem(_)))
            {
                if(screenItem.Screen.Primary)
                {
                    primary = screenItem;
                }
                richTextBox.SelectionColor = Color.Navy;
                richTextBox.AppendText($"{screenItem.DisplayName}{Environment.NewLine}");
                richTextBox.SelectionColor = ForeColor;
                richTextBox.AppendText($"{screenItem}{Environment.NewLine}");
            }
        }
    }
    class ScreenItem
    {
        public ScreenItem(Screen screen) => Screen = screen;
        public Screen Screen { get; }
        string _displayName = null;
        public string DisplayName
        {
            get => _displayName?? Screen.DeviceName.Substring(Screen.DeviceName.LastIndexOf('\\') + 1);
            set => _displayName = value;
        }
        public override string ToString()
        {
            List<string> builder= new List<string>();
            builder.Add($"{nameof(Screen.Primary)} : {Screen.Primary}");
            builder.Add($"{nameof(Screen.Bounds)} : {Screen.Bounds}");
            builder.Add($"{nameof(Screen.WorkingArea)} : {Screen.WorkingArea}");
            return 
                string.Join(Environment.NewLine, builder.ToArray()
                .Select(_ => $"\t{_}"))
                + Environment.NewLine;
        }
    }
}