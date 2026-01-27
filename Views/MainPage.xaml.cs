using praaaaa13.Models;
using praaaaa13.Services;
using System.Collections.ObjectModel;

namespace praaaaa13.Views;

public partial class MainPage : ContentPage
{
    private readonly ApiService _apiService;
    private ObservableCollection<Product> _products;

    public MainPage()
    {
        InitializeComponent();
        _apiService = new ApiService();
        _products = new ObservableCollection<Product>();
        lvProducts.ItemsSource = _products;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadProductsAsync();
    }

    private async Task LoadProductsAsync()
    {
        var products = await _apiService.GetProductsAsync();
        _products.Clear();
        foreach (var product in products)
        {
            _products.Add(product);
        }
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddEditPage());
    }

    private async void OnItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is Product selectedProduct)
        {
            await Navigation.PushAsync(new AddEditPage(selectedProduct));
        }
    }
}
