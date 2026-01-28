using praaaaa13.Models;
using praaaaa13.Services;

namespace praaaaa13.Views;

public partial class AddEditProductPage : ContentPage
{
    private bool isEditMode = false;
    private Product currentProduct;

    public string TitleText => isEditMode ? "Редактировать продукт" : "Добавить новый продукт";
    public string ProductName { get; set; }
    public string Category { get; set; }
    public string Manufacturer { get; set; }

    public AddEditProductPage()
    {
        InitializeComponent();
        BindingContext = this;

        if (Data.Product != null)
        {
            isEditMode = true;
            currentProduct = Data.Product;
            ProductName = currentProduct.Name;
            Category = currentProduct.Category;
            Manufacturer = currentProduct.Manufacturer;
        }
        else
        {
            isEditMode = false;
        }
    }

    private async void btnSave_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(entryName.Text))
        {
            await DisplayAlert("Ошибка", "Введите название продукта", "OK");
            return;
        }

        try
        {
            var product = new Product
            {
                Name = entryName.Text,
                Category = entryCategory.Text,
                Manufacturer = entryManufacturer.Text
            };

            if (isEditMode)
            {
                product.Id = currentProduct.Id;
                await APIService.Put(product, product.Id, "api/products");
                await DisplayAlert("Успешно", "Продукт обновлен", "OK");
            }
            else
            {
                await APIService.Post(product, "api/products");
                await DisplayAlert("Успешно", "Продукт добавлен", "OK");
            }

            await Navigation.PopModalAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ошибка", $"Не удалось сохранить продукт: {ex.Message}", "OK");
        }
    }

    private async void btnCancel_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}