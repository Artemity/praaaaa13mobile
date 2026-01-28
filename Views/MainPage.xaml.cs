using praaaaa13.Models;
using praaaaa13.Services;
using System.Collections.ObjectModel;

namespace praaaaa13.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadProducts();
    }

    private void LoadProducts()
    {
        try
        {
            var products = APIService.Get<List<Product>>("api/products");
            if (products != null)
            {
                lvProducts.ItemsSource = products;
            }
            else
            {
                DisplayAlert("Внимание", "Не удалось загрузить продукты", "OK");
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Ошибка", $"Не удалось загрузить продукты: {ex.Message}", "OK");
        }
    }

    private async void btnAdd_Clicked(object sender, EventArgs e)
    {
        Data.Product = null;
        await Navigation.PushModalAsync(new AddEditProductPage());
    }

    private async void btnEdit_Clicked(object sender, EventArgs e)
    {
        var selectedProduct = (Product)lvProducts.SelectedItem;
        if (selectedProduct != null)
        {
            Data.Product = selectedProduct;
            await Navigation.PushModalAsync(new AddEditProductPage());
        }
        else
        {
            await DisplayAlert("Внимание", "Выберите продукт для редактирования", "OK");
        }
    }

    private async void btnDelete_Clicked(object sender, EventArgs e)
    {
        var selectedProduct = (Product)lvProducts.SelectedItem;
        if (selectedProduct != null)
        {
            bool confirm = await DisplayAlert("Подтверждение удаления",
                $"Вы уверены, что хотите удалить продукт: {selectedProduct.Name}?",
                "Да", "Нет");

            if (confirm)
            {
                bool success = await APIService.Delete(selectedProduct.Id, "api/products");
                if (success)
                {
                    await DisplayAlert("Успешно", "Продукт удален", "OK");
                    LoadProducts();
                }
                else
                {
                    await DisplayAlert("Ошибка", "Не удалось удалить продукт", "OK");
                }
            }
        }
        else
        {
            await DisplayAlert("Внимание", "Выберите продукт для удаления", "OK");
        }
    }
}