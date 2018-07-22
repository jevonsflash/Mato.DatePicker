using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Mato.AutoComplete.UWP
{

    public sealed partial class AutoCompleteView : UserControl
    {


        /// <summary>
        /// The execute on suggestion click property.
        /// </summary>
        public static readonly DependencyProperty ExecuteOnSuggestionClickProperty = DependencyProperty.Register(nameof(ExecuteOnSuggestionClick), typeof(bool), typeof(AutoCompleteView), new PropertyMetadata(false));

        /// <summary>
        /// The placeholder property.
        /// </summary>
        public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register(nameof(PlaceholderProperty), typeof(string), typeof(AutoCompleteView), new PropertyMetadata(string.Empty, PlaceHolderChanged));



        /// <summary>
        /// The search background color property.
        /// </summary>
        public static readonly DependencyProperty SearchBackgroundColorProperty = DependencyProperty.Register(nameof(SearchBackgroundColor), typeof(Brush), typeof(AutoCompleteView), new PropertyMetadata(new SolidColorBrush(Colors.Red), SearchBackgroundColorChanged));

        /// <summary>
        /// The search border color property.
        /// </summary>
        public static readonly DependencyProperty SearchBorderColorProperty = DependencyProperty.Register(nameof(SearchBorderColor), typeof(Brush), typeof(AutoCompleteView), new PropertyMetadata(new SolidColorBrush(Colors.White), SearchBorderColorChanged));



        /// <summary>
        /// The search command property.
        /// </summary>
        public static readonly DependencyProperty SearchCommandProperty = DependencyProperty.Register(nameof(SearchCommand), typeof(ICommand), typeof(AutoCompleteView), new PropertyMetadata(null));

        /// <summary>
        /// The search horizontal options property
        /// </summary>
        public static readonly DependencyProperty SearchHorizontalOptionsProperty = DependencyProperty.Register(nameof(SearchHorizontalOptions), typeof(HorizontalAlignment), typeof(AutoCompleteView), new PropertyMetadata(HorizontalAlignment.Stretch, SearchHorizontalOptionsChanged));

        /// <summary>
        /// The search text color property.
        /// </summary>
        public static readonly DependencyProperty SearchTextColorProperty = DependencyProperty.Register(nameof(SearchTextColor), typeof(Brush), typeof(AutoCompleteView), new PropertyMetadata(new SolidColorBrush(Colors.Red), SearchTextColorChanged));

        /// <summary>
        /// The search text property.
        /// </summary>
        public static readonly DependencyProperty SearchTextProperty = DependencyProperty.Register(nameof(SearchText), typeof(string), typeof(AutoCompleteView), new PropertyMetadata("Search", SearchTextChanged));

        /// <summary>
        /// The search vertical options property
        /// </summary>
        public static readonly DependencyProperty SearchVerticalOptionsProperty = DependencyProperty.Register(nameof(SearchVerticalOptions), typeof(VerticalAlignment), typeof(AutoCompleteView), new PropertyMetadata(VerticalAlignment.Center, SearchVerticalOptionsChanged));

        /// <summary>
        /// The selected command property.
        /// </summary>
        public static readonly DependencyProperty SelectedCommandProperty = DependencyProperty.Register(nameof(SelectedCommand), typeof(ICommand), typeof(AutoCompleteView), new PropertyMetadata(null));

        /// <summary>
        /// The selected item property.
        /// </summary>
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(nameof(SelectedItem), typeof(object), typeof(AutoCompleteView), new PropertyMetadata(null));

        /// <summary>
        /// The show search property.
        /// </summary>
        public static readonly DependencyProperty ShowSearchProperty = DependencyProperty.Register(nameof(ShowSearchButton), typeof(bool), typeof(AutoCompleteView), new PropertyMetadata(true, ShowSearchChanged));

        /// <summary>
        /// The suggestion background color property.
        /// </summary>
        public static readonly DependencyProperty SuggestionBackgroundColorProperty = DependencyProperty.Register(nameof(SuggestionBackgroundColor), typeof(Brush), typeof(AutoCompleteView), new PropertyMetadata(new SolidColorBrush(Colors.Red), SuggestionBackgroundColorChanged));

        /// <summary>
        /// The suggestion item data template property.
        /// </summary>
        public static readonly DependencyProperty SuggestionItemDataTemplateProperty = DependencyProperty.Register(nameof(SuggestionItemDataTemplate), typeof(DataTemplate), typeof(AutoCompleteView), new PropertyMetadata(null, SuggestionItemDataTemplateChanged));

        /// <summary>
        /// The suggestion height request property.
        /// </summary>
        public static readonly DependencyProperty SuggestionsHeightRequestProperty = DependencyProperty.Register(nameof(SuggestionsHeightRequest), typeof(double), typeof(AutoCompleteView), new PropertyMetadata(250.0, SuggestionHeightRequestChanged));

        /// <summary>
        /// The suggestions property.
        /// </summary>
        public static readonly DependencyProperty SuggestionsProperty = DependencyProperty.Register(nameof(Suggestions), typeof(IEnumerable), typeof(AutoCompleteView), null);

        /// <summary>
        /// The text background color property.
        /// </summary>
        public static readonly DependencyProperty TextBackgroundColorProperty = DependencyProperty.Register(nameof(TextBackgroundColor), typeof(Brush), typeof(AutoCompleteView), new PropertyMetadata(new SolidColorBrush(Colors.Transparent), TextBackgroundColorChanged));

        /// <summary>
        /// The text color property.
        /// </summary>
        public static readonly DependencyProperty TextColorProperty = DependencyProperty.Register(nameof(Foreground), typeof(Brush), typeof(AutoCompleteView), new PropertyMetadata(new SolidColorBrush(Colors.Black), TextColorChanged));

        /// <summary>
        /// The text horizontal options property
        /// </summary>
        public static readonly DependencyProperty TextHorizontalOptionsProperty = DependencyProperty.Register(nameof(TextHorizontalOptions), typeof(HorizontalAlignment), typeof(AutoCompleteView), new PropertyMetadata(HorizontalAlignment.Stretch, TextHorizontalOptionsChanged));

        /// <summary>
        /// The text property.
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(AutoCompleteView), new PropertyMetadata(string.Empty, TextValueChanged));

        /// <summary>
        /// The text vertical options property.
        /// </summary>
        public static readonly DependencyProperty TextVerticalOptionsProperty = DependencyProperty.Register(nameof(TextVerticalOptions), typeof(string), typeof(VerticalAlignment), new PropertyMetadata(VerticalAlignment.Top, TestVerticalOptionsChanged));
        private readonly ObservableCollection<IClueObject> _availableSuggestions;


        public static readonly DependencyProperty DisplayPathProperty = DependencyProperty.Register(nameof(DisplayPath), typeof(string), typeof(AutoCompleteView), new PropertyMetadata(string.Empty));

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCompleteView"/> class.
        /// </summary>
        public AutoCompleteView()
        {
            InitializeComponent();
            _availableSuggestions = new ObservableCollection<IClueObject>();

            EntText.HorizontalAlignment = TextHorizontalOptions;
            EntText.VerticalAlignment = TextVerticalOptions;
            EntText.Foreground = Foreground;
            EntText.Background = TextBackgroundColor;


            BtnSearch.VerticalAlignment = SearchVerticalOptions;
            BtnSearch.HorizontalAlignment = SearchHorizontalOptions;
            BtnSearch.Content = SearchText;



            LstSuggestions.Height = SuggestionsHeightRequest;

            LstSuggestions.ItemsSource = _availableSuggestions;


            ShowHideListbox(false);
        }

        /// <summary>
        /// Occurs when [selected item changed].
        /// </summary>
        public event EventHandler<ItemClickEventArgs> SelectedItemChanged;

        /// <summary>
        /// Occurs when [text changed].
        /// </summary>
        public event EventHandler<TextChangedEventArgs> TextChanged;

        /// <summary>
        /// Gets the available Suggestions.
        /// </summary>
        /// <value>The available Suggestions.</value>
        public IEnumerable AvailableSuggestions
        {
            get { return _availableSuggestions; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [execute on sugestion click].
        /// </summary>
        /// <value><c>true</c> if [execute on sugestion click]; otherwise, <c>false</c>.</value>
        public bool ExecuteOnSuggestionClick
        {
            get { return (bool)GetValue(ExecuteOnSuggestionClickProperty); }
            set { SetValue(ExecuteOnSuggestionClickProperty, value); }
        }

        /// <summary>
        /// Gets or sets the placeholder.
        /// </summary>
        /// <value>The placeholder.</value>
        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        /// <summary>
        /// Gets or sets the color of the search background.
        /// </summary>
        /// <value>The color of the search background.</value>
        public Brush SearchBackgroundColor
        {
            get { return (Brush)GetValue(SearchBackgroundColorProperty); }
            set { SetValue(SearchBackgroundColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the search border color.
        /// </summary>
        /// <value>The search border brush.</value>
        public Brush SearchBorderColor
        {
            get { return (Brush)GetValue(SearchBorderColorProperty); }
            set { SetValue(SearchBorderColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the search command.
        /// </summary>
        /// <value>The search command.</value>
        public ICommand SearchCommand
        {
            get { return (ICommand)GetValue(SearchCommandProperty); }
            set { SetValue(SearchCommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the search horizontal options.
        /// </summary>
        /// <value>The search horizontal options.</value>
        public HorizontalAlignment SearchHorizontalOptions
        {
            get { return (HorizontalAlignment)GetValue(SearchHorizontalOptionsProperty); }
            set { SetValue(SearchHorizontalOptionsProperty, value); }
        }

        /// <summary>
        /// Gets or sets the search text.
        /// </summary>
        /// <value>The search text.</value>
        public string SearchText
        {
            get { return (string)GetValue(SearchTextProperty); }
            set { SetValue(SearchTextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the color of the search text button.
        /// </summary>
        /// <value>The color of the search text.</value>
        public Brush SearchTextColor
        {
            get { return (Brush)GetValue(SearchTextColorProperty); }
            set { SetValue(SearchTextColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the search vertical options.
        /// </summary>
        /// <value>The search vertical options.</value>
        public VerticalAlignment SearchVerticalOptions
        {
            get { return (VerticalAlignment)GetValue(SearchVerticalOptionsProperty); }
            set { SetValue(SearchVerticalOptionsProperty, value); }
        }


        /// <summary>
        /// Gets or sets the selected command.
        /// </summary>
        /// <value>The selected command.</value>
        public ICommand SelectedCommand
        {
            get { return (ICommand)GetValue(SelectedCommandProperty); }
            set { SetValue(SelectedCommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        /// <value>The selected item.</value>
        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show search button].
        /// </summary>
        /// <value><c>true</c> if [show search button]; otherwise, <c>false</c>.</value>
        public bool ShowSearchButton
        {
            get { return (bool)GetValue(ShowSearchProperty); }
            set { SetValue(ShowSearchProperty, value); }
        }

        /// <summary>
        /// Gets or sets the color of the sugestion background.
        /// </summary>
        /// <value>The color of the sugestion background.</value>
        public Brush SuggestionBackgroundColor
        {
            get { return (Brush)GetValue(SuggestionBackgroundColorProperty); }
            set { SetValue(SuggestionBackgroundColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the suggestion item data template.
        /// </summary>
        /// <value>The sugestion item data template.</value>
        public DataTemplate SuggestionItemDataTemplate
        {
            get { return (DataTemplate)GetValue(SuggestionItemDataTemplateProperty); }
            set { SetValue(SuggestionItemDataTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Suggestions.
        /// </summary>
        /// <value>The Suggestions.</value>
        public IEnumerable Suggestions
        {
            get { return (IEnumerable)GetValue(SuggestionsProperty); }
            set { SetValue(SuggestionsProperty, value); }
        }

        /// <summary>
        /// Gets or sets the height of the suggestion.
        /// </summary>
        /// <value>The height of the suggestion.</value>
        public double SuggestionsHeightRequest
        {
            get { return (double)GetValue(SuggestionsHeightRequestProperty); }
            set { SetValue(SuggestionsHeightRequestProperty, value); }
        }
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the color of the text background.
        /// </summary>
        /// <value>The color of the text background.</value>
        public Brush TextBackgroundColor
        {
            get { return (Brush)GetValue(TextBackgroundColorProperty); }
            set { SetValue(TextBackgroundColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the color of the text.
        /// </summary>
        /// <value>The color of the text.</value>
        public new Brush Foreground
        {
            get { return (Brush)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the text horizontal options.
        /// </summary>
        /// <value>The text horizontal options.</value>
        public HorizontalAlignment TextHorizontalOptions
        {
            get { return (HorizontalAlignment)GetValue(TextHorizontalOptionsProperty); }
            set { SetValue(TextHorizontalOptionsProperty, value); }
        }

        public VerticalAlignment TextVerticalOptions
        {
            get { return (VerticalAlignment)GetValue(TextVerticalOptionsProperty); }
            set { SetValue(TextVerticalOptionsProperty, value); }
        }

        public string DisplayPath
        {
            get { return (string)GetValue(DisplayPathProperty); }
            set { SetValue(DisplayPathProperty, value); }
        }
        private static void PlaceHolderChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {

            string oldPlaceHolderValue = e.OldValue as string;
            string newPlaceHolderValue = e.NewValue as string;
            var autoCompleteView = obj as AutoCompleteView;
            if (autoCompleteView != null)
            {
                autoCompleteView.EntText.PlaceholderText = newPlaceHolderValue;
            }
        }


        private static void SearchBackgroundColorChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Brush oldValue = e.OldValue as Brush;
            Brush newValue = e.NewValue as Brush;
            var autoCompleteView = obj as AutoCompleteView;
            if (autoCompleteView != null)
            {
                autoCompleteView.StkBase.Background = newValue;
            }
        }

        private static void SearchBorderColorChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {

            Brush oldValue = e.OldValue as Brush;
            Brush newValue = e.NewValue as Brush;
            var autoCompleteView = obj as AutoCompleteView;
            if (autoCompleteView != null)
            {
                autoCompleteView.BtnSearch.BorderBrush = newValue;
            }
        }



        private static void SearchHorizontalOptionsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {

            var oldValue = (HorizontalAlignment)e.OldValue;
            var newValue = (HorizontalAlignment)e.NewValue;
            var autoCompleteView = obj as AutoCompleteView;
            if (autoCompleteView != null)
            {
                autoCompleteView.BtnSearch.HorizontalAlignment = newValue;
            }
        }

        private static void SearchTextChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var oldValue = (string)e.OldValue;
            var newValue = (string)e.NewValue;
            var autoCompleteView = obj as AutoCompleteView;
            if (autoCompleteView != null)
            {
                autoCompleteView.BtnSearch.Content = newValue;
            }
        }

        private static void SearchTextColorChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {

            Brush oldValue = e.OldValue as Brush;
            Brush newValue = e.NewValue as Brush;
            var autoCompleteView = obj as AutoCompleteView;
            if (autoCompleteView != null)
            {
                autoCompleteView.BtnSearch.Foreground = newValue;
            }
        }

        private static void SearchVerticalOptionsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {

            var oldValue = (VerticalAlignment)e.OldValue;
            var newValue = (VerticalAlignment)e.NewValue;
            var autoCompleteView = obj as AutoCompleteView;
            if (autoCompleteView != null)
            {
                autoCompleteView.BtnSearch.VerticalAlignment = newValue;
            }
        }

        private static void ShowSearchChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var newShowSearchValue = (bool)e.NewValue;

            var autoCompleteView = obj as AutoCompleteView;
            if (autoCompleteView != null)
            {
                if (newShowSearchValue)
                {


                    autoCompleteView.BtnSearch.Visibility = Visibility.Visible;
                }
                else
                {
                    autoCompleteView.BtnSearch.Visibility = Visibility.Collapsed;

                }
            }
        }

        private static void SuggestionBackgroundColorChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {

            Brush oldValue = e.OldValue as Brush;
            Brush newValue = e.NewValue as Brush;
            var autoCompleteView = obj as AutoCompleteView;
            if (autoCompleteView != null)
            {
                autoCompleteView.LstSuggestions.Background = newValue;
            }
        }

        /// <summary>
        /// Suggestions the height changed.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void SuggestionHeightRequestChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {

            int newValue = (int)e.NewValue;
            var autoCompleteView = obj as AutoCompleteView;
            if (autoCompleteView != null)
            {
                autoCompleteView.LstSuggestions.Height = newValue;
            }
        }
        /// <summary>
        /// Suggestions the item data template changed.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="oldShowSearchValue">The old show search value.</param>
        /// <param name="newShowSearchValue">The new show search value.</param>
        private static void SuggestionItemDataTemplateChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            DataTemplate newShowSearchValue = (DataTemplate)e.NewValue;

            var autoCompleteView = obj as AutoCompleteView;
            if (autoCompleteView != null)
            {
                autoCompleteView.LstSuggestions.ItemTemplate = newShowSearchValue;
            }
        }

        /// <summary>
        /// Tests the vertical options changed.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void TestVerticalOptionsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            VerticalAlignment newValue = (VerticalAlignment)e.NewValue;

            var autoCompleteView = obj as AutoCompleteView;
            if (autoCompleteView != null)
            {
                autoCompleteView.EntText.VerticalAlignment = newValue;
            }
        }

        /// <summary>
        /// Texts the background color changed.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void TextBackgroundColorChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Brush newValue = e.NewValue as Brush;
            var autoCompleteView = obj as AutoCompleteView;
            if (autoCompleteView != null)
            {
                autoCompleteView.EntText.Background = newValue;
            }
        }

        /// <summary>
        /// Texts the color changed.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void TextColorChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Brush newValue = e.NewValue as Brush;
            var autoCompleteView = obj as AutoCompleteView;
            if (autoCompleteView != null)
            {
                autoCompleteView.EntText.Foreground = newValue;
            }
        }

        /// <summary>
        /// Texts the horizontal options changed.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void TextHorizontalOptionsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            VerticalAlignment newValue = (VerticalAlignment)e.NewValue;
            var autoCompleteView = obj as AutoCompleteView;
            if (autoCompleteView != null)
            {
                autoCompleteView.EntText.VerticalAlignment = newValue;
            }
        }
        /// <summary>
        /// Texts the changed.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="oldPlaceHolderValue">The old place holder value.</param>
        /// <param name="newPlaceHolderValue">The new place holder value.</param>
        private static async void TextValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {

            var oldPlaceHolderValue = (string)e.OldValue;
            var newPlaceHolderValue = (string)e.NewValue;
            var control = obj as AutoCompleteView;

            if (control != null)
            {
                control.BtnSearch.IsEnabled = !string.IsNullOrEmpty(newPlaceHolderValue);

                var cleanedNewPlaceHolderValue = Regex.Replace((newPlaceHolderValue ?? string.Empty).ToLowerInvariant(), @"\s+", string.Empty);

                if (!string.IsNullOrEmpty(cleanedNewPlaceHolderValue) && control.Suggestions != null)
                {
                    var suggestions = control.Suggestions;
                    var filteredSuggestions = await Task.Run(() =>
                    {

                        var filteredSuggestionsList = suggestions.Cast<IClueObject>()
                            .Where(
                                x =>
                                {
                                    var result = false;
                                    foreach (var item in x.ClueStrings)
                                    {
                                        if (item.StartsWith(cleanedNewPlaceHolderValue, StringComparison.OrdinalIgnoreCase))
                                        {
                                            result = true;
                                        }
                                    }
                                    return result;
                                })
                            .OrderByDescending(x =>
                            {
                                var result = x.ClueStrings.IndexOf(cleanedNewPlaceHolderValue);
                                return result;
                            }).ToList();
                        return filteredSuggestionsList;
                    });
                    control._availableSuggestions.Clear();
                    if (filteredSuggestions.Count > 0)
                    {

                        foreach (var suggestion in filteredSuggestions)
                        {
                            control._availableSuggestions.Add(suggestion);
                        }

                        control.ShowHideListbox(true);
                    }
                    else
                    {
                        control.ShowHideListbox(false);
                    }

                }
                else
                {
                    if (control._availableSuggestions.Count > 0)
                    {
                        control._availableSuggestions.Clear();
                        control.ShowHideListbox(false);
                    }
                }
            }
        }

        /// <summary>
        /// Called when [selected item changed].
        /// </summary>
        /// <param name="selectedItem">The selected item.</param>
        private void OnSelectedItemChanged(object selectedItem)
        {
            SelectedItem = selectedItem;

            if (SelectedCommand != null)
                SelectedCommand.Execute(selectedItem);

            var handler = SelectedItemChanged;
            handler?.Invoke(this, new ItemClickEventArgs() { });
        }

        /// <summary>
        /// Handles the <see cref="E:TextChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
        private void OnTextChanged(TextChangedEventArgs e)
        {
            TextChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Shows the hide listbox.
        /// </summary>
        /// <param name="show">if set to <c>true</c> [show].</param>
        private void ShowHideListbox(bool show)
        {
            if (show)
            {
                LstSuggestions.Visibility = Visibility.Visible;

            }
            else
            {
                LstSuggestions.Visibility = Visibility.Collapsed;

            }
        }

        private void EntText_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var obj = (sender as TextBox);
            Text = obj.Text;
            OnTextChanged(e);
        }

        private void BtnSearch_OnClicked(object sender, RoutedEventArgs e)
        {
            if (SearchCommand != null && SearchCommand.CanExecute(Text))
            {
                SearchCommand.Execute(Text);
            }

        }

        private void LstSuggestions_OnItemSelected(object sender, ItemClickEventArgs e)
        {
            var SelectedItem = e.ClickedItem;
            //If not null, bind as specified by Path
            if (!string.IsNullOrEmpty(DisplayPath))
            {
                this.EntText.DataContext = SelectedItem;
                var binding = new Binding() { Path = new PropertyPath(DisplayPath) };
                EntText.SetBinding(TextBox.TextProperty, binding);

            }
            //If Path is empty, assignment is based on the first result of ClueString
            else
            {
                var clueObject = SelectedItem as IClueObject;
                if (clueObject != null)
                {
                    var candidateDisplay = clueObject.ClueStrings.FirstOrDefault();
                    //The first result of ClueString is empty, only ToString, What a tragedy!
                    EntText.Text = !string.IsNullOrEmpty(candidateDisplay) ?
                        candidateDisplay :
                        SelectedItem.ToString();
                }
            }
            _availableSuggestions.Clear();
            ShowHideListbox(false);
            OnSelectedItemChanged(e.ClickedItem);

            if (ExecuteOnSuggestionClick
                && SearchCommand != null
                && SearchCommand.CanExecute(Text))
            {
                SearchCommand.Execute(e);
            }
        }


    }


}
