using System.ComponentModel;
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
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string PATH = $"{Environment.CurrentDirectory}\\todaGataList.json";
        private BindingList<TodoModel> _todoDataList;
        private FileIOServise _fileIOService;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _fileIOService = new FileIOServise(PATH);

            try
            {
                _todoDataList = _fileIOService.LoadData();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }

            dgToDoList.ItemsSource = _todoDataList;
            _todoDataList.ListChanged += _todoDataList_ListChanged;

        }

        private void _todoDataList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.ItemChanged)

            try
            {
                _fileIOService.SaveData(sender);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }
        }
    }
}