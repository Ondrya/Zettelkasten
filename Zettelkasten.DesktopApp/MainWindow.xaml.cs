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

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void PlanningButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ZettelkastenButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SelectionMapButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void NotepadButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TagCollection_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedItems = TagCollection.SelectedItems.Cast<string>();
            var selectedTagName = selectedItems;
            var vm = (this.DataContext as ApplicationViewModel);

            var filteredFigures = vm._figures.Where(x => ((string)x.ToolTip).ContainsAny(selectedTagName)).ToList();
            vm.DrawNotes(filteredFigures);
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