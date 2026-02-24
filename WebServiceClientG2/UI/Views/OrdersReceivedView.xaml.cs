using CommunityToolkit.Maui.Core;

namespace WebServiceClientG2.UI.Views;

public partial class OrdersReceivedView : ContentView
{
    UI.ViewModels.OrdersReceivedViewModel viewModel;
    public OrdersReceivedView(Base.AppEngine appEngine,
                              IPopupService popupService)
    {
        InitializeComponent();

        this.viewModel = new UI.ViewModels.OrdersReceivedViewModel(appEngine, popupService);
        this.BindingContext = viewModel;

        // Build UI programmatically to ensure Expander-like hidden section is available
        var toolbar = new Grid
        {
            HeightRequest = 62,
            ColumnDefinitions = new ColumnDefinitionCollection
            {
                new ColumnDefinition { Width = new GridLength(48) },
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = new GridLength(48) }
            }
        };

        var title = new Label
        {
            Text = "Orders received",
            FontSize = 24,
            VerticalOptions = Microsoft.Maui.Controls.LayoutOptions.Center,
            HorizontalOptions = Microsoft.Maui.Controls.LayoutOptions.Center
        };
        toolbar.Add(title, 1, 0);

        // Toggle button to show/hide add form (acts as an expander)
        var toggleButton = new Button { Text = "Show / Hide Add form", Margin = new Thickness(10) };

        // Add form (hidden by default)
        var addGrid = new Grid { IsVisible = false, RowSpacing = 8 };
        addGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        addGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        addGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        addGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        addGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

        // GUID row
        var guidLabel = new Label { Text = "GUID", Margin = new Thickness(10,0,0,0) };
        var guidEntry = new Entry();
        guidEntry.SetBinding(Entry.TextProperty, "OrderReceivedAddGUID");
        var genButton = new Button { Text = "Generovať" };
        genButton.Command = viewModel.GUIDGenerateCommand;

        var guidRow = new Grid();
        guidRow.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
        guidRow.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        guidRow.Add(guidEntry, 0, 0);
        guidRow.Add(genButton, 1, 0);

        addGrid.Add(guidLabel, 0, 0);
        addGrid.Add(guidRow, 0, 1);

        // Number
        var numberLabel = new Label { Text = "Číslo", Margin = new Thickness(10,6,0,0) };
        var numberEntry = new Entry();
        numberEntry.SetBinding(Entry.TextProperty, "OrderReceivedNumber");
        addGrid.Add(numberLabel, 0, 2);
        addGrid.Add(numberEntry, 0, 3);

        // DateDelivery
        var dateLabel = new Label { Text = "Dátum doručenia", Margin = new Thickness(10,6,0,0) };
        var dateEntry = new Entry();
        dateEntry.SetBinding(Entry.TextProperty, "OrderReceivedDateDelivery");
        addGrid.Add(dateLabel, 0, 4);
        addGrid.Add(dateEntry, 0, 5);

        // BusinessPartner GUID
        var bpLabel = new Label { Text = "BusinessPartner GUID", Margin = new Thickness(10,6,0,0) };
        var bpEntry = new Entry();
        bpEntry.SetBinding(Entry.TextProperty, "OrderReceivedBusinessPartnerGUID");
        addGrid.Add(bpLabel, 0, 6);
        addGrid.Add(bpEntry, 0, 7);

        // Notice
        var noticeLabel = new Label { Text = "Poznámka", Margin = new Thickness(10,6,0,0) };
        var noticeEntry = new Entry();
        noticeEntry.SetBinding(Entry.TextProperty, "OrderReceivedNotice");
        addGrid.Add(noticeLabel, 0, 8);
        addGrid.Add(noticeEntry, 0, 9);

        // Create button
        var createButton = new Button { Text = "Vytvoriť", Margin = new Thickness(10,12,10,0) };
        createButton.Command = viewModel.OrderReceived_AddCommand;
        addGrid.Add(createButton, 0, 10);

        toggleButton.Clicked += (s, e) => { addGrid.IsVisible = !addGrid.IsVisible; };

        var mainStack = new StackLayout { Spacing = 10, Padding = new Thickness(10) };
        mainStack.Children.Add(toolbar);
        mainStack.Children.Add(toggleButton);
        mainStack.Children.Add(addGrid);
        mainStack.Children.Add(new Label { Text = "Placeholder for Orders received operations", Margin = new Thickness(10) });

        this.Content = new ScrollView { Content = mainStack };
    }
}

