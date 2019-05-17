using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wolverine.Core;

namespace Wolverine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Project project;

        public Project Project
        {
            get { return project; }
            set
            {
                project = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Project)));
            }
        }

        private void UnsortedListBoxPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var obj = unsortedListBox.GetDragSource(e.GetPosition(unsortedListBox)) as Card;
            if (obj != null)
            {
                DragDrop.DoDragDrop(unsortedListBox, obj, DragDropEffects.Move);
                Project.UnsortedGroup.Cards.Remove(obj);
            }
        }

        private void OnGroupedListBoxPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var listBox = sender as ListBox;
            var obj = listBox.GetDragSource(e.GetPosition(listBox)) as Card;
            if (obj != null)
            {
                DragDrop.DoDragDrop(listBox, obj, DragDropEffects.Move);
                var sortedList = listBox.ItemsSource as ObservableCollection<Card>;
                sortedList.Remove(obj);
            }
        }

        private void OnUnsortedCardsListBoxDrop(object sender, DragEventArgs e)
        {
            Project.UnsortedGroup.Cards.Add(e.Data.GetData(typeof(Card)) as Card);
        }

        private void OnGroupedListBoxDrop(object sender, DragEventArgs e)
        {
            var target = e.OriginalSource as DependencyObject;
            var groupContainer = target.GetVisualAncestor<ListBox>(10);
            if (groupContainer != null)
            {
                var groupedItems = groupContainer.DataContext as Group;
                groupedItems.Cards.Add(e.Data.GetData(typeof(Card)) as Card);
            }
        }

        private void OpenMenuItemClick(object sender, RoutedEventArgs e)
        {
            ProjectManager projectManager = new ProjectManager(new FileStorageManger());
            Project = projectManager.Load(@"C:\Users\sesa151027\Desktop\cards.json");
        }

        private void SaveMenuItemClick(object sender, RoutedEventArgs e)
        {

        }
    }

    public static class Extensions
    {
        public static T GetVisualChild<T>(this DependencyObject parent, string childName) where T : DependencyObject
        {
            T foundChild = null;

            // Confirm parent and childName are valid. 
            if (parent != null)
            {
                int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
                for (int i = 0; i < childrenCount; i++)
                {
                    var child = VisualTreeHelper.GetChild(parent, i);
                    // If the child is not of the request child type child
                    T childType = child as T;
                    if (childType == null)
                    {
                        // recursively drill down the tree
                        foundChild = GetVisualChild<T>(child, childName);

                        // If the child is found, break so we do not overwrite the found child. 
                        if (foundChild != null) break;
                    }
                    else if (!string.IsNullOrEmpty(childName))
                    {
                        var frameworkElement = child as FrameworkElement;
                        // If the child's name is set for search
                        if (frameworkElement != null && frameworkElement.Name == childName)
                        {
                            // if the child's name is of the request name
                            foundChild = (T)child;
                            break;
                        }
                    }
                    else
                    {
                        // child element found.
                        foundChild = (T)child;
                        break;
                    }
                }
            }
            return foundChild;
        }

        public static T GetVisualAncestor<T>(this DependencyObject control, int level) where T : DependencyObject
        {
            if (level > 0 && control != null)
            {
                T visualAncestor;
                if (control is T)
                {
                    visualAncestor = control as T;
                }
                else
                {
                    level = level - 1;
                    visualAncestor = GetVisualAncestor<T>(VisualTreeHelper.GetParent(control), level);
                }
                return visualAncestor;
            }
            else
            {
                return null;
            }
        }

        public static object GetDragSource(this ListBox listBox, Point mousePoint)
        {
            var dragSourceElement = listBox.InputHitTest(mousePoint) as DependencyObject;
            var dragSourceItem = dragSourceElement.GetVisualAncestor<ListBoxItem>(10);
            if (dragSourceItem != null)
            {
                var dragSourceObject = listBox.ItemContainerGenerator.ItemFromContainer(dragSourceItem);
                return dragSourceObject;
            }
            return null;
        }
    }
}