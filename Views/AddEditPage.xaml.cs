using praaaaa13.Models;
using praaaaa13.Services;

namespace praaaaa13.Views;

public partial class AddEditPage : ContentPage
{
    private readonly ApiService _apiService;
    private Product _product;
    private bool _isEditing;

    public AddEditPage(Product product = null)
    {
        InitializeComponent();
        _apiService = new ApiService();

        if (product != null)
        {
            _product = product;
            _isEditing = true;
            Title = "Редактировать товар";
        }
        else
        {
            _product = new Product();
            _isEditing = false;
            Title = "Добавить товар";
        }

        BindingContext = _product;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        bool success;

        if (_isEditing)
        {
            success = await _apiService.UpdateProductAsync(_product.Id, _product);
        }
        else
        {
            success = await _apiService.AddProductAsync(_product);
        }

        if (success)
        {
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Ошибка", "Не удалось сохранить товар", "OK");
        }
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}