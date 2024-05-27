using System.Windows;
using Zettelkasten.Applications.Services;
using Zettelkasten.DesktopApp.ViewModels;

namespace Zettelkasten.DesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new ApplicationViewModel();
        }

        private void TagCollection_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ApplicationViewModel? vm = GetVM();
            vm.ClearDrawNotes();    

            var selectedItems = TagCollection.SelectedItems.Cast<string>();
            var selectedTagName = selectedItems.Select(x => $"#{x};");
            
            var filteredFigures = vm._figures.Where(x => ((string)x.ToolTip).ContainsAny(selectedTagName)).ToList();
            vm.DrawNotes(filteredFigures);
        }

        private ApplicationViewModel? GetVM()
        {
            return (this.DataContext as ApplicationViewModel);
        }

        private void SelectAllTags_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in TagCollection.Items)
                TagCollection.SelectedItems.Add(item);
        }

        private void DeSelectAllTags_Click(object sender, RoutedEventArgs e)
        {
            TagCollection.SelectedItems.Clear();
        }
    }
}