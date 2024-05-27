using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;
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


        // Zoom
        private Double zoomMax = 5;
        private Double zoomMin = 1;
        private Double zoomSpeed = 0.001;
        private Double zoom = 1;


        private void Canvas_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            var element = (UIElement)sender;

            zoom += zoomSpeed * e.Delta; // Ajust zooming speed (e.Delta = Mouse spin value )
            if (zoom < zoomMin)
                zoom = zoomMin; // Limit Min Scale
            if (zoom > zoomMax)
                zoom = zoomMax; // Limit Max Scale


            Point mousePos = e.GetPosition(element);
            
            if (zoom > 1)
                element.RenderTransform = new ScaleTransform(zoom, zoom, mousePos.X, mousePos.Y); // transform Canvas size from mouse position
            else
                element.RenderTransform = new ScaleTransform(zoom, zoom); // transform Canvas size
        }
    }
}